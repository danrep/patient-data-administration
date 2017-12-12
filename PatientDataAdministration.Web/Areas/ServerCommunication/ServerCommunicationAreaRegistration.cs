using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ServerCommunication
{
    public class ServerCommunicationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ServerCommunication";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ServerCommunication_default",
                "ServerCommunication/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "PatientDataAdministration.Web.Areas.ServerCommunication.Controllers" }
            );
        }
    }
}