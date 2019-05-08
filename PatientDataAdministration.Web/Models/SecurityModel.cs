using System;
using System.Linq;
using System.Web;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;

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

        public static bool CheckIfUserIsValid(int userId)
        {
            try
            {
                using (var entities = new Entities())
                {
                    return entities.Administration_StaffInformation.FirstOrDefault(x =>
                        !x.IsDeleted && x.Id == userId && x.AuthenticationState == (int) Status.Active) != null;
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return false;
            }
        }

        public static void LogAudit(AuditCategory auditCategory, string auditDetail, int userId, object auditData = null )
        {
            try
            {
                using (var entities = new Entities())
                {
                    entities.System_AuditTrail.Add(new System_AuditTrail()
                    {
                        AuditCategory = (int)auditCategory, 
                        AuditData = auditData != null ? Newtonsoft.Json.JsonConvert.SerializeObject(auditData) : null, 
                        AuditDetail = auditDetail, 
                        AuditimeStamp = DateTime.Now, 
                        UserId = userId
                    });
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
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