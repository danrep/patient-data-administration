using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PatientDataAdministration.Client
{
    public partial class SubSettings : MetroFramework.Forms.MetroForm
    {
        private LocalPDAEntities _localPdaEntities = new LocalPDAEntities();
        private List<System_Setting> _systemSettings;

        public SubSettings()
        {
            InitializeComponent();
            _systemSettings = LocalCache.Get<List<System_Setting>>("System_Setting");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.Close();
        }

        private void btnSaveConnections_Click(object sender, EventArgs e)
        {
            try
            {
                #region Connection Setting
                
                var currentRemoteApiSetting =
                    _systemSettings.FirstOrDefault(x => x.SettingKey == (int) EnumLibrary.SettingKey.RemoteApi);

                if (currentRemoteApiSetting == null)
                {
                    _localPdaEntities.System_Setting.Add(new System_Setting()
                    {
                        IsDeleted = false,
                        SettingKey = (int)EnumLibrary.SettingKey.RemoteApi,
                        DateReceived = DateTime.Now,
                        SettingValue = string.Empty
                    });
                }
                else
                {
                    currentRemoteApiSetting.SettingValue = txtRemoteApi.Text;
                    _localPdaEntities.Entry(currentRemoteApiSetting).State = EntityState.Modified;
                }
                _localPdaEntities.SaveChanges();

                var currentOnDemandSync = _systemSettings.FirstOrDefault(x => x.SettingKey == (int)EnumLibrary.SettingKey.OnDemandSync);
                if (currentOnDemandSync == null)
                {
                    _localPdaEntities.System_Setting.Add(new System_Setting()
                    {
                        IsDeleted = false,
                        SettingKey = (int)EnumLibrary.SettingKey.RemoteApi,
                        DateReceived = DateTime.Now,
                        SettingValue = chkCurrentOnDemandSync.Checked.ToString()
                    });
                }
                else
                {
                    currentOnDemandSync.SettingValue = chkCurrentOnDemandSync.Checked.ToString();
                    _localPdaEntities.Entry(currentOnDemandSync).State = EntityState.Modified;
                }
                _localPdaEntities.SaveChanges();

                LocalCache.RefreshCache("System_Setting");
                lblInformation.Text = @"Connection Settings Update Successful";
                System.Media.SystemSounds.Exclamation.Play();

                #endregion
            }
            catch (Exception exception)
            {
                lblInformation.Text = exception.Message;
                LocalCore.TreatError(exception, 0, true);
            }
        }

        private void SubSettings_Shown(object sender, EventArgs e)
        {
            try
            {
                lblInformation.Text = @"Loading Settings. Please Wait.";

                Application.DoEvents();

                txtRemoteApi.Text =
                    _systemSettings.FirstOrDefault(
                        x => x.SettingKey == (int)EnumLibrary.SettingKey.RemoteApi)?.SettingValue ?? "No URL Set";

                chkCurrentOnDemandSync.Checked = Convert.ToBoolean(_systemSettings.FirstOrDefault(
                                                                           x => x.SettingKey ==
                                                                               (int) EnumLibrary.SettingKey.OnDemandSync)
                                                                       ?.SettingValue ?? "false");

                lblInformation.Text = @"Done Loading Settings";
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                lblInformation.Text = @"An Error Occured. Please Close this Window and Start Again";
            }
        }

        private void SubSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Thread(() =>
            {
                LocalCache.RefreshCache("System_Setting");
            }).Start();
        }

        private void SubSettings_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'localPDADataSet.System_State' table. You can move, or remove it, as needed.
                this.system_StateTableAdapter.Fill(this.localPDADataSet.System_State);
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                lblInformation.Text = @"An Error Occured. Please Close this Window and Start Again";
            }
        }

        private void btnSaveSite_Click(object sender, EventArgs e)
        {
            try
            {
                lblInformation.Text = @"Saving Settings. Please Wait.";

                var previous =
                    _localPdaEntities.System_SiteData.FirstOrDefault(x => x.IsCurrent);

                if (previous != null)
                {
                    previous.IsCurrent = false;
                    _localPdaEntities.Entry(previous).State = EntityState.Modified;
                    _localPdaEntities.SaveChanges();
                }

                var current =
                    _localPdaEntities.System_SiteData.FirstOrDefault(x => x.Id == (int)ddlSite.SelectedValue);

                if (current != null)
                {
                    current.IsCurrent = true;
                    _localPdaEntities.Entry(current).State = EntityState.Modified;
                    _localPdaEntities.SaveChanges();
                }
                else MessageBox.Show("");

                lblInformation.Text = @"Done Saving Settings";
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                lblInformation.Text = @"An Error Occured. Please Close this Window and Start Again";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                lblInformation.Text = $@"Connecting to Server. Please wait ...";
                Application.DoEvents();

                var result = LocalCore.Get($@"/ClientCommunication/Sync/GetSiteData");
                if (result.Result.Status)
                {
                    if (result.Result.Data != null)
                    {
                        var siteData =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data.Administration_SiteInformation>>(
                                Newtonsoft.Json.JsonConvert.SerializeObject(result.Result.Data));

                        lblInformation.Text = $@"Synchronizing {siteData.Count} Sites. Please wait ...";
                        Application.DoEvents();
                        var i = 1;
                        foreach (var siteDatum in siteData)
                        {
                            if (_localPdaEntities.System_SiteData.Any(x => x.RemoteSiteId == siteDatum.Id))
                            {
                                var existing =
                                    _localPdaEntities.System_SiteData.FirstOrDefault(x => x.RemoteSiteId == siteDatum.Id);

                                if (existing == null)
                                    continue;

                                if (existing.LastUpdate >= siteDatum.LastUpdate)
                                    continue;

                                existing.SiteNameOfficial = siteDatum.SiteNameOfficial;
                                existing.SiteCode = siteDatum.SiteCode;
                                existing.SiteNameInformal = siteDatum.SiteNameInformal;
                                existing.StateId = siteDatum.StateId;
                                existing.LastUpdate = siteDatum.LastUpdate;

                                _localPdaEntities.Entry(existing).State = EntityState.Modified;
                                _localPdaEntities.SaveChanges();
                            }
                            else
                            {
                                _localPdaEntities.System_SiteData.Add(new System_SiteData()
                                {
                                    SiteNameOfficial = siteDatum.SiteNameOfficial, 
                                    LastUpdate = siteDatum.LastUpdate, 
                                    RemoteSiteId = siteDatum.Id,
                                    SiteCode = siteDatum.SiteCode, 
                                    SiteNameInformal = siteDatum.SiteNameInformal,
                                    StateId = siteDatum.StateId
                                });
                                _localPdaEntities.SaveChanges();
                            }

                            i++;

                            lblInformation.Text = $@"Processed {i} of {siteData.Count} Sites. Still Working ...";
                            Application.DoEvents();
                        }

                        lblInformation.Text = $@"All Done.";
                        Application.DoEvents();
                    }
                    else
                    {
                        MessageBox.Show(result.Result.Message);
                    }
                }
                else
                {
                    MessageBox.Show(result.Result.Message);
                }

                LoadSites();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                lblInformation.Text = @"An Error Occured. Please Close this Window and Start Again";
            }
        }

        private void ddlState_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadSites();
        }

        private void LoadSites()
        {
            try
            {
                // TODO: This line of code loads data into the 'localPDADataSet.System_SiteData' table. You can move, or remove it, as needed.
                this.system_SiteDataTableAdapter.FillByState(this.localPDADataSet.System_SiteData,
                    (int)ddlState.SelectedValue);
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }
    }
}
