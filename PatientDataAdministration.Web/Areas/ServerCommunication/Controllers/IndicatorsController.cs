using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Web.Engines;

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
                var registeredRecently =
                    _entities.Patient_PatientInformation.Where(
                            x =>
                                !x.IsDeleted &&
                                DbFunctions.TruncateTime(x.WhenCreated) >= DbFunctions.TruncateTime(date7Past))
                        .Select(x => x.PepId)
                        .Distinct()
                        .Count();
                var updatedRecently =
                    _entities.Patient_PatientInformation.Where(
                            x =>
                                !x.IsDeleted &&
                                DbFunctions.TruncateTime(x.LastUpdated) >= DbFunctions.TruncateTime(date7Past) &&
                                DbFunctions.TruncateTime(x.WhenCreated) < date7Past &&
                                x.LastUpdated > x.WhenCreated)
                        .Select(x => x.PepId)
                        .Distinct()
                        .Count();

                var registeredToday =
                    _entities.Patient_PatientInformation.Where(
                        x => !x.IsDeleted && x.WhenCreated >= DbFunctions.TruncateTime(DateTime.Now))
                        .Select(x => x.PepId)
                        .Distinct()
                        .Count();
                var updatedToday =
                    _entities.Patient_PatientInformation.Where(
                        x => !x.IsDeleted && x.LastUpdated >= DbFunctions.TruncateTime(DateTime.Now) &&
                        DbFunctions.TruncateTime(x.WhenCreated) != DbFunctions.TruncateTime(DateTime.Now))
                        .Select(x => x.PepId)
                        .Distinct()
                        .Count();

                var complied = _entities.Sp_Administration_GetPatientCompliance().ToList().Count;

                var biometricsOnly =
                    _entities.Patient_PatientBiometricData.Select(x => x.PepId).Distinct().ToList().Count;

                var bioStats = _entities.Sp_System_Indicators_PopulationDistro_BioCount(null).ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                TotalPatients = patients,
                                RegisteredRecently = registeredRecently,
                                UpdatedRecently = updatedRecently,
                                RegisteredToday = registeredToday,
                                UpdatedToday = updatedToday,
                                Complied = complied,
                                QuickFacts = new
                                {
                                    RegBioCount = biometricsOnly,
                                    RegBioPercent = (biometricsOnly / Convert.ToDouble(patients)) * 100,
                                    NewBio = bioStats.Count(x => !x.IsUpdated ?? false),
                                    UpdatedBio = bioStats.Count(x => x.IsUpdated ?? false)
                                }
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

        public JsonResult PatientStatsByDate(string date)
        {
            try
            {
                date = date.Replace("00/", DateTime.Now.Month.ToString("00") + "/");

                var dateOfConcern = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var previousDateOfConcern = dateOfConcern.Subtract(new TimeSpan(1, 0, 0, 0));

                var patients =
                    _entities.Patient_PatientInformation.Where(
                        x =>
                            !x.IsDeleted &&
                            DbFunctions.TruncateTime(x.WhenCreated) == DbFunctions.TruncateTime(dateOfConcern)).ToList();

                var patientsOfPreviousDay = _entities.Patient_PatientInformation.Count(x => !x.IsDeleted &&
                        DbFunctions.TruncateTime(x.WhenCreated) == DbFunctions.TruncateTime(previousDateOfConcern));

                if (!patients.Any())
                    return
                        Json(
                            new ResponseData
                            {
                                Status = true,
                                Message = "Successful",
                                Data = new
                                {
                                    TotalPatients = 0,
                                    MalePatients = 0,
                                    FemalePatients = 0,
                                    ChangeOverYesterday = 0
                                }
                            },
                            JsonRequestBehavior.AllowGet);

                var top3SiteData = patients.GroupBy(info => info.SiteId)
                    .Select(group => new
                    {
                        SiteId = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(x => x.Count).Take(3).ToList();

                var top3Sites = top3SiteData.Select(x => x.SiteId).ToList();

                var states = _entities.System_State.Where(x => !x.IsDeleted).ToList();

                var sites =
                    _entities.Administration_SiteInformation.Where(
                        x => !x.IsDeleted && top3Sites.Contains(x.Id));

                var siteData = top3SiteData.Select(siteMetric => new
                {
                    SiteMetric = siteMetric,
                    Site = sites.FirstOrDefault(z => z.Id == siteMetric.SiteId) ?? null, 
                    PercentOverGlobal = ((siteMetric.Count * 100) / patients.Count)
                }).ToList();

                var changeOverYesterday = 0.0;
                if (patientsOfPreviousDay != 0)
                    changeOverYesterday = ((patients.Count - patientsOfPreviousDay) * 100) / patientsOfPreviousDay;
                else
                    changeOverYesterday = 0;

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = new
                            {
                                TotalPatients = patients.Count,
                                HighSitesInfo = siteData.Select(sm => new
                                {
                                    Metric = sm,
                                    State = states.FirstOrDefault(y => y.Id == sm.Site.StateId)
                                }).OrderByDescending(x => x.Metric.SiteMetric.Count).ToList(),
                                MalePatients = patients.Count(x => x.Sex.ToLower() == "male"),
                                FemalePatients = patients.Count(x => x.Sex.ToLower() == "female"), 
                                ChangeOverYesterday = changeOverYesterday
                            }
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
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

                var sitesInState =
                    _entities.Administration_SiteInformation.Count(x => !x.IsDeleted && x.StateId == state.Id);

                var distroData = _entities.Sp_System_Indicators_PopulationDistro_SexSiteState()
                    .Where(x => x.StateAbbreviation == abbreviation).ToList();

                var patientsInState = distroData.Sum(x => x.PatientPopulation).Value;
                var collectedBioData = _entities.Sp_System_Indicators_PopulationDistro_BioCount(abbreviation).ToList();
                var collectedNfcData = _entities.Sp_System_Indicators_PopulationDistro_NfcCount(abbreviation).ToList();

                var average = 0.0;
                if (sitesInState != 0)
                    average = patientsInState / (double)sitesInState;

                var distro = _entities.Sp_System_Indicators_PopulationDistro_30DayStatePlotData(abbreviation)
                    .OrderBy(x => x.PlotDate).Select(
                            distroElement => new { x = distroElement.PlotDate.ToString("yyyy-MM-dd"), y = distroElement.CreatedCount, z = distroElement.UpdatedCount })
                        .ToList();

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
                                Average = average,
                                CollectedBioData = collectedBioData.Sum(x=> x.PatientPopulation).Value,
                                CollectedNfcData = collectedNfcData.Sum(x => x.PatientPopulation).Value,
                                TotalMales = distroData.Where(x => x.Sex == "male").ToList().Count(),
                                TotalFemales = distroData.Where(x => x.Sex == "female").ToList().Count(), 
                                Distro = distro, 
                                NewBio = collectedBioData.Where(x=> !x.IsUpdated.Value).Sum(x => x.PatientPopulation).Value,
                                UpdatedBio = collectedBioData.Where(x => x.IsUpdated.Value).Sum(x => x.PatientPopulation).Value
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
                                SexDistro = sexDistro,
                                BadDate = _entities.Patient_PatientInformation.Count(x => x.DateOfBirth == null)
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

        public JsonResult GetSiteDistro()
        {
            try
            {
                var allSites = LocalCache.Get<List<Administration_SiteInformation>>(
                    "Administration_SiteInformation");

                var allSitesInStateInCountry =
                    LocalCache.Get<List<Sp_System_Indicators_PopulationDistro_SexSiteState_Result>>(
                        "Sp_System_Indicators_PopulationDistro_SexSiteState_Result");

                var states =
                    LocalCache.Get<List<System_State>>(
                        "System_State");

                var totalPopulation = allSitesInStateInCountry.Sum(x => x.PatientPopulation);

                var allPatientInSite = allSitesInStateInCountry
                    .GroupBy(sites => new {sites.SiteId, sites.StateAbbreviation})
                    .Select(grp => new
                    {
                        level = "Site",
                        name = allSites.FirstOrDefault(x => x.Id == grp.Key.SiteId)?.SiteNameOfficial ?? "Unknown",
                        value = allSitesInStateInCountry.Where(x =>
                                x.StateAbbreviation == grp.Key.StateAbbreviation && x.SiteId == grp.Key.SiteId)
                            .Sum(x => x.PatientPopulation),
                        description =
                            $"{allSites.FirstOrDefault(x => x.Id == grp.Key.SiteId)?.SiteNameOfficial ?? "Unknown"}",
                        state = states.FirstOrDefault(x => x.StateAbbreviation == grp.Key.StateAbbreviation),
                        site = allSites.FirstOrDefault(x => x.Id == grp.Key.SiteId)
                    }).ToArray();

                var allSitesInStates = allSitesInStateInCountry
                    .GroupBy(sites => sites.StateAbbreviation)
                    .Select(grp => new
                    {
                        level = "State",
                        name = states.FirstOrDefault(x => x.StateAbbreviation == grp.Key)?.StateName ?? "Unknown",
                        description = states.FirstOrDefault(x => x.StateAbbreviation == grp.Key),
                        value = allPatientInSite.Where(x => x.state.StateAbbreviation == grp.Key)
                            .Sum(x => x.value ?? 0),
                        children = allPatientInSite.Where(x => x.state.StateAbbreviation == grp.Key).ToList()
                    }).ToArray();

                return
                    Json(
                        new
                        {
                            Level = "National",
                            name = "Nigeria",
                            value = totalPopulation,
                            children = allSitesInStates
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSiteDistroForState(string stateAbbreviation)
        {
            try
            {
                var states =
                    LocalCache.Get<List<System_State>>(
                        "System_State");

                var distroData =
                    LocalCache.Get<List<Sp_System_Indicators_PopulationDistro_SexSiteState_Result>>(
                            "Sp_System_Indicators_PopulationDistro_SexSiteState_Result")
                        .Where(x => x.StateAbbreviation == stateAbbreviation).ToList();

                return
                    Json(ResponseData.SendSuccessMsg(data: new
                    {
                        Heading = "State Information",
                        Name =
                            states.FirstOrDefault(x => x.StateAbbreviation == stateAbbreviation)?.StateName ??
                            "NA",
                        SiteCount = distroData.GroupBy(x => x.SiteId).Count(),
                        PatientTotal = distroData.Sum(x => x.PatientPopulation), 
                        SiteId = 0
                    }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(ResponseData.SendExceptionMsg(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSiteDistroForSite(int siteId)
        {
            try
            {
                var site =
                    LocalCache.Get<List<Administration_SiteInformation>>(
                        "Administration_SiteInformation").FirstOrDefault(x => x.Id == siteId);

                var state =
                    LocalCache.Get<List<System_State>>(
                        "System_State").FirstOrDefault(x => x.Id == (site?.StateId ?? 0));

                var distroData =
                    LocalCache.Get<List<Sp_System_Indicators_PopulationDistro_SexSiteState_Result>>(
                            "Sp_System_Indicators_PopulationDistro_SexSiteState_Result")
                        .Where(x => x.SiteId == siteId).ToList();

                return
                    Json(ResponseData.SendSuccessMsg(data: new
                    {
                        Heading = "Site Information",
                        Name = site?.SiteNameOfficial ?? "NA",
                        PatientTotal = distroData.Sum(x => x.PatientPopulation),
                        Location = state?.StateName ?? "NA",
                        SiteId = siteId
                    }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(ResponseData.SendExceptionMsg(ex), JsonRequestBehavior.AllowGet);
            }
        }
    }
}