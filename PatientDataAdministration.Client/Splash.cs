using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using PatientDataAdministration.Data.InterchangeModels;

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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

               
                if (!_finalControl)
                {
                    CheckForDownloadedUpdates();

                    this.Hide();
                    var auth = new Authentication();
                    auth.ShowDialog();
                    _finalControl = true;
                    this.Show();
                }
                else this.Close();

                timer1.Enabled = true;

                ShowInfo("Shutting Down...");
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
                    return;

                ShowInfo("Waiting for Confirmation");
                var response = System.Windows.Forms.MessageBox.Show(@"Waiting for Confirmation",
                    @"An Update is ready. Do you want to apply it?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (response != DialogResult.Yes)
                    return;

                Process.Start("PatientDataAdministration.ClientUpdater.exe", "U");
                this.Close();
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
    }
}
