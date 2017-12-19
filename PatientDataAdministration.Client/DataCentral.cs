﻿using System;
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
        
        private bool _isSyncInProgress;
        private bool _onDemandSyncEnabled;
        private bool _killCommandReceived;

        private readonly UserCredential _userCredential;
        private readonly SubInformationManagement _subInfoMan;

        private readonly Process _process = new Process();
        private static List<OperationQueue> _operationQueue = new List<OperationQueue>();

        private static int GenerateVersionCode(string version)
        {
            return Convert.ToInt32(version.Replace(".", "").Replace("v", "").Trim());
        }

        public static void PushEvents(OperationQueue operationQueue)
        {
            _operationQueue.Add(operationQueue);
        }

        public DataCentral(UserCredential userCredential)
        {
            _onDemandSyncEnabled = false;
            InitializeComponent();

            try
            {
                this._userCredential = userCredential;
                _subInfoMan = new SubInformationManagement(_userCredential);

                _onDemandSyncEnabled = Convert.ToBoolean(
                    LocalCache.Get<List<System_Setting>>("System_Setting")
                        .FirstOrDefault(x => x.SettingKey == (int)EnumLibrary.SettingKey.OnDemandSync)?
                        .SettingValue ?? "false");

                btnSync.Visible = !_onDemandSyncEnabled;

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
                                        x => x.SettingKey == (int) EnumLibrary.SettingKey.RemoteApi)?.SettingValue ?? "",
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
            catch (Exception e)
            {
                LocalCore.TreatError(e, _userCredential.AdministrationStaffInformation.Id);
            }
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
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
        }

        private void DataCentral_Load(object sender, EventArgs e)
        {
            lblUserInformation.Text = @"Welcome, " +
                                      _userCredential.AdministrationStaffInformation.Surname + @" " +
                                      _userCredential.AdministrationStaffInformation.FirstName +
                                      @". You are attached to " +
                                      _userCredential.AdministrationSiteInformation.SiteNameOfficial;
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
                LocalCore.TreatError(ex, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            //
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            picSyncInProcess.Visible = _isSyncInProgress;
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
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
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

                if(!bgwNewPatient.IsBusy)
                    bgwNewPatient.RunWorkerAsync();

                if(!bgwUpdatePatient.IsBusy)
                    bgwUpdatePatient.RunWorkerAsync();
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _userCredential.AdministrationStaffInformation.Id);
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
                existingPatient.LastUpdate = patientInformation.Patient_PatientInformation.LastUpdated ?? DateTime.Now;
                existingPatient.LastSync = patientInformation.Patient_PatientInformation.LastUpdated ?? DateTime.Now;
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
                    LastUpdate = patientInformation.Patient_PatientInformation.LastUpdated ?? DateTime.Now,
                    LastSync = patientInformation.Patient_PatientInformation.LastUpdated ?? DateTime.Now, 
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
            try
            {
                picConnectionAvailable.Visible = LocalCore.Get($@"/ClientCommunication/Misc/Ping").Result.Status;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
                picConnectionAvailable.Visible = false;
            }
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
            ExecuteSync();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            InitiateSync();
        }

        private void bgwNewPatient_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (_isSyncInProgress)
                    return;

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Initiating Synchronization Task: New Entries from Partner Sites"
                });

                var pulled = 0;
                while (true)
                {
                    if (_killCommandReceived)
                        break;

                    var patientInformation = _localPdaEntities.System_BioDataStore.Select(x => x.PepId).ToList();

                    var listOfPepId = "";

                    if (patientInformation.Any())
                    {
                        listOfPepId = patientInformation.Aggregate("",
                            (current, patientInfo) => current + $"{patientInfo},");
                        listOfPepId = listOfPepId.Substring(0, listOfPepId.Length - 1);
                    }
                    var data = Encoding.ASCII.GetBytes(listOfPepId);

                    var payLoad = new
                    {
                        encodedListOfAvailablePepId = Convert.ToBase64String(data),
                        siteId = _userCredential.AdministrationSiteInformation.Id
                    };

                    var responseData = LocalCore.Post($@"/ClientCommunication/Sync/PullNew",
                        Newtonsoft.Json.JsonConvert.SerializeObject(payLoad));

                    if (responseData.Status)
                    {
                        _isSyncInProgress = true;

                        _operationQueue.Add(new OperationQueue()
                        {
                            Param = responseData.Message
                        });

                        var returnedPatients =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientInformation>>(
                                Newtonsoft.Json.JsonConvert.SerializeObject(responseData.Data));

                        if (returnedPatients.Count == 0)
                            break;

                        pulled += returnedPatients.Count;

                        foreach (var patient in returnedPatients)
                        {
                            if (_killCommandReceived)
                                break;

                            SaveUpdate(patient);
                        }

                        if (_onDemandSyncEnabled)
                            _subInfoMan.UpdatePersistedData();
                        else _operationQueue.Add(new OperationQueue()
                        {
                            Param = $"New Patient Pull in Progress ... Pulled {pulled} so far"
                        });

                        _isSyncInProgress = false;
                    }

                    if (!_onDemandSyncEnabled)
                        _subInfoMan.UpdatePersistedData();
                }

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Completing Synchronization Task: New Entries from Partner Sites"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }

        private void bgwUpdatePatient_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Initiating Synchronization Task: Modified Entries from Partner Sites"
                });

                var totalCount = _localPdaEntities.System_BioDataStore.Count();

                for (var i = 0; i > totalCount; i += 100)
                {
                    if (_killCommandReceived)
                        break;

                    var patientsMatching =
                        _localPdaEntities.System_BioDataStore.Take(100).Skip(i).Select(
                            x => new PatientMatching() { PepId = x.PepId, LastUpdate = x.LastUpdate }).ToList();

                    var responseData = LocalCore.Post($@"/ClientCommunication/Sync/CheckIfUpdated",
                        Newtonsoft.Json.JsonConvert.SerializeObject(patientsMatching ?? new List<PatientMatching>()));

                    if (!responseData.Status)
                        continue;

                    _isSyncInProgress = true;
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
                            break;

                        SaveUpdate(patient);
                    }

                    if (_onDemandSyncEnabled)
                        _subInfoMan.UpdatePersistedData();
                    else _operationQueue.Add(new OperationQueue()
                    {
                        Param = $"Existing Patient Pull in Progress ... Completed {i} of {totalCount}"
                    });

                    _isSyncInProgress = false;
                }

                if (!_onDemandSyncEnabled)
                    _subInfoMan.UpdatePersistedData();

                _operationQueue.Add(new OperationQueue()
                {
                    Param = "Completing Synchronization Task: Modified Entries from Partner Sites"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _userCredential.AdministrationStaffInformation.Id);
            }
        }
    }
}
