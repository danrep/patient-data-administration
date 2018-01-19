using System;
using System.Collections.Generic;
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
        public Administration_StaffInformation AdministrationStaffInformation { get; set; }
    }
}