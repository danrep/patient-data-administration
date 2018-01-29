using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public partial class DataCentral : MetroFramework.Forms.MetroForm
    {
        readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        private bool _isSyncNewInProgress;
        private bool _isSyncUpdateInProgress;
        private bool _onDemandSyncEnabled;
        private bool _killCommandReceived;
        private bool _pingResult;

        private Administration_StaffInformation _administrationStaffInformation;
        private System_SiteData _systemSiteData;
        private SubInformationManagement _subInfoMan;

        private Process _process = new Process();
        private static List<OperationQueue> _operationQueue = new List<OperationQueue>();

        private static int GenerateVersionCode(string version)
        {
            return Convert.ToInt32(version.Replace(".", "").Replace("v", "").Trim());
        }

        public DataCentral(Administration_StaffInformation administrationStaffInformation)
        {
            InitializeComponent();

            _operationQueue.Add(new OperationQueue() { Param = "PDA Initializing" });
            this._administrationStaffInformation = administrationStaffInformation;

            this._systemSiteData = _localPdaEntities.System_SiteData.FirstOrDefault(x => x.IsCurrent && !x.IsDeleted);
            if (_systemSiteData != null)
                _operationQueue.Add(new OperationQueue() { Param = "Settings Loaded" });

            _onDemandSyncEnabled = Convert.ToBoolean(
                LocalCache.Get<List<System_Setting>>("System_Setting")
                    .FirstOrDefault(x => x.SettingKey == (int)EnumLibrary.SettingKey.OnDemandSync)?
                    .SettingValue ?? "false");

            _operationQueue.Add(new OperationQueue()
            {
                Param = "Real Time Synchronization " + (_onDemandSyncEnabled ? "Enabled" : "Disabled")
            });

            SiteInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                _onDemandSyncEnabled = false;
                _killCommandReceived = true;

                bgwNewPatient.CancelAsync();
                bgwUpdatePatient.CancelAsync();

                this.Close();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
        }

        private void DataCentral_Load(object sender, EventArgs e)
        {
            lblUserInformation.Text = @"Welcome, " +
                                      _administrationStaffInformation.Surname + @" " +
                                      _administrationStaffInformation.FirstName +
                                      @". You are attached to " +
                                     _systemSiteData.SiteNameOfficial;
        }

        private void btnPatientManagement_Click(object sender, EventArgs e)
        {
            try
            {
                _subInfoMan.Show();
                _subInfoMan.BringToFront();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            picConnectionAvailable.Visible = _pingResult;

            picSyncInProcess.Visible = _isSyncNewInProgress;
            if (_isSyncNewInProgress)
                return;

            if (_isSyncUpdateInProgress)
                picSyncInProcess.Visible = _isSyncUpdateInProgress;
        }

        private void tmrPostInfoLogs_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrPostInfoLogs.Enabled = false;
                var timeBar = DateTime.Now;

                var listOfEvents = _operationQueue.Where(x => x.TimeStamp < timeBar).ToList();
                foreach (var eventItem in listOfEvents.OrderBy(x => x.TimeStamp))
                {
                    lstBoxInfoLog.Items.Add($"{eventItem.TimeStamp:yyyyMMddHHmmss}: {eventItem.Param}");
                    lstBoxInfoLog.SelectedIndex = lstBoxInfoLog.Items.Count - 1;
                    Application.DoEvents();
                }

                _operationQueue.RemoveAll(x => x.TimeStamp < timeBar);

                tmrPostInfoLogs.Enabled = true;

                if (bgwNewPatient.IsBusy || bgwUpdatePatient.IsBusy)
                {
                    btnSync.Visible = false;
                    btnCancelSync.Visible = true;
                } 

                if (bgwNewPatient.IsBusy == bgwUpdatePatient.IsBusy)
                {
                    btnCancelSync.Visible = bgwNewPatient.IsBusy;
                    btnSync.Visible = !bgwNewPatient.IsBusy;
                }

                picDataReady.Visible = !_subInfoMan._isBusy;
                picDataWait.Visible = _subInfoMan._isBusy;

                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void InitiateSync()
        {
            try
            {
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Initiating Synchronization Tasks"
                });

                if (!bgwNewPatient.IsBusy)
                    bgwNewPatient.RunWorkerAsync();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }
        }

        private void ExecuteSync()
        {
            if (!_onDemandSyncEnabled)
                return;

            InitiateSync();
        }

        private void tmrSync_Tick(object sender, EventArgs e)
        {
            ExecuteSync();
        }

        private void SaveUpdate(PatientInformation patientInformation)
        {
            var localPdaEntitiesPatientUpdateSync = new LocalPDAEntities();
            if (
                localPdaEntitiesPatientUpdateSync.System_BioDataStore.Any(
                    x => x.PepId == patientInformation.Patient_PatientInformation.PepId))
            {

                var existingPatient = localPdaEntitiesPatientUpdateSync.System_BioDataStore.FirstOrDefault(
                    x => x.PepId == patientInformation.Patient_PatientInformation.PepId);

                if (existingPatient == null)
                    return;

                existingPatient.FullName = patientInformation.Patient_PatientInformation.Surname + @" " +
                                           patientInformation.Patient_PatientInformation.Othername;
                existingPatient.IsSync = true;
                existingPatient.LastUpdate = DateTime.Now;
                existingPatient.LastSync = DateTime.Now;
                existingPatient.SiteId = patientInformation.Patient_PatientInformation.SiteId;

                if (patientInformation.Patient_PatientNearFieldCommunicationData != null)
                    existingPatient.NfcUid = patientInformation.Patient_PatientNearFieldCommunicationData.CardId;

                if (patientInformation.Patient_PatientBiometricData != null)
                {
                    existingPatient.PrimaryFinger = patientInformation.Patient_PatientBiometricData.FingerPrimary;
                    existingPatient.SecondaryFinger = patientInformation.Patient_PatientBiometricData.FingerSecondary;
                }

                existingPatient.PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientInformation);

                localPdaEntitiesPatientUpdateSync.Entry(existingPatient).State = EntityState.Modified;
                localPdaEntitiesPatientUpdateSync.SaveChanges();
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Updated Patient with PEPID " + patientInformation.Patient_PatientInformation.PepId
                });
            }
            else
            {
                var newPatient = new System_BioDataStore
                {
                    PepId = patientInformation.Patient_PatientInformation.PepId,
                    FullName = patientInformation.Patient_PatientInformation.Surname + @" " +
                               patientInformation.Patient_PatientInformation.Othername,
                    IsSync = true,
                    LastUpdate = DateTime.Now,
                    LastSync = DateTime.Now, 
                    SiteId = patientInformation.Patient_PatientInformation.SiteId
                };

                if (patientInformation.Patient_PatientNearFieldCommunicationData != null)
                    newPatient.NfcUid = patientInformation.Patient_PatientNearFieldCommunicationData.CardId;

                if (patientInformation.Patient_PatientBiometricData != null)
                {
                    newPatient.PrimaryFinger = patientInformation.Patient_PatientBiometricData.FingerPrimary;
                    newPatient.SecondaryFinger = patientInformation.Patient_PatientBiometricData.FingerSecondary;
                }

                newPatient.PatientData = Newtonsoft.Json.JsonConvert.SerializeObject(patientInformation);

                localPdaEntitiesPatientUpdateSync.System_BioDataStore.Add(newPatient);
                localPdaEntitiesPatientUpdateSync.SaveChanges();

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Added Patient with PEPID " + patientInformation.Patient_PatientInformation.PepId
                });
            }
        }

        private void tmrCheckConnection_Tick(object sender, EventArgs e)
        {
            if (!bgwPing.IsBusy)
                bgwPing.RunWorkerAsync();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            //
        }

        private void tmrLaunchUpdate_Tick(object sender, EventArgs e)
        {
            if (_process.HasExited)
            {
                _operationQueue.Add(new OperationQueue() { Param = $"Download of New Update is complete." });
                picIndUpdate.Visible = false;
                tmrLaunchUpdate.Enabled = false;
            }
        }

        private void DataCentral_Shown(object sender, EventArgs e)
        {
            try
            {
                ExecuteSync();

                _operationQueue.Add(new OperationQueue() { Param = "Initializing Patient Data Management" });
                _subInfoMan = new SubInformationManagement(_administrationStaffInformation);
                _operationQueue.Add(new OperationQueue() { Param = "Patient Data Management Initialization Complete" });

                btnSync.Visible = !_onDemandSyncEnabled;
                
                _operationQueue.Add(new OperationQueue(){Param = "Starting Patient Data Load"});
                _subInfoMan.UpdatePersistedData();

                new Thread(() =>
                {
                    var responseData = LocalCore.Get($@"/ClientCommunication/Misc/GetUpdateData").Result;
                    if (!responseData.Status)
                        return;

                    if (responseData.Data == null)
                        return;

                    var update =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<System_Update>(
                            Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                    if (GenerateVersionCode(update.VersionNumber) <=
                        GenerateVersionCode(ConfigurationManager.AppSettings["appVersion"]))
                        return;

                    _operationQueue.Add(new OperationQueue() { Param = $"Detected Update {update.VersionNumber}" });

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "PatientDataAdministration.ClientUpdater.exe",
                        Arguments = _localPdaEntities.System_Setting.FirstOrDefault(
                                        x => x.SettingKey == (int)EnumLibrary.SettingKey.RemoteApi)?.SettingValue ?? "",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    _process.StartInfo = startInfo;
                    _process.EnableRaisingEvents = true;
                    _process.Start();

                    tmrLaunchUpdate.Enabled = true;
                    tmrLaunchUpdate.Start();

                    _operationQueue.Add(new OperationQueue() { Param = $"Download of Update {update.VersionNumber} is running" });
                }).Start();
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue() { Param = ex.Message });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id, true);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            _killCommandReceived = false;
            SiteInfo();
            InitiateSync();
        }

        public void SiteInfo()
        {
            _operationQueue.Add(new OperationQueue()
            {
                Param = $"Current Site is {_systemSiteData.SiteNameOfficial}"
            });
        }

        private void bgwNewPatient_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (_isSyncNewInProgress)
                    return;

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Initiating Synchronization Task: New Entries from Partner Sites"
                });
                var innerEntity = new LocalPDAEntities();
                var pulled = 0;
                var patientInformation = new List<string>();

                while (true)
                {
                    if (_killCommandReceived)
                    {
                        _operationQueue.Add(new OperationQueue()
                        {
                            Param = $"STOP Command Received"
                        });
                        break;
                    }

                    var listOfPepId = "";

                    if (innerEntity.System_BioDataStore.Any())
                    {
                        patientInformation = innerEntity.System_BioDataStore.Select(x => x.PepId).ToList();

                        listOfPepId = patientInformation.Aggregate("",
                            (current, patientInfo) => current + $"{patientInfo},");
                        listOfPepId = listOfPepId.Substring(0, listOfPepId.Length - 1);

                        patientInformation.Clear();
                    }

                    var data = Encoding.ASCII.GetBytes(listOfPepId);

                    var payLoad = new
                    {
                        encodedListOfAvailablePepId = Convert.ToBase64String(data),
                        siteId = _systemSiteData.RemoteSiteId
                    };

                    var responseData = LocalCore.Post($@"/ClientCommunication/Sync/PullNew",
                        Newtonsoft.Json.JsonConvert.SerializeObject(payLoad));

                    if (responseData.Status)
                    {
                        _isSyncNewInProgress = true;

                        _operationQueue.Add(new OperationQueue()
                        {
                            Param = responseData.Message
                        });

                        var returnedPatients =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientInformation>>(
                                Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                        if (returnedPatients.Count == 0)
                        {
                            if (!_onDemandSyncEnabled)
                                _operationQueue.Add(new OperationQueue()
                                {
                                    Param = $"No New Patients found this time. Please Try the Synchronization Later"
                                });

                            break;
                        }

                        pulled += returnedPatients.Count;

                        foreach (var patient in returnedPatients)
                        {
                            if (_killCommandReceived)
                            {
                                _operationQueue.Add(new OperationQueue()
                                {
                                    Param = $"STOP Command Received"
                                });
                                break;
                            }

                            SaveUpdate(patient);
                        }

                        if (_onDemandSyncEnabled)
                            _subInfoMan.UpdatePersistedData();
                        else
                            _operationQueue.Add(new OperationQueue()
                            {
                                Param = $"New Patient Pull in Progress ... Pulled {pulled} so far"
                            });

                        _isSyncNewInProgress = false;
                    }
                }

                if (!_onDemandSyncEnabled)
                    _subInfoMan.UpdatePersistedData();

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Completing Synchronization Task: New Entries from Partner Sites"
                });
            }
            catch (InvalidOperationException)
            {
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Please wait. System is stil loading. Try again shortly!"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "ERROR >> Please retry the last action"
                });
            }

            _isSyncNewInProgress = false;
        }

        private void bgwUpdatePatient_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Initiating Synchronization Task: Modified Entries from Partner Sites"
                });

                var innerEntity = new LocalPDAEntities();
                var totalCount = innerEntity.System_BioDataStore.Count();

                for (var i = 0; i < totalCount; i += 100)
                {
                    if (_killCommandReceived)
                    {
                        _operationQueue.Add(new OperationQueue()
                        {
                            Param = $"STOP Command Received"
                        });
                        break;
                    }

                    var patientsMatching =
                        innerEntity.System_BioDataStore.OrderBy(x => x.Id).Skip(i).Take(100).Select(
                            x => new PatientMatching() { PepId = x.PepId, LastUpdate = x.LastUpdate }).ToList();

                    var responseData = LocalCore.Post($@"/ClientCommunication/Sync/CheckIfUpdated",
                        Newtonsoft.Json.JsonConvert.SerializeObject(patientsMatching ?? new List<PatientMatching>()));

                    if (!responseData.Status)
                        continue;

                    _isSyncUpdateInProgress = true;
                    _operationQueue.Add(new OperationQueue()
                    {
                        Param = responseData.Message
                    });

                    var returnedPatients =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientInformation>>(
                            Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                    foreach (var patient in returnedPatients)
                    {
                        if (_killCommandReceived)
                        {
                            _operationQueue.Add(new OperationQueue()
                            {
                                Param = $"STOP Command Received"
                            });
                            break;
                        }

                        SaveUpdate(patient);
                    }

                    if (_onDemandSyncEnabled)
                        _subInfoMan.UpdatePersistedData();
                    else _operationQueue.Add(new OperationQueue()
                    {
                        Param = $"Existing Patient Pull in Progress ... Completed {i + patientsMatching.Count} of {totalCount}"
                    });

                    _isSyncUpdateInProgress = false;
                }

                if (!_onDemandSyncEnabled)
                    _subInfoMan.UpdatePersistedData();

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Completing Synchronization Task: Modified Entries from Partner Sites"
                });
            }
            catch (InvalidOperationException)
            {
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Please wait. System is stil loading. Try again shortly!"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "ERROR >> Please retry the last action"
                });
            }

            _isSyncUpdateInProgress = false;
        }

        private void btnCancelSync_Click(object sender, EventArgs e)
        {
            _killCommandReceived = true;

            bgwNewPatient.CancelAsync();
            bgwUpdatePatient.CancelAsync();

            _isSyncNewInProgress = _isSyncUpdateInProgress = false;

            _subInfoMan.UpdatePersistedData();
        }

        private void bgwNewPatient_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!bgwUpdatePatient.IsBusy)
                    bgwUpdatePatient.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void bgwPing_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                _pingResult = LocalCore.Get($@"/ClientCommunication/Misc/Ping").Result.Status;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _pingResult = false;
            }
        }
    }
}
