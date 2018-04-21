using PatientDataAdministration.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using ThreadState = System.Threading.ThreadState;

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
                new EngineDataIntegrity();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion
        }

        public static bool GetTimerInterval(int interval, DateTime? lastDate)
        {
            var today = DateTime.Now.Date;
            var publishDate = lastDate ?? DateTime.Now;

            switch ((EnumLibrary.RecurrenceInterval)interval)
            {
                case EnumLibrary.RecurrenceInterval.BiAnnual:
                    if (publishDate.AddDays(182) <= today)
                        return true;
                    break;
                case EnumLibrary.RecurrenceInterval.Day:
                    if (publishDate.AddDays(1) <= today)
                        return true;
                    break;
                case EnumLibrary.RecurrenceInterval.Month:
                    if (publishDate.AddMonths(1) <= today)
                        return true;
                    break;
                case EnumLibrary.RecurrenceInterval.Querterly:
                    if (publishDate.AddMonths(3) <= today)
                        return true;
                    break;
                case EnumLibrary.RecurrenceInterval.Week:
                    if (publishDate.AddDays(7) <= today)
                        return true;
                    break;
                case EnumLibrary.RecurrenceInterval.Year:
                    if (publishDate.AddYears(1) <= today)
                        return true;
                    break;
            }

            return false;
        }
    }
}