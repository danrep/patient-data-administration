using PatientDataAdministration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class MiscController : BaseClientCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult GetUpdateData()
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = _entities.System_Update.FirstOrDefault(x => !x.IsDeleted && x.IsNew)
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Ping()
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}