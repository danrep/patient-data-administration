using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Client.LocalSettingStorage
{
    public  class AppSetting
    {
        public static string Version => ConfigurationManager.AppSettings["appVersion"].Replace('.', '_');
        public static string ClientId => ConfigurationManager.AppSettings["ClientId"] ?? "";
    }
}
