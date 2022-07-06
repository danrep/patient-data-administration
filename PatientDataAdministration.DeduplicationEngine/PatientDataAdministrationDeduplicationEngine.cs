using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.DeduplicationEngine.Engines.EngineDataIntegrity;
using PatientDataAdministration.DeduplicationEngine.Engines.EngineDataValidation;
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
        System.Timers.Timer _dusk;

        static EngineDuplicateBioDataInstant _instant;

        static Thread _bioDataValidation;

        public PatientDataAdministrationDeduplicationEngine()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Thread.Sleep(10000);

                ActivityLogger.LogFileName = "PDA_DeduplicationEngine_Logs.txt";

                ActivityLogger.Log("INFO", "Starting Up PBS DeduplicationEngine Services");

                _nightly = new System.Timers.Timer(60000)
                {
                    Enabled = true
                };
                _nightly.Elapsed += Nightly_Elapsed;
                _nightly.Start();
                ActivityLogger.Log("INFO", "Started Nightly Services");

                _dusk = new System.Timers.Timer(60000)
                {
                    Enabled = true
                };
                _dusk.Elapsed += Dusk_Elapsed;
                _dusk.Start();
                ActivityLogger.Log("INFO", "Started Dusk Services");

                _bioDataValidation = new Thread(() => {
                    using (var engineValidation = new EngineSecondaryValidation())
                    {
                        ActivityLogger.Log("INFO", "Starting Secondary Validation Engine");
                        engineValidation.Execute();
                        ActivityLogger.Log("INFO", "Completing Secondary Validation Engine");
                    }
                });
                _bioDataValidation.Start();

                LoadMessageListeners();
                ActivityLogger.Log("INFO", "Started Message Listener Services");
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

                _dusk.Stop();
                _dusk.Enabled = false;

                EngineDuplicateBioData.KillProcessing();
                EngineDuplicateBioDataSecondary.KillProcessing();

                StopMessageListeners();

                _instant = null;

                _bioDataValidation.Abort();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

        }

        private static void Nightly_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Data Integrity Engine

            if (DateTime.Now.Hour != Setting.NightlyHour)
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

        private static void Dusk_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Data Integrity Engine

            if (DateTime.Now.Hour != Setting.DuskHour)
                return;

            try
            {
                if (_bioDataValidation.ThreadState == ThreadState.Running)
                {
                    _bioDataValidation = new Thread(() => {
                        using (var engineValidation = new EngineSecondaryValidation())
                        {
                            ActivityLogger.Log("INFO", "Starting Secondary Validation Engine");
                            engineValidation.Execute();
                            ActivityLogger.Log("INFO", "Completing Secondary Validation Engine");
                        }
                    });
                    _bioDataValidation.Start();
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
                Core.PubSub.Redis.Operations.Subscribe(EnumLibrary.PubSubAction.ProcessSecondaryDataUploadedFile.NormalizeDisplayName(), FileOperations.ProcessRouter);

                Core.PubSub.Redis.Operations.Subscribe(EnumLibrary.PubSubAction.DeleteUploadedFile.NormalizeDisplayName(), FileOperations.ProcessRouter);

                _instant = new EngineDuplicateBioDataInstant();
                Core.PubSub.Redis.Operations.Subscribe(EnumLibrary.PubSubAction.InstaDedupClientSub.NormalizeDisplayName(), _instant.ReceiveMessage);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void StopMessageListeners()
        {
            try
            {
                Core.PubSub.Redis.Operations.Unsubscribe(EnumLibrary.PubSubAction.ProcessSecondaryDataUploadedFile.NormalizeDisplayName());

                Core.PubSub.Redis.Operations.Unsubscribe(EnumLibrary.PubSubAction.DeleteUploadedFile.NormalizeDisplayName());

                Core.PubSub.Redis.Operations.Unsubscribe(EnumLibrary.PubSubAction.InstaDedupClientSub.NormalizeDisplayName());

                _instant = null;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}
