using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDataAdministration.Client
{
    public partial class SubSettings : MetroFramework.Forms.MetroForm
    {
        private readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

        public SubSettings()
        {
            InitializeComponent();
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

                var currentRemoteApiSetting = _localPdaEntities.System_Setting.FirstOrDefault(x => x.SettingKey == (int)EnumLibrary.SettingKey.RemoteApi);
                if (currentRemoteApiSetting == null)
                {
                    _localPdaEntities.System_Setting.Add(new System_Setting()
                    {
                        IsDeleted = false,
                        SettingKey = (int)EnumLibrary.SettingKey.RemoteApi,
                        DateReceived = DateTime.Now,
                        SettingValue = string.Empty
                    });
                    _localPdaEntities.SaveChanges();
                    currentRemoteApiSetting = _localPdaEntities.System_Setting.FirstOrDefault(x => x.SettingKey == (int)EnumLibrary.SettingKey.RemoteApi);
                }

                currentRemoteApiSetting.SettingValue = txtRemoteApi.Text;
                _localPdaEntities.Entry(currentRemoteApiSetting).State = EntityState.Modified;
                _localPdaEntities.SaveChanges();

                lblInformation.Text = @"Connection Settings Update Successful";

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
                    _localPdaEntities.System_Setting.FirstOrDefault(
                        x => x.SettingKey == (int) EnumLibrary.SettingKey.RemoteApi)?.SettingValue;

                lblInformation.Text = @"Done Loading Settings";
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }
    }
}
