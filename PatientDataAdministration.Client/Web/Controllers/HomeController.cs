using System.Web.Http;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Client.Web.Controllers
{
    public class HomeController: ApiController
    {
        public ResponseData Get()
        {
            return ResponseData.SendSuccessMsg("PDA Client",
                new
                {
                    Version = $"Version {LocalSettingStorage.AppSetting.Version}", 
                    Status = "Running"
                });
        }
    }
}
