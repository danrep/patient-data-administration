using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Client.LocalSettingStorage
{
    public  class AppSetting
    {
        public static string Version => ConfigurationManager.AppSettings["appVersion"];
        public static string ClientId => ConfigurationManager.AppSettings["ClientId"] ?? "";
        private static string BaseCentralPath => System.AppDomain.CurrentDomain.BaseDirectory;

        private static string BaseCentralIntegrationPath => Path.Combine(BaseCentralPath,
            "Integration");
        public static string PathAppointmentDataIngress => Path.Combine(BaseCentralIntegrationPath,
            "AppointmentDataIngress");
    }
}
