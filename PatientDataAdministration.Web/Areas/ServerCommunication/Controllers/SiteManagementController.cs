using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class SiteManagementController : BaseServerCommController
    {
        private readonly Entities _db = new Entities();

        public JsonResult SaveSite(Administration_SiteInformation administrationSiteInformation)
        {
            try
            {
                if (administrationSiteInformation.Id == 0)
                {
                    if (
                        _db.Administration_SiteInformation.FirstOrDefault(
                            x =>
                                !x.IsDeleted &&
                                x.SiteNameOfficial == administrationSiteInformation.SiteNameOfficial &&
                                x.SiteCode == administrationSiteInformation.SiteCode &&
                                x.StateId == administrationSiteInformation.StateId) != null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "There is a Site having the same name in this State. Please Check"
                                },
                                JsonRequestBehavior.AllowGet);

                    _db.Administration_SiteInformation.Add(administrationSiteInformation);
                }
                else
                {
                    var existing = _db.Administration_SiteInformation.FirstOrDefault(
                        x =>
                            !x.IsDeleted && x.Id == administrationSiteInformation.Id);

                    if (existing == null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "This site does not exist."
                                },
                                JsonRequestBehavior.AllowGet);

                    if (_db.Administration_SiteInformation.FirstOrDefault(
                        x =>
                            !x.IsDeleted && 
                            x.SiteCode == administrationSiteInformation.SiteCode &&
                            x.SiteNameOfficial == administrationSiteInformation.SiteNameOfficial &&
                            x.Id != administrationSiteInformation.Id) != null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "The new information is clashing with one of the sites."
                                },
                                JsonRequestBehavior.AllowGet);

                    existing.SiteNameOfficial = administrationSiteInformation.SiteNameOfficial;
                    existing.SiteCode = administrationSiteInformation.SiteCode;
                    existing.StateId = administrationSiteInformation.StateId;
                    existing.LastUpdate = DateTime.Now;

                    _db.Entry(existing).State = EntityState.Modified;
                }

                _db.SaveChanges();
                return Json(new ResponseData { Status = true, Message = "Successful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSites()
        {
            try
            {
                var sites = _db.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList();
                var states = _db.System_State.ToList();

                var siteData =
                    sites.Select(
                            s =>
                                new
                                {
                                    SiteInformation = s,
                                    StateInformation = states.FirstOrDefault(x => x.Id == s.StateId)
                                })
                        .OrderBy(x => x.StateInformation.StateName)
                        .ThenBy(x => x.SiteInformation.SiteNameOfficial)
                        .ToList();

                return Json(new ResponseData { Status = true, Message = "Successful", Data = siteData },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSiteSingle(int siteId)
        {
            try
            {
                var site = _db.Administration_SiteInformation.FirstOrDefault(x => !x.IsDeleted && x.Id == siteId);

                return
                    Json(
                        site == null
                            ? new ResponseData {Status = true, Message = "No Site Found"}
                            : new ResponseData {Status = true, Message = "Successful", Data = site},
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}