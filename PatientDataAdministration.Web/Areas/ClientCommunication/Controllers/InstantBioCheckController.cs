using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using System;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class InstantBioCheckController : BaseClientCommController
    {

        public JsonResult PostNewVerification(PatientInformation patientInformation)
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Message"
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckOperationStatus(string operationGuid)
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Message"
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}