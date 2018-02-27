using System;

namespace PatientDataAdministration.Core
{
    public static class Transforms
    {
        public static DateTime NormalizeDate(string rawDate)
        {
            try
            {
                var dateCompnent = new string[] { };

                if (rawDate.Contains("\\"))
                    dateCompnent = rawDate.Split('\\');
                else if (rawDate.Contains("/"))
                    dateCompnent = rawDate.Split('/');
                else if (rawDate.Contains("-"))
                    dateCompnent = rawDate.Split('-');
                else
                    dateCompnent = rawDate.Split('.');

                if (Convert.ToInt32(dateCompnent[0].Trim()) > 31)
                    return new DateTime(Convert.ToInt32(dateCompnent[0].Trim()),
                        Convert.ToInt32(dateCompnent[1].Trim()), Convert.ToInt32(dateCompnent[2].Trim()));

                var month = Convert.ToInt32(dateCompnent[1].Trim());
                if (month > 12)
                    month = 12;

                var day = Convert.ToInt32(dateCompnent[0].Trim());
                if (day > 30 && month == 2)
                    day = 28;

                var date = new DateTime(Convert.ToInt32(dateCompnent[2].Trim()),
                    month, day).Date;
                return date;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return DateTime.Now.Date;
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
    }
}
