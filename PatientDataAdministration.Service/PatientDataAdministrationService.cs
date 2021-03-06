﻿using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Service.Engines.EngineDataIntegrity;
using System;
using System.ServiceProcess;
using System.Threading;

namespace PatientDataAdministration.Service
{
    public partial class PatientDataAdministrationService : ServiceBase
    {
        System.Timers.Timer _timer;
        private TaskManager _taskManagerEngineDuplicateBioData;

        public PatientDataAdministrationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _timer = new System.Timers.Timer(10000)
                {
                    Enabled = true
                };
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();

                ActivityLogger.LogFileName = "PDA_Service_Logs.txt";
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
                if (!EngineDuplicateBioData.IsProcessing)
                    TaskManagerEngineDuplicateBioData()?.ThreadEngine.Start();

                if (!EngineDuplicateBioDataSecondary.IsProcessing)
                    TaskManagerEngineDuplicateBioDataSecondary()?.ThreadEngine.Start();
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

                _taskManagerEngineDuplicateBioData.ThreadEngine.Abort();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        private static TaskManager TaskManagerEngineDuplicateBioData()
        {
            return null;

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
