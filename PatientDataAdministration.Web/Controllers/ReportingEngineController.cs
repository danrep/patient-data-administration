using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class ReportingEngineController : BaseController
    {
        public ActionResult DataRequest()
        {
            return View();
        }

        public ActionResult PatientDataExport()
        {
            return View();
        }
    }
}
