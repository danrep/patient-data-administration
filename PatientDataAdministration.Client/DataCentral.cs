using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;
using MetroFramework.Forms;
using Newtonsoft.Json;
using PatientDataAdministration.Client.LocalSettingStorage;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using LocalCache = PatientDataAdministration.Client.LocalSettingStorage.LocalCache;
using ThreadState = System.Threading.ThreadState;

namespace PatientDataAdministration.Client
{
    public partial class DataCentral : MetroForm
    {
        private bool _isSyncNewInProgress;
        private bool _isSyncUpdateInProgress;
        private bool _onDemandSyncEnabled;
        private bool _killCommandReceived;
        private bool _pingResult;

        private int _endPointId;

        private CheckState _checkState = CheckState.Checked;

        private readonly Administration_StaffInformation _administrationStaffInformation;
        private readonly System_SiteData _systemSiteData;

        private static List<OperationQueue> _operationQueue;

        private SubInformationManagement _subInfoMan;
        private SubHtc _subPopReg;

        private HttpSelfHostServer _selfServer;
        private readonly Process _process = new Process();

        private Thread _threadPullDataAppointment;
        private Thread _threadPushDataBiometrics;

        public DataCentral(Administration_StaffInformation administrationStaffInformation)
        {
            try
            {
                InitializeComponent();

                _operationQueue = new List<OperationQueue> { new OperationQueue { Param = "PDA Initializing" } };

                _administrationStaffInformation = administrationStaffInformation;

                using (var entities = new LocalPDAEntities())
                {
                    _systemSiteData = entities.System_SiteData.FirstOrDefault(x => x.IsCurrent && !x.IsDeleted);
                }

                if (_systemSiteData != null)
                    _operationQueue.Add(new OperationQueue { Param = "Settings Loaded" });

                _onDemandSyncEnabled = Convert.ToBoolean(
                    LocalCache.Get<List<System_Setting>>("System_Setting")
                        .FirstOrDefault(x => x.SettingKey == (int)SyncMode.OnDemandSync)?
                        .SettingValue ?? "false");

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Real Time Synchronization " + (_onDemandSyncEnabled ? "Enabled" : "Disabled")
                });

                SiteInfo();

                if ((UserRole) administrationStaffInformation.RoleId == UserRole.SiteAdministrator)
                {
                    btnForceUpdate.Visible = btnAdminSettings.Visible = false;
                }
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue { Param = ex.Message });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id, true);
            }
        }

        private void DataCentral_Load(object sender, EventArgs e)
        {
            lblUserInformation.Text = @"Welcome, " +
                                      _administrationStaffInformation.Surname + @" " +
                                      _administrationStaffInformation.FirstName +
                                      @". You are attached to " +
                                     _systemSiteData.SiteNameOfficial;
        }

        private void DataCentral_Shown(object sender, EventArgs e)
        {
            try
            {
                ExecuteSync();

                _operationQueue.Add(new OperationQueue { Param = "Initializing Patient Data Management" });

                _subInfoMan = new SubInformationManagement(_administrationStaffInformation);
                _subPopReg = new SubHtc(_administrationStaffInformation);

                _operationQueue.Add(new OperationQueue { Param = "Patient Data Management Initialization Complete" });

                btnSync.Visible = !_onDemandSyncEnabled;

                _operationQueue.Add(new OperationQueue { Param = "Starting Patient Data Pre Load" });
                _subInfoMan.UpdatePersistedData();
                _subPopReg.UpdatePersistedData();

                CheckForUpdates();

                new Thread(() =>
                {
                    while (_subInfoMan.UpdatePersistenceStatus == ThreadState.Running)
                    {

                    }

                    _operationQueue.Add(new OperationQueue
                    {
                        Param = "Patient Data Pre Load for Patient Information Completed Sucessfully"
                    });
                }).Start();

                new Thread(() =>
                {
                    while (_subPopReg.UpdatePersistenceStatus == ThreadState.Running)
                    {

                    }

                    _operationQueue.Add(new OperationQueue
                    {
                        Param = "Patient Data Pre Load for Population Register Completed Sucessfully"
                    });
                }).Start();

                _operationQueue.Add(new OperationQueue { Param = "Starting Application Server" });
                bgwSelfServer.RunWorkerAsync();
                _operationQueue.Add(new OperationQueue {Param = "Application Server Started Successfully"});

                _operationQueue.Add(new OperationQueue { Param = "Initializing Pull/Push for 3rd Party Data" });

                using (var entities = new LocalPDAEntities())
                {
                    var allEndPoints = EnumDictionary.GetList<LocalEndPoint>().Select(x => x.ItemId).ToList();
                    var savedEndPoints = entities.System_EndPointLog.Where(x =>
                        allEndPoints.Contains(x.EndPointId) && !string.IsNullOrEmpty(x.EndPointUrl)).ToList();

                    chkEndPointExecutionControl.DisplayMember = "Text";
                    chkEndPointExecutionControl.ValueMember = "Value";
                    foreach (var savedEndPoint in savedEndPoints)
                    {
                        chkEndPointExecutionControl.Items.Add(
                            new
                            {
                                Text = ((LocalEndPoint) savedEndPoint.EndPointId).DisplayName(),
                                Value = savedEndPoint.EndPointId
                            }, CheckState.Checked);
                    }
                }

                _threadPullDataAppointment = new Thread(PullAppointments);
                _threadPushDataBiometrics = new Thread(PushAppointments);
                _operationQueue.Add(new OperationQueue { Param = "Initialization for Pull/Push for 3rd Party Data Complete" });

                _operationQueue.Add(new OperationQueue { Param = "All Done" });
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue { Param = ex.Message });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id, true);
            }
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                _selfServer.CloseAsync();
                
                _onDemandSyncEnabled = false;
                _killCommandReceived = true;

                bgwNewPatient.CancelAsync();
                bgwUpdatePatient.CancelAsync();
                bgwSelfServer.CancelAsync();
                bgwContentManager.CancelAsync();
                bgwPing.CancelAsync();

                this.Close();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);

                this.Close();
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            _killCommandReceived = false;

            SiteInfo();
            InitiateSync();
            PushPullOperations();
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

        private void btnCancelSync_Click(object sender, EventArgs e)
        {
            try
            {
                _killCommandReceived = true;

                bgwNewPatient.CancelAsync();
                bgwUpdatePatient.CancelAsync();

                _isSyncNewInProgress = _isSyncUpdateInProgress = false;

                _subInfoMan.UpdatePersistedData();
                _subPopReg.UpdatePersistedData();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnPopulationStatusRegister_Click(object sender, EventArgs e)
        {
            try
            {
                _subPopReg.Show();
                _subPopReg.BringToFront();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void btnAdministratorSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var adminSettings = new SubAdminSettings(_administrationStaffInformation);
                adminSettings.ShowDialog();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void btnClearDump_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog.ShowDialog();

                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                    return;

                File.WriteAllText(Path.Combine(folderBrowserDialog.SelectedPath, $"{DateTime.Now:yyyyMMddHHmmssfff}"),
                    lstBoxInfoLog.Text);

                lstBoxInfoLog.Text = string.Empty;
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

        private void tmrSync_Tick(object sender, EventArgs e)
        {
            ExecuteSync();

            PushPullOperations();
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
                    lstBoxInfoLog.AppendText($"{eventItem.TimeStamp:yyyyMMddHHmmss}: {eventItem.Param}\n");
                    lstBoxInfoLog.ScrollToCaret();
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

                picDataReady.Visible = !_subInfoMan?._isBusy ?? false;
                picDataWait.Visible = _subInfoMan?._isBusy ?? false;

                listOfEvents.Clear();
                
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void tmrEndPointExecutionEffect_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_endPointId == 0)
                    return;

                tmrEndPointExecutionEffect.Enabled = false;

                var indexOfItem = chkEndPointExecutionControl.Items.IndexOf(new
                {
                    Text = ((LocalEndPoint)_endPointId).DisplayName(),
                    Value = _endPointId
                });

                chkEndPointExecutionControl.SetItemCheckState(indexOfItem, _checkState);
                _endPointId = 0;

                tmrEndPointExecutionEffect.Enabled = true;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
            }
        }

        private void tmrCheckConnection_Tick(object sender, EventArgs e)
        {
            if (!bgwPing.IsBusy)
                bgwPing.RunWorkerAsync();
        }

        private void tmrLaunchUpdate_Tick(object sender, EventArgs e)
        {
            if (_process.HasExited)
            {
                _operationQueue.Add(new OperationQueue { Param = "Download of New Update is complete." });
                picIndUpdate.Visible = false;
                tmrLaunchUpdate.Enabled = false;
            }
        }

        private void tmrFreshPatient_Tick(object sender, EventArgs e)
        {
            if (!bgwFreshPatient.IsBusy)
                bgwFreshPatient.RunWorkerAsync();
        }

        private void bgwNewPatient_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_isSyncNewInProgress)
                    return;

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Initiating Synchronization Task: New Entries from Partner Sites"
                });

                var pulled = 0;
                var blankResult = 1;

                while (true)
                {
                    if (!CheckConnection())
                        break;

                    using (var innerEntity = new LocalPDAEntities())
                    {
                        if (_killCommandReceived)
                        {
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = "STOP Command Received"
                            });
                            break;
                        }

                        var listOfPepId = "";

                        if (innerEntity.System_BioDataStore.Any())
                        {
                            var patientInformation = innerEntity.System_BioDataStore.Select(x => x.PepId).ToList();

                            listOfPepId = patientInformation.OrderBy(x => x).Aggregate("",
                                (current, patientInfo) => current + $"{patientInfo},");
                            listOfPepId = listOfPepId.Substring(0, listOfPepId.Length - 1);

                            patientInformation.Clear();
                            GC.Collect();
                        }

                        var data = Encoding.ASCII.GetBytes(listOfPepId);

                        var payLoad = new
                        {
                            encodedListOfAvailablePepId = Convert.ToBase64String(data),
                            siteId = _systemSiteData.RemoteSiteId
                        };

                        var responseData = LocalCore.Post(@"/ClientCommunication/Sync/PullNew",
                            JsonConvert.SerializeObject(payLoad));

                        if (!responseData.Status)
                        {
                            if (blankResult == 5)
                            {
                                _operationQueue.Add(new OperationQueue
                                {
                                    Param = "Processing"
                                });
                                blankResult = 1;
                            }

                            blankResult++;
                            continue;
                        }

                        _isSyncNewInProgress = true;

                        _operationQueue.Add(new OperationQueue
                        {
                            Param = responseData.Message
                        });

                        var returnedPatients =
                            JsonConvert.DeserializeObject<List<PatientInformation>>(
                                JsonConvert.SerializeObject(responseData.Data));

                        if (returnedPatients.Count == 0)
                        {
                            if (!_onDemandSyncEnabled)
                                _operationQueue.Add(new OperationQueue
                                {
                                    Param = "No New Patients found this time. Please Try the Synchronization Later"
                                });

                            break;
                        }

                        pulled += returnedPatients.Count;

                        foreach (var patient in returnedPatients)
                        {
                            if (_killCommandReceived)
                            {
                                _operationQueue.Add(new OperationQueue
                                {
                                    Param = "STOP Command Received"
                                });
                                break;
                            }

                            SaveUpdate(patient);
                        }

                        if (_onDemandSyncEnabled)
                            _subInfoMan.UpdatePersistedData();
                        else
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = $"New Patient Pull in Progress ... Pulled {pulled} so far"
                            });

                        _isSyncNewInProgress = false;

                        if (returnedPatients.Count < 100)
                        {
                            break;
                        }
                    }
                }

                if (!_onDemandSyncEnabled)
                    _subInfoMan.UpdatePersistedData();

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Completing Synchronization Task: New Entries from Partner Sites"
                });
            }
            catch (InvalidOperationException)
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Please wait. System is stil loading. Try again shortly!"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _operationQueue.Add(new OperationQueue
                {
                    Param = "ERROR >> Please retry the last action"
                });
            }

            _isSyncNewInProgress = false;
        }

        private void bgwUpdatePatient_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Initiating Synchronization Task: Modified Entries from Partner Sites"
                });

                var innerEntity = new LocalPDAEntities();
                var totalCount = innerEntity.System_BioDataStore.Count();

                for (var i = 0; i < totalCount; i += 100)
                {
                    if (_killCommandReceived)
                    {
                        _operationQueue.Add(new OperationQueue
                        {
                            Param = "STOP Command Received"
                        });
                        break;
                    }

                    if (!CheckConnection())
                        break;

                    var patientsMatching =
                        innerEntity.System_BioDataStore.OrderBy(x => x.Id).Skip(i).Take(100).Select(
                            x => new PatientMatching { PepId = x.PepId, LastUpdate = x.LastUpdate }).ToList();

                    var responseData = LocalCore.Post(@"/ClientCommunication/Sync/CheckIfUpdated",
                        JsonConvert.SerializeObject(patientsMatching));

                    if (!responseData.Status)
                        continue;

                    _isSyncUpdateInProgress = true;
                    _operationQueue.Add(new OperationQueue
                    {
                        Param = responseData.Message
                    });

                    var returnedPatients =
                        JsonConvert.DeserializeObject<List<PatientInformation>>(
                            JsonConvert.SerializeObject(responseData.Data));

                    foreach (var patient in returnedPatients)
                    {
                        if (_killCommandReceived)
                        {
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = "STOP Command Received"
                            });
                            break;
                        }

                        SaveUpdate(patient);
                    }

                    if (_onDemandSyncEnabled)
                        _subInfoMan.UpdatePersistedData();
                    else _operationQueue.Add(new OperationQueue
                    {
                        Param = $"Existing Patient Pull in Progress ... Completed {i + patientsMatching.Count} of {totalCount}"
                    });

                    _isSyncUpdateInProgress = false;
                }

                if (!_onDemandSyncEnabled)
                    _subInfoMan.UpdatePersistedData();

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Completing Synchronization Task: Modified Entries from Partner Sites"
                });
            }
            catch (InvalidOperationException)
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Please wait. System is stil loading. Try again shortly!"
                });
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _operationQueue.Add(new OperationQueue
                {
                    Param = "ERROR >> Please retry the last action"
                });
            }

            _isSyncUpdateInProgress = false;
        }

        private void bgwNewPatient_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void bgwPing_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _pingResult = LocalCore
                    .Get(
                        $@"/ClientCommunication/Misc/Ping?clientId={AppSetting.ClientId.ToUpper()}&appVersion={
                                AppSetting.Version.Replace('.', '_')
                            }&currentUserId={_administrationStaffInformation.Id}").Result.Status;
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);
                _pingResult = false;
            }
        }

        private void bgwFreshPatient_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (var entities = new LocalPDAEntities())
                {
                    var patientData =
                        entities.System_BioDataStore.Where(x => !x.IsDeleted && !x.IsSync).ToList();

                    foreach (var patientDatum in patientData)
                    {
                        if (!CheckConnection())
                            break;

                        var result = LocalCore.Post(@"/ClientCommunication/Patient/PostPatient", patientDatum.PatientData);

                        if (result.Status)
                        {
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = $"Patient with Pep Id {patientDatum.PepId} was pushed Successfully"
                            });
                            patientDatum.IsSync = true;
                            entities.Entry(patientDatum).State = EntityState.Modified;
                            entities.SaveChanges();
                        }
                        else
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = "Synchronization Error: " + result.Message
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Synchronization Error: " + ex
                });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void bgwSelfServer_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var config = new HttpSelfHostConfiguration("http://localhost:8000");

                config.Routes.MapHttpRoute(
                    "API Default", "api/{controller}/{id}",
                    new { controller = "Home", id = RouteParameter.Optional });

                _selfServer = new HttpSelfHostServer(config);
                _selfServer.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Local Server Operation Encountered an Error. " + ex
                });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        private void bgwContentManager_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Initiating Synchronization Task: 3rd Party Data >> Appointment Data Push"
                });

                var directoryInfo = new DirectoryInfo(AppSetting.PathAppointmentDataIngress);

                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                var readyFiles = directoryInfo.EnumerateFiles().ToList();

                foreach (var readyFile in readyFiles)
                {
                    try
                    {
                        if (!CheckConnection())
                            break;

                        var fileData =
                            JsonConvert.DeserializeObject<List<IntegrationAppointmentDataIngress>>(
                                File.ReadAllText(readyFile.FullName));

                        var integrationAppointmentDataIngressPayLoad = new IntegrationAppointmentDataIngressPayLoad
                        {
                            SiteId = _systemSiteData.RemoteSiteId,
                            IntegrationAppointmentDataIngresses = fileData,
                            UserId = _administrationStaffInformation.Id,
                            ClientId = AppSetting.ClientId
                        };

                        var result = LocalCore.Post(@"/ClientCommunication/Sync/ClientPushAppointmentData",
                            JsonConvert.SerializeObject(integrationAppointmentDataIngressPayLoad));

                        if (result.Status)
                        {
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = $"File {readyFile.Name} has been uploaded Successfully"
                            });

                            File.Delete(readyFile.FullName);
                        }
                        else
                            _operationQueue.Add(new OperationQueue
                            {
                                Param = $"File {readyFile.Name} Failed this time. Reason: {result.Message}"
                            });
                    }
                    catch (Exception exception)
                    {
                        LocalCore.TreatError(exception, _administrationStaffInformation.Id);

                        _operationQueue.Add(new OperationQueue
                        {
                            Param = $"File {readyFile.Name} Failed this time. Reason: {exception}"
                        });
                    }
                }

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Completing Synchronization Task: 3rd Party Data >> Appointment Data Push"
                });
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Content Manager Encountered an Error. " + ex
                });
            }
        }

        private void bgwUpdatePatient_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnTogglePushPull_Click(object sender, EventArgs e)
        {
            grpSyncController.Visible = !grpSyncController.Visible;
        }


        private static int GenerateVersionCode(string version)
        {
            return Convert.ToInt32(version.Replace(".", "").Replace("_", "").Replace("v", "").Trim());
        }

        private void InitiateSync()
        {
            try
            {
                using (var entities = new LocalPDAEntities())
                {
                    _operationQueue.Add(new OperationQueue
                    {
                        Param = "Running Local Clean Up"
                    });

                    entities.Sp_System_CleanUp();
                }
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }

            try
            {
                _operationQueue.Add(new OperationQueue
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

            try
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Initiating 3rd Party Integrations"
                });

                if (!bgwContentManager.IsBusy)
                    bgwContentManager.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }

            CheckForUpdates();
        }

        private void ExecuteSync()
        {
            if (!_onDemandSyncEnabled)
                return;

            InitiateSync();
        }

        private void SaveUpdate(PatientInformation patientInformation)
        {
            try
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

                    existingPatient.PatientData = JsonConvert.SerializeObject(patientInformation);

                    localPdaEntitiesPatientUpdateSync.Entry(existingPatient).State = EntityState.Modified;
                    localPdaEntitiesPatientUpdateSync.SaveChanges();
                    _operationQueue.Add(new OperationQueue
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

                    newPatient.PatientData = JsonConvert.SerializeObject(patientInformation);

                    localPdaEntitiesPatientUpdateSync.System_BioDataStore.Add(newPatient);
                    localPdaEntitiesPatientUpdateSync.SaveChanges();

                    _operationQueue.Add(new OperationQueue
                    {
                        Param = "Added Patient with PEPID " + patientInformation.Patient_PatientInformation.PepId
                    });
                }
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, _administrationStaffInformation.Id);
            }
        }

        private void CheckForUpdates()
        {
            new Thread(() =>
            {
                try
                {
                    using (var entities = new LocalPDAEntities())
                    {
                        var responseData = LocalCore.Get(@"/ClientCommunication/Misc/GetUpdateData").Result;
                        if (!responseData.Status)
                            return;

                        if (responseData.Data == null)
                            return;

                        var update =
                            JsonConvert.DeserializeObject<System_Update>(
                                JsonConvert.SerializeObject(responseData.Data));

                        if (GenerateVersionCode(update.VersionNumber) <=
                            GenerateVersionCode(AppSetting.Version))
                        {
                            _operationQueue.Add(new OperationQueue { Param = $"Your Version {AppSetting.Version} is the latest" });
                            return;
                        }

                        _operationQueue.Add(new OperationQueue { Param = $"Detected Update {update.VersionNumber}" });

                        var startInfo = new ProcessStartInfo
                        {
                            FileName = "PatientDataAdministration.ClientUpdater.exe",
                            Arguments = entities.System_Setting.FirstOrDefault(
                                            x => x.SettingKey == (int)SyncMode.RemoteApi)?.SettingValue ?? "",
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

                        _operationQueue.Add(new OperationQueue { Param = $"Download of Update {update.VersionNumber} is running" });
                    }
                }
                catch (Exception ex)
                {
                    _operationQueue.Add(new OperationQueue { Param = ex.Message });
                    LocalCore.TreatError(ex, _administrationStaffInformation.Id, true);
                }
            }).Start();
        }

        public void SiteInfo()
        {
            _operationQueue.Add(new OperationQueue
            {
                Param = $"Current Site is {_systemSiteData.SiteNameOfficial}"
            });
        }

        public bool CheckConnection()
        {
            if (!_pingResult)
            {
                _operationQueue.Add(new OperationQueue
                {
                    Param = "Cannot Reach the Server at the moment. Please try again later"
                });
            }

            return _pingResult;
        }

        private void PushPullOperations()
        {
            try
            {
                if (_threadPushDataBiometrics.ThreadState != ThreadState.Running)
                    _threadPushDataBiometrics.Start();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);

                _threadPushDataBiometrics = new Thread(PushAppointments);
                _threadPushDataBiometrics.Start();
            }


            try
            {
                if (_threadPullDataAppointment.ThreadState != ThreadState.Running)
                    _threadPullDataAppointment.Start();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, _administrationStaffInformation.Id);

                _threadPullDataAppointment = new Thread(PullAppointments);
                _threadPullDataAppointment.Start();
            }
        }

        /// <summary>
        /// Pull Data from End Points on Network
        /// </summary>
        public void PullAppointments()
        {
            try
            {
                using (var entities = new LocalPDAEntities())
                {
                    var endPoints = EnumDictionary.GetList<LocalEndPoint>()
                        .Where(y => y.ItemName.ToLower().Contains("pull api")).Select(y => y.ItemId).ToList();

                    var appointmentDataEndPoints =
                        entities.System_EndPointLog.Where(x => endPoints.Contains(x.EndPointId)).ToList();

                    foreach (var appointmentDataEndPoint in appointmentDataEndPoints)
                    {
                        if (!CheckIfEndPointEnabled(appointmentDataEndPoint.EndPointId))
                            continue;

                        if (string.IsNullOrEmpty(appointmentDataEndPoint.EndPointUrl))
                            continue;

                        var appointmentDataResponse = LocalCore.GetLocal(appointmentDataEndPoint.EndPointUrl);

                        if (appointmentDataResponse.Result == null)
                            continue;

                        var appointmentData = JsonConvert.DeserializeObject<List<IntegrationAppointmentDataIngress>>(
                            JsonConvert.SerializeObject(appointmentDataResponse.Result));

                        if (appointmentData.Any())
                            _operationQueue.Add(new OperationQueue
                            {
                                Param =
                                    $"Initiating Synchronization Task: 3rd Party Data >> Appointment Data Pull from {((LocalEndPoint) appointmentDataEndPoint.EndPointId).DisplayName()}"
                            });

                        var basePath = AppSetting.PathAppointmentDataIngress;

                        if (!Directory.Exists(basePath))
                            Directory.CreateDirectory(basePath);

                        long size = 0;
                        foreach (var chunk in Transforms.ListChunk(appointmentData, 100))
                        {
                            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pda";

                            File.WriteAllText(Path.Combine(basePath, fileName),
                                JsonConvert.SerializeObject(chunk));

                            size += new FileInfo(Path.Combine(basePath, fileName)).Length;
                        }

                        _operationQueue.Add(new OperationQueue
                        {
                            Param =
                                $"Completing Synchronization Task: 3rd Party Data >> Appointment Data Pull from {((LocalEndPoint)appointmentDataEndPoint.EndPointId).DisplayName()}. Total Size {size:#,##0} bytes"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _operationQueue.Add(new OperationQueue { Param = ex.Message });
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);
            }
        }

        /// <summary>
        /// Push Data to End Points on the Network
        /// </summary>
        public void PushAppointments()
        {
            try
            {
                using (var innerEntity = new LocalPDAEntities())
                {
                    var endPoints =
                    EnumDictionary.GetList<LocalEndPoint>().Where(y => y.ItemName.ToLower().Contains("push api"))
                        .Select(y => y.ItemId).ToList();

                    var clinicalDataEndPoints =
                        innerEntity.System_EndPointLog.Where(x => endPoints
                                .Contains(x.EndPointId)).ToList();

                    if (!clinicalDataEndPoints.Any())
                        return;

                    var pendingPush = innerEntity.System_BioDataStore
                        .Where(x => !x.IsLocalPush && !x.IsDeleted && !string.IsNullOrEmpty(x.PrimaryFinger) &&
                                    !string.IsNullOrEmpty(x.SecondaryFinger))
                        .Take(100)
                        .ToList();

                    while (innerEntity.System_BioDataStore.Any(x =>
                        !x.IsLocalPush && !x.IsDeleted && !string.IsNullOrEmpty(x.PrimaryFinger) &&
                        !string.IsNullOrEmpty(x.SecondaryFinger)))
                    {
                        foreach (var pending in pendingPush)
                        {
                            foreach (var clinicalDataEndPoint in clinicalDataEndPoints)
                            {
                                if (!CheckIfEndPointEnabled(clinicalDataEndPoint.EndPointId))
                                    continue;

                                if (string.IsNullOrEmpty(clinicalDataEndPoint.EndPointUrl))
                                    continue;

                                if (pendingPush.Any())
                                    _operationQueue.Add(new OperationQueue
                                    {
                                        Param =
                                            $"Local Data Push: Processing {pendingPush.Count} records at this time to {clinicalDataEndPoint.EndPointUrl} for {((LocalEndPoint) clinicalDataEndPoint.EndPointId).DisplayName()}."
                                    });

                                pending.IsLocalPush = LocalCore.PostLocal(clinicalDataEndPoint.EndPointUrl,
                                    JsonConvert.SerializeObject(new[]
                                    {
                                        new
                                        {
                                            pepId = pending.PepId,
                                            pda1 = pending.PrimaryFinger,
                                            pda2 = pending.SecondaryFinger,
                                            mobile = "",
                                            address = ""
                                        }
                                    }));

                                if (pending.IsLocalPush)
                                    continue;

                                var message =
                                    $"Failed to Push Data to {clinicalDataEndPoint.EndPointUrl} for {((LocalEndPoint)clinicalDataEndPoint.EndPointId).DisplayName()}. ";
                                _operationQueue.Add(new OperationQueue
                                {
                                    Param = message.Trim()
                                });

                                var dialog = MessageBox.Show(
                                    message +
                                    @"Do you wish to STOP pushing data so that you can either investigate or contact Support?",
                                    @" Please Decide", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                                if (dialog == DialogResult.Yes)
                                {
                                    _endPointId = clinicalDataEndPoint.EndPointId;
                                    _checkState = CheckState.Unchecked;
                                    return;
                                }
                            }

                            if (!pending.IsLocalPush)
                                continue;

                            innerEntity.Entry(pending).State = EntityState.Modified;
                            innerEntity.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCore.TreatError(ex, _administrationStaffInformation.Id);

                _operationQueue.Add(new OperationQueue
                {
                    Param = "Local Data Push Encountered an Error. " + ex
                });
            }
        }

        private bool CheckIfEndPointEnabled(int endPointId)
        {
            var indexOfItem = chkEndPointExecutionControl.Items.IndexOf(new
            {
                Text = ((LocalEndPoint) endPointId).DisplayName(),
                Value = endPointId
            });

            return chkEndPointExecutionControl.GetItemCheckState(indexOfItem) == CheckState.Checked;
        }

        private void btnForceUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
    }
}
