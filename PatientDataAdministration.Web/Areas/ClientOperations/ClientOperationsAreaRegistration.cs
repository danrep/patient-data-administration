using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientOperations
{
    public class ClientOperationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ClientOperations";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ClientOperations_default",
                "ClientOperations/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}