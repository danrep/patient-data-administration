using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using PatientDataAdministration.Data;

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
            ShowInfo("Starting Up");
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = "/c sqllocaldb start mssqllocaldb",
                    CreateNoWindow = true, 
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            process.Start();
            while (!process.HasExited) { }
            ShowInfo("Initialization Complete");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
               
                if (!_finalControl)
                    CheckForDownloadedUpdates();
                else this.Close();
            }
            catch 
            {
                // Safety Measure for Graceful Closure
            }
        }

        private void Splash_Shown(object sender, EventArgs e)
        {
            //
        }

        private void Splash_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception exception)
            {
                LocalCore.TreatError(exception, 0);
                GC.Collect();
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
                Console.WriteLine(exception);
                throw;
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
            pnlUpdate.Visible = false;

            this.Hide();
            var auth = new Authentication();
            auth.ShowDialog();
            _finalControl = true;
            this.Show();

            timer1.Enabled = true;
            ShowInfo("Shutting Down...");
        }
    }
}
