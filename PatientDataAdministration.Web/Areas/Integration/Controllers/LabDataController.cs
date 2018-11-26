using System.Web.Mvc;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.Integration.Controllers
{
    public class LabDataController : BaseIntegrationController
    {
        public JsonResult GetLabResults()
        {
            return Json(ResponseData.SendSuccessMsg(data: new { }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLabResultPerPatient()
        {
            return Json(ResponseData.SendSuccessMsg(data: new { }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLabSamples()
        {
            return Json(ResponseData.SendSuccessMsg(data: new { }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLabSamplePerPatient()
        {
            return Json(ResponseData.SendSuccessMsg(data: new { }), JsonRequestBehavior.AllowGet);
        }
    }
}
