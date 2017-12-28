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
    public class IndicatorsController : BaseServerCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult UserStats()
        {
            try
            {
                var users = _entities.Administration_StaffInformation.Count(x => !x.IsDeleted);
                var activeToday =
                    _entities.Administration_PatientRegistrationLog.Where(
                            x => !x.IsDeleted && DbFunctions.TruncateTime(x.DateLogged) >= DbFunctions.TruncateTime(DateTime.Now))
                        .Select(x => x.ClientId)
                        .Distinct()
                        .Count();
                var checkedIn =
                    _entities.Administration_ClientLog.Where(
                            x => !x.IsDeleted && DbFunctions.TruncateTime(x.DateLog) >= DbFunctions.TruncateTime(DateTime.Now))
                        .Select(x => x.CurrentUserId)
                        .Distinct()
                        .Count();
                var inactive = _entities.Sp_Administration_GetInActiveUsers(7).ToList().Count;

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                TotalUsers = users,
                                ActiveToday = activeToday, 
                                CheckedIn = checkedIn,
                                Inactive = inactive
                            }
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PatientStats()
        {
            try
            {
                var patients = _entities.Patient_PatientInformation.Count(x => !x.IsDeleted);
                var date7Past = DateTime.Now.AddDays(-7);
                var seenRecently =
                    _entities.Administration_PatientRegistrationLog.Where(
                            x =>
                                !x.IsDeleted &&
                                DbFunctions.TruncateTime(x.DateLogged) >= DbFunctions.TruncateTime(date7Past))
                        .Select(x => x.PepId)
                        .Distinct()
                        .Count();
                var registeredToday =
                    _entities.Patient_PatientInformation.Count(
                        x => !x.IsDeleted && x.WhenCreated >= DbFunctions.TruncateTime(DateTime.Now));
                var complied = _entities.Sp_Administration_GetPatientCompliance().ToList().Count;

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                TotalPatients = patients,
                                SeenRecently = seenRecently,
                                RegisteredToday = registeredToday,
                                Complied = complied
                            }
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult StateStatsByStateAbb(string abbreviation)
        {
            try
            {
                var state = _entities.System_State.FirstOrDefault(x => !x.IsDeleted && x.StateAbbreviation == abbreviation);

                if(state == null)
                    return
                    Json(
                        new ResponseData
                        {
                            Status = false,
                            Message = "State not Found"
                        },
                        JsonRequestBehavior.AllowGet);

                var patientsInState =
                    _entities.Patient_PatientInformation.Count(x => !x.IsDeleted && x.HouseAddressState == state.Id);

                var sitesInState =
                    _entities.Administration_SiteInformation.Count(x => !x.IsDeleted && x.StateId == state.Id);

                var average = 0.0;
                if (sitesInState != 0)
                    average = patientsInState / (double)sitesInState;

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                State = state,
                                PatientsInState = patientsInState,
                                SitesInState = sitesInState,
                                Average = average
                            }
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAgeDistro()
        {
            try
            {
                var distro =
                    _entities.Sp_Administration_GetAgeDistro()
                        .Select(
                            distroElement => new {x = distroElement.RangeOfValues, y = distroElement.NumberOfOccurences})
                        .ToList();

                var malePatients =
                    _entities.Patient_PatientInformation.Count(x => !x.IsDeleted && x.Sex.ToLower() == "male");
                var femalePatients =
                    _entities.Patient_PatientInformation.Count(x => !x.IsDeleted && x.Sex.ToLower() == "female");

                var sexDistro = new List<object>
                {
                    new {label = "Male", value = malePatients},
                    new {label = "Female", value = femalePatients}
                };

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                Distro = distro,
                                Max = distro.FirstOrDefault(z => z.y == distro.Max(y => y.y)),
                                Min = distro.FirstOrDefault(z => z.y == distro.Min(y => y.y)),
                                SexDistro = sexDistro
                            }
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSiteStats()
        {
            try
            {
                var totalSites = _entities.Administration_SiteInformation.Count(x => !x.IsDeleted);

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                TotalSites = totalSites
                            }
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