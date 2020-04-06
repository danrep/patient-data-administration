using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class MessagingCenterController : BaseController
    {
        // GET: MessagingCenter0
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMessageManifest()
        {
            try
            {
                using (var entities = new Entities())
                {
                    var manifest = entities.Integration_SystemDeliveryManifest
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.IsDelivered)
                    .ThenByDescending(x => x.MessageDate)
                    .Take(1000).ToList();

                    return Json(new
                    {
                        Status = true,
                        Message = "Successful",
                        Data = manifest
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}