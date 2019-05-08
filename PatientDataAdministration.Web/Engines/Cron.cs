using PatientDataAdministration.Core;
using System;
using System.Linq;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Engines
{
    public class Cron
    {
        private static bool _isTriggered;
        private static int _currentHour;

        public Cron()
        {
            var timer = new System.Timers.Timer(60000)
            {
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            _currentHour = 0;
            _isTriggered = false;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isTriggered)
                return;
            _isTriggered = true;

            if (_currentHour == DateTime.Now.Hour)
                return;
            _currentHour = DateTime.Now.Hour;

            #region Data Integrity Engine

            try
            {
                new EngineDataIntegrity.EngineDataIntegrity();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Reporting Engine

            try
            {
                new EngineReporting.EngineReporting();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Operation Engine

            try
            {
                new EngineOperationManagement.EngineOperation();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Misc Operations

            try
            {
                using (var pdaEntities = new Entities())
                {
                    var pendingHashes = pdaEntities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.FingerDataHash)).Take(100).ToList();

                    foreach (var pendingHash in pendingHashes)
                    {
                        pendingHash.FingerDataHash =
                            Sha512Engine.GenerateSHA512String(
                                pendingHash.FingerPrimary + "|" + pendingHash.FingerSecondary);
                        pdaEntities.Entry(pendingHash).State = System.Data.Entity.EntityState.Modified;
                        pdaEntities.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
            #endregion

            GC.Collect();
            GC.WaitForPendingFinalizers();

            _isTriggered = false;
        }
    }
}