using PatientDataAdministration.Web.Engines;
using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PatientDataAdministration.Core;
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

            ActivityLogger.LogFileName = "PDA_Web_Logs.txt";

            new Thread(() => {
                var cron = new Cron();
                ActivityLogger.Log("INFO", "Started Cron Manager");
            }).Start();

            new Thread(() => {
                var recurrent = new RecurrentData();
                ActivityLogger.Log("INFO", "Started Recurrent Data Manager");
            }).Start();
        }
    }
}