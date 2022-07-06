using System;
using System.Configuration;

namespace PatientDataAdministration.Core
{
    public static class Setting
    {
        public static dynamic MailSettings()
        {
            return new
            {
                SmtpMailFrom = ConfigurationManager.AppSettings["SmtpMailFrom"] ?? "info@PatientDataAdministration.com",
                SmtpMailHead = ConfigurationManager.AppSettings["SmtpMailHead"] ?? "Patient Data Administration Messaging",
                SmtpServer = ConfigurationManager.AppSettings["SmtpServer"] ?? "smtp.gmail.com",
                SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"] ?? "notification@codesistance.com",
                SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"] ?? "skyRunn3r",
                SmtpSslMode = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSslMode"] ?? "true"),
                SmtpServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPort"] ?? "587")
            };
        }

        public static string LogFolder => ConfigurationManager.AppSettings["LogFolder"] ?? "\\";
        public static long LogRollOver => Convert.ToInt64(ConfigurationManager.AppSettings["LogRollOver"] ?? "1000000");

        public static string FtpHost => ConfigurationManager.AppSettings["FtpHost"] ?? "";
        public static string FtpUsername => ConfigurationManager.AppSettings["FtpUsername"] ?? "";
        public static string FtpPassword => ConfigurationManager.AppSettings["FtpPassword"] ?? "";
        public static string FtpPath => ConfigurationManager.AppSettings["FtpPath"] ?? "";
        public static int FtpServerPort => Convert.ToInt32(ConfigurationManager.AppSettings["FtpServerPort"] ?? "587");

        public static int DedupDataLimit => Convert.ToInt32(ConfigurationManager.AppSettings["DedupDataLimit"] ?? "10000");
        public static int DedupProcLimit => Convert.ToInt32(ConfigurationManager.AppSettings["DedupProcLimit"] ?? "1000");
        public static int DedupMatchScore => Convert.ToInt32(ConfigurationManager.AppSettings["DedupMatchScore"] ?? "8000");
        public static int DedupBioDataScore => Convert.ToInt32(ConfigurationManager.AppSettings["DedupBioDataScore"] ?? "70");

        public static string FileLanding => ConfigurationManager.AppSettings["FileLanding"] ?? "";
        public static long NightlyHour => Convert.ToInt32(ConfigurationManager.AppSettings["NightlyHour"] ?? "23");
        public static long DuskHour => Convert.ToInt32(ConfigurationManager.AppSettings["DuskHour"] ?? "04");
    }
}
