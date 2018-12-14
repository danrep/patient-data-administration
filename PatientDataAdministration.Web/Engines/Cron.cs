using PatientDataAdministration.Core;
using System;

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

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}