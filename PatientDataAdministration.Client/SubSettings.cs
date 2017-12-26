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
                // TODO: This line of code loads data into the 'localPDADataSet.System_SiteData' table. You can move, or remove it, as needed.
                this.system_SiteDataTableAdapter.Fill(this.localPDADataSet.System_SiteData);
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                lblInformation.Text = @"An Error Occured. Please Close this Window and Start Again";
            }
        }
    }
}
