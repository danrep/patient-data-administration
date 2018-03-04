using PatientDataAdministration.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace PatientDataAdministration.Web.Engines
{
    public class Cron
    {
        public Cron()
        {
            System.Timers.Timer timer = new System.Timers.Timer(10000)
            {
                Enabled = true
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region Cache PopulationDistro
            try
            {

            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
            #endregion
        }
    }
}