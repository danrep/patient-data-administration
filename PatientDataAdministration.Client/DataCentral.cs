using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client
{
    public partial class DataCentral : MetroFramework.Forms.MetroForm
    {
        private readonly UserCredential _userCredential;
        private bool _showDownloadIcon;
        private readonly Process _process = new Process();

        private static int GenerateVersionCode(string version)
        {
            return Convert.ToInt32(version.Replace(".", "").Replace("v", "").Trim());
        }

        public DataCentral(UserCredential userCredential)
        {
            InitializeComponent();
            this._userCredential = userCredential;

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

                _showDownloadIcon = true;

                var startInfo = new ProcessStartInfo
                {
                    FileName = "PatientDataAdministration.ClientUpdater.exe",
                    Arguments = LocalCore.BaseUrl,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                _process.StartInfo = startInfo;
                _process.EnableRaisingEvents = true;
                _process.Start();

                while (!_process.HasExited)
                {
                    //
                }

                _showDownloadIcon = false;
            }).Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            CollectGarbage();
        }

        private void DataCentral_Load(object sender, EventArgs e)
        {
            lblUserInformation.Text = @"Welcome, " +
                                      _userCredential.AdministrationStaffInformation.Surname + @" " +
                                      _userCredential.AdministrationStaffInformation.FirstName + @". You are attached to " + 
                                      _userCredential.AdministrationSiteInformation.SiteNameOfficial;
        }

        private void btnPatientManagement_Click(object sender, EventArgs e)
        {
            var subInfoMan = new SubInformationManagement(_userCredential);
            subInfoMan.ShowDialog();
            CollectGarbage();
        }

        private void CollectGarbage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            //
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            picIndUpdate.Visible = _showDownloadIcon;
        }
    }
}
