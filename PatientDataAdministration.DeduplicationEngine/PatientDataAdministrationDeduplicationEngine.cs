using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.DeduplicationEngine.Engines.EngineDataIntegrity;
using PatientDataAdministration.DeduplicationEngine.Engines.FileOperations;
using PatientDataAdministration.EnumLibrary.Dictionary;
using System;
using System.ServiceProcess;
using System.Threading;

namespace PatientDataAdministration.DeduplicationEngine
{
    public partial class PatientDataAdministrationDeduplicationEngine : ServiceBase
    {
        System.Timers.Timer _nightly;

        public PatientDataAdministrationDeduplicationEngine()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ActivityLogger.LogFileName = "PDA_DeduplicationEngine_Logs.txt";

                ActivityLogger.Log("INFO", "Starting Up PBS DeduplicationEngine");

                _nightly = new System.Timers.Timer(60000)
                {
                    Enabled = true
                };
                _nightly.Elapsed += Nightly_Elapsed;
                _nightly.Start();

                LoadMessageListeners();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _nightly.Stop();
                _nightly.Enabled = false;

                EngineDuplicateBioData.KillProcessing();
                EngineDuplicateBioDataSecondary.KillProcessing();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

        }

        private static void Nightly_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Data Integrity Engine

            if (DateTime.Now.Hour != 23)
                return;

            try
            {
                if (!EngineDuplicateBioData.IsProcessing)
                {
                    ActivityLogger.Log("INFO", "Starting Up Primary Dedup Engine");
                    TaskManagerEngineDuplicateBioData()?.ThreadEngine.Start();
                }

                if (!EngineDuplicateBioDataSecondary.IsProcessing)
                {
                    ActivityLogger.Log("INFO", "Starting Up Secondary Dedup Engine");
                    TaskManagerEngineDuplicateBioDataSecondary()?.ThreadEngine.Start();
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion
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

        private static void LoadMessageListeners()
        {
            try
            {
                Core.PubSub.Redis.Operations.Subscribe(EnumLibrary.PubSubAction.ProcessSecondaryDataUploadedFile.NormalizeDisplayName(), ProcessFile.ProcessSecondaryData);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}
