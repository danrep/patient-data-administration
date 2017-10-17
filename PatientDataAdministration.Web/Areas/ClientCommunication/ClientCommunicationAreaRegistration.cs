using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientCommunication
{
    public class ClientCommunicationAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "ClientCommunication";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ClientCommunication_default",
                "ClientCommunication/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}