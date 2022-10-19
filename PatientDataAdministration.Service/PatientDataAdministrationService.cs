using PatientDataAdministration.Core;
using PatientDataAdministration.Service.Engines;
using PatientDataAdministration.Service.Engines.EngineReporting;
using System;
using System.ServiceProcess;

namespace PatientDataAdministration.Service
{
    public partial class PatientDataAdministrationService : ServiceBase
    {
        System.Timers.Timer _timer;
        static Cron _cron;

        public PatientDataAdministrationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ActivityLogger.LogFileName = "PDA_Service_Logs.txt";

                ActivityLogger.Log("INFO", "Starting Up PBS Service");

                _timer = new System.Timers.Timer(20000)
                {
                    Enabled = true
                };
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Data Integrity Engine

            try
            {
                //EngineReporting.PatientSecondaryBioDataDuplicationReport();

                //if (!EngineDuplicateBioData.IsProcessing)
                //{
                //    ActivityLogger.Log("INFO", "Starting Up Primary Dedup Engine");
                //    TaskManagerEngineDuplicateBioData()?.ThreadEngine.Start();
                //}

                //if (!EngineDuplicateBioDataSecondary.IsProcessing)
                //{
                //    ActivityLogger.Log("INFO", "Starting Up Secondary Dedup Engine");
                //    TaskManagerEngineDuplicateBioDataSecondary()?.ThreadEngine.Start();
                //}

                _cron = new Cron();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion
        }

        protected override void OnStop()
        {
            try
            {
                _timer.Stop();
                _timer.Enabled = false;
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }
    }
}
