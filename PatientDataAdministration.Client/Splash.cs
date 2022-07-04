using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using PatientDataAdministration.Client.LocalSettingStorage;
using PatientDataAdministration.Data;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PatientDataAdministration.Client
{
    public partial class Splash : MetroFramework.Forms.MetroForm
    {
        private bool _finalControl;

        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            label1.Text = ConfigurationManager.AppSettings["appVersion"].ToString();
        }

        private void Splash_Shown(object sender, EventArgs e)
        {
            ShowInfo("Starting Up");

            processDBCheck.StartInfo = new ProcessStartInfo("powershell.exe", "sqllocaldb start mssqllocaldb") { 
                CreateNoWindow = true, 
                WindowStyle = ProcessWindowStyle.Hidden
            };
            processDBCheck.Start();

            while (!processDBCheck.HasExited)
            {
                //
            }

            ShowInfo("Initialization Complete");
        }

        private void Splash_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                var processes =
                    Process.GetProcesses().Where(x => x.ProcessName.Contains("PatientDataAdministration")).ToList();

                foreach (var proc in processes)
                    proc.Kill();
            }
            catch
            {
                //
            }
            finally
            {
                System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void StartUp()
        {
            GetClientId();
            InitializeDataStore();
            RunDataBaseUpdate();
            CheckForDownloadedUpdates();
        }

        private void InitializeDataStore()
        {
            try
            {
                LocalCore.GetLocalDb("LOCALPDA");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error occurred during Initialization: " + e.Message);
                this.Close();
            }
        }

        private void RunDataBaseUpdate()
        {
            try
            {
                ShowInfo("Checking for Database Schema");

                var storeLocation = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DownloadedUpdate");
                var databaseScriptFile = $@"{storeLocation}/database.sql";

                if (!File.Exists(databaseScriptFile))
                {
                    databaseScriptFile = $@"{System.AppDomain.CurrentDomain.BaseDirectory}/database.sql";

                    if (!File.Exists(databaseScriptFile))
                    {
                        ShowInfo("Schema up to Date");
                        return;
                    }
                }

                var databaseScriptSessions = File.ReadAllText(databaseScriptFile).Split('~');
                foreach (var databaseScriptSession in databaseScriptSessions)
                    LocalCore.RunDatabaseSript("LOCALPDA", databaseScriptSession);

                ShowInfo("Schema updated Succesfully");

                File.Delete(databaseScriptFile);
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }

        private void CheckForDownloadedUpdates()
        {
            try
            {
                ShowInfo("Checking for Updates");
                var storeLocation = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DownloadedUpdate");
                var updateInfo = $@"{storeLocation}/data.txt";

                if (!File.Exists(updateInfo))
                {
                    ShowInfo("No New Updates. Proceeding to Start Program");
                    OpenApp();
                    return;
                }

                var updateData = File.ReadAllText(updateInfo);
                var updateDataResolved = Newtonsoft.Json.JsonConvert.DeserializeObject<System_Update>(updateData);

                ShowInfo("Update Found");

                lblUpdateInfoHead.Text = @"New Update!";
                lblUpdateInfo.Text = $@"An Update was released on {updateDataResolved.DateProvided.ToLongDateString()}. The Version Number is {updateDataResolved.VersionNumber}. ";
                lblUpdateInfo.Text += $@"Do you want to apply it?";

                pnlUpdate.Visible = true;
                ShowInfo("Waiting for Confirmation");
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
            }
        }

        private void ShowInfo(string text)
        {
            label2.Text = text;
            System.Windows.Forms.Application.DoEvents();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Process.Start("PatientDataAdministration.ClientUpdater.exe", "U");
            this.Close();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            OpenApp();
        }

        private void OpenApp()
        {
            try
            {
                pnlUpdate.Visible = false;

                LocalCache.RefreshCache("System_Setting");

                this.Hide();
                var auth = new Authentication();
                auth.ShowDialog();
                _finalControl = true;
                this.Show();

                tmrStartUp.Enabled = true;
                ShowInfo("Shutting Down...");
            }
            catch (Exception e)
            {
                LocalCore.TreatError(e, 0, true);
                this.Close();
            }
        }

        private void tmrStartUp_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrStartUp.Enabled = false;

                if (!_finalControl)
                    StartUp();
                else
                    this.Close();
            }
            catch
            {
                // Safety Measure for Graceful Closure
            }
        }

        private void GetClientId()
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"]?.ToString() ?? "";
            if (string.IsNullOrEmpty(clientId))
            {
                var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                clientId = Guid.NewGuid().ToString();
                configuration.AppSettings.Settings.Add("ClientId", clientId);
                configuration.Save(ConfigurationSaveMode.Modified, false);
                ConfigurationManager.RefreshSection("appSettings");
            }

            LocalCache.Set("ClientId", clientId);
            return;
        }
    }
}
