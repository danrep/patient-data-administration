using PatientDataAdministration.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientDataAdministration.Core
{
    public static class Transforms
    {
        public static DateTime? NormalizeDate(string rawDate)
        {
            try
            {
                if (rawDate == "...")
                    return null;

                string[] dateComponent;

                if (rawDate.Contains("\\"))
                    dateComponent = rawDate.Split('\\');
                else if (rawDate.Contains("/"))
                    dateComponent = rawDate.Split('/');
                else if (rawDate.Contains("-"))
                    dateComponent = rawDate.Split('-');
                else
                    dateComponent = rawDate.Split('.');

                if (Convert.ToInt32(dateComponent[0].Trim()) > 31)
                    return new DateTime(Convert.ToInt32(dateComponent[0].Trim()),
                        Convert.ToInt32(dateComponent[1].Trim()), Convert.ToInt32(dateComponent[2].Trim()));

                var month = Convert.ToInt32(dateComponent[1].Trim());
                if (month > 12)
                    month = 12;

                var day = Convert.ToInt32(dateComponent[0].Trim());
                if (day > 30 && month == 2)
                    day = 28;

                var date = new DateTime(Convert.ToInt32(dateComponent[2].Trim()),
                    month, day).Date;
                return date;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }

        public static string NormalizePhoneNumber(string rawPhoneNumber)
        {
            if (string.IsNullOrEmpty(rawPhoneNumber))
                return "00000000000";

            if (rawPhoneNumber.Length == 10 && rawPhoneNumber[0] != '0')
                return "0" + rawPhoneNumber;
            else return rawPhoneNumber;
        }

        public static string TrimSpacesBetweenString(string rawString)
        {
            var mystring = rawString.Split(' ');
            var result = mystring.Select(mstr => mstr.Trim()).Where(ss => !string.IsNullOrEmpty(ss))
                .Aggregate(string.Empty, (current, ss) => current + ss + " ");
            return result.Trim();
        }

        public static DateTime? ProcessDateUpperBound(DateTime lastProcessDate, RecurrenceInterval recurrenceInterval, out DateTime lowerBound)
        {
            lowerBound = DateTime.Now;

            try
            {
                switch (recurrenceInterval)
                {
                    case RecurrenceInterval.Day:
                        if (DateTime.Now.Date.Subtract(lastProcessDate.Date).TotalDays > 0)
                        {
                            lowerBound = DateTime.Now.Date;
                            return lowerBound.AddDays(1);
                        }
                        break;
                    case RecurrenceInterval.Month:
                        if (DateTime.Now.Month != lastProcessDate.Month)
                        {
                            lowerBound = new DateTime(lastProcessDate.Year, lastProcessDate.Month, 1).Date;
                            return lowerBound.AddMonths(1);
                        }
                        break;
                }
                return null;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }

        public static string TransformInterval(int inverval)
        {
            var intervalText = "";
            switch ((RecurrenceInterval)inverval)
            {
                case RecurrenceInterval.Month:
                    intervalText = "Monthly";
                    break;
                case RecurrenceInterval.Day:
                    intervalText = "Daily";
                    break;
                case RecurrenceInterval.Year:
                    intervalText = "Yearly";
                    break;
            }
            return intervalText;
        }

        public static List<List<T>> ListChunk<T>(List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return "2340000000000";

            if (phoneNumber.Length == 10 && phoneNumber[0] != '0')
                phoneNumber =  "234" + phoneNumber;
            if (phoneNumber.Length == 11 && phoneNumber[0] == '0')
                phoneNumber = "234" + phoneNumber.Substring(1);
            
            return phoneNumber;
        }
    }
}
