using PatientDataAdministration.Data;
using System;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Controllers
{
    public class CommonController : Controller
    {
        readonly Entities _entities = new Entities();

        public JsonResult States()
        {
            try
            {
                var states =
                    _entities.System_State.Where(
                        x => !x.IsDeleted).OrderBy(x => x.StateName).ToList();

                return Json(new
                {
                    Status = true,
                    Message = "Successful",
                    Data = states
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LocalGovtAreas(int stateId)
        {
            try
            {
                var lgas =
                    _entities.System_LocalGovermentArea.Where(
                        x => !x.IsDeleted && x.StateID == stateId).OrderBy(x => x.LocalGovermentAreaName).ToList();

                return Json(new
                {
                    Status = true,
                    Message = "Successful",
                    Data = lgas
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Sites()
        {
            try
            {
                var sites =
                    _entities.Administration_SiteInformation.Where(
                        x => !x.IsDeleted).OrderBy(x => x.SiteNameOfficial).ToList();

                return Json(new
                {
                    Status = true,
                    Message = "Successful",
                    Data = sites
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