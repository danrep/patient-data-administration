using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.Integration
{
    public class IntegrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Integration";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Integration_default",
                "Integration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "PatientDataAdministration.Web.Areas.Integration.Controllers" }
            );
        }
    }
}