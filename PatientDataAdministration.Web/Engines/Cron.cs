using PatientDataAdministration.Core;
using System;

namespace PatientDataAdministration.Web.Engines
{
    public class Cron
    {
        public Cron()
        {
            var timer = new System.Timers.Timer(60000)
            {
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
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
        }
    }
}