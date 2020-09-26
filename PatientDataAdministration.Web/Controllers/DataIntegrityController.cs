using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class DataIntegrityController : BaseController
    {
        // GET: DataIntegrity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TreatPepIdDuplicates()
        {
            return View();
        }

        public ActionResult TreatDupBioDataDuplicatesPrimary()
        {
            return View();
        }

        public ActionResult TreatDupBioDataDuplicatesSecondary()
        {
            return View();
        }
    }
}