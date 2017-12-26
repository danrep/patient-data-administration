using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class DataController : BaseServerCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult GetStates()
        {
            try
            {
                var states = _entities.System_State.Where(x => !x.IsDeleted).OrderBy(x => x.StateName).ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = states
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSites(int stateId = 0)
        {
            try
            {
                var sites =
                    _entities.Administration_SiteInformation.Where(
                            x => !x.IsDeleted && x.StateId == (stateId == 0 ? x.StateId : stateId))
                        .OrderBy(x => x.SiteNameOfficial)
                        .ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = sites
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetLocalGovermentArea(int stateId = 0)
        {
            try
            {
                var lga =
                    _entities.System_LocalGovermentArea.Where(
                            x => !x.IsDeleted && x.StateID == (stateId == 0 ? x.StateID : stateId))
                        .OrderBy(x => x.LocalGovermentAreaName)
                        .ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = lga
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