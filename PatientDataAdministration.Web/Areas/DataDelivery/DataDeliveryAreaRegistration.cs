using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.DataDelivery
{
    public class DataDeliveryAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "DataDelivery";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DataDelivery_default",
                "DataDelivery/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "PatientDataAdministration.Web.Areas.DataDelivery.Controllers" }
            );
        }
    }
}