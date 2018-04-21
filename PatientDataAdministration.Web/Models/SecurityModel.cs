using System.Linq;
using System.Web;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Models
{
    public static class SecurityModel
    {
        public static bool IsUserSessionActive => HttpContext.Current.Session["UserInformation"] != null;

        public static void SetUserSession(UserInformation userInformation)
        {
            HttpContext.Current.Session.Add("UserInformation", userInformation);
        }

        public static UserInformation GetUserInSession
            => (UserInformation) HttpContext.Current.Session["UserInformation"];

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
        }
    }

    public class UserInformation
    {
        private readonly Entities _entities = new Entities();

        public Administration_StaffInformation AdministrationStaffInformation { get; set; }

        public Administration_SiteInformation AdministrationSiteInformation
            =>
                _entities.Administration_SiteInformation.FirstOrDefault(
                    x => x.Id == AdministrationStaffInformation.SiteId) ?? new Administration_SiteInformation()
                {
                    SiteNameOfficial = "Head Office"
                };
    }
}