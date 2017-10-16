using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientOperations.Controllers
{
    public class BioRegController : Controller
    {
        // GET: ClientOperations/BioReg
        public JsonResult GetPatients()
        {
            try
            {
                return Json(new {Status = true, Message = "Success", Data = string.Empty}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Status = true, Message = "Success", Data = string.Empty}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PostPatient()
        {
            return Json(new {Status = true, Message = "Success", Data = string.Empty}, JsonRequestBehavior.AllowGet);
        }
    }
}