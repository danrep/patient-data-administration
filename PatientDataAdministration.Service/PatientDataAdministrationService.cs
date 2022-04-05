using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Service.Engines;
using PatientDataAdministration.Service.Engines.EngineDataIntegrity;
using PatientDataAdministration.Service.Engines.EngineReporting;
using System;
using System.ServiceProcess;
using System.Threading;

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
                EngineReporting.PatientSecondaryBioDataDuplicationReport();

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

                EngineDuplicateBioData.KillProcessing();
                EngineDuplicateBioDataSecondary.KillProcessing();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        private static TaskManager TaskManagerEngineDuplicateBioData()
        {
            if (EngineDuplicateBioData.IsProcessing)
                return null;

            return new TaskManager()
            {
                ThreadEngine = new Thread(EngineDuplicateBioData.ProcessDataIntegrityBiometric)
            };
        }

        private static TaskManager TaskManagerEngineDuplicateBioDataSecondary()
        {
            if (EngineDuplicateBioDataSecondary.IsProcessing)
                return null;

            return new TaskManager()
            {
                ThreadEngine = new Thread(EngineDuplicateBioDataSecondary.ProcessDataIntegrityBiometric)
            };
        }
    }
}
