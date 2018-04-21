using PatientDataAdministration.Web.Engines;
using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new Thread(() => {
                var cron = new Cron();
            }).Start();

            new Thread(() => {
                var recurrent = new RecurrentData();
            }).Start();
        }
    }
}