using PatientDataAdministration.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class SyncController : BaseClientCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult CheckIfUpdated(List<PatientMatching> listOfPatientMatching)
        {
            try
            {
                var returnUpdatedPatients = new List<PatientInformation>();

                foreach (var patient in listOfPatientMatching)
                {
                    var patientInformation = new PatientInformation();

                    var patientData = _entities.Patient_PatientInformation.FirstOrDefault(
                        x => x.PepId == patient.PepId && x.LastUpdated > patient.LastUpdate);

                    if (patientData == null)
                        continue;

                    patientInformation.Patient_PatientInformation = new Patient_PatientInformation();
                    patientInformation.Patient_PatientInformation = patientData;

                    if (_entities.Patient_PatientBiometricData.Any(x => x.PepId == patient.PepId))
                    {
                        patientInformation.Patient_PatientBiometricData = new Patient_PatientBiometricData();
                        patientInformation.Patient_PatientBiometricData =
                            _entities.Patient_PatientBiometricData.FirstOrDefault(x => x.PepId == patient.PepId);
                    }

                    if (_entities.Patient_PatientNearFieldCommunicationData.Any(x => x.PepId == patient.PepId))
                    {
                        patientInformation.Patient_PatientNearFieldCommunicationData =
                            new Patient_PatientNearFieldCommunicationData();
                        patientInformation.Patient_PatientNearFieldCommunicationData =
                            _entities.Patient_PatientNearFieldCommunicationData.FirstOrDefault(
                                x => x.PepId == patient.PepId);
                    }

                    returnUpdatedPatients.Add(patientInformation);
                }

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Found {returnUpdatedPatients.Count} Patient Information for Updating",
                            Data = returnUpdatedPatients
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PullNew(string encodedListOfAvailablePepId, int siteId)
        {
            try
            {
                var stringData = Convert.FromBase64String(encodedListOfAvailablePepId);
                var listOfAvalailablePepId =
                    Encoding.ASCII.GetString(stringData).ToLower().Split(',').OrderBy(x => x).ToList();

                var patientsInSite = _entities.Patient_PatientInformation
                    .Where(x => !x.IsDeleted && x.SiteId == siteId)
                    .OrderBy(x => x.Id)
                    .ToList();

                var listOfNewPatients = patientsInSite
                    .Where(x => listOfAvalailablePepId.Any(y => y == x.PepId.ToLower()) == false)
                    .Take(100).ToList();

                var returnNewPatients = new List<PatientInformation>();

                foreach (var patient in listOfNewPatients)
                {
                    var patientInformation = new PatientInformation
                    {
                        Patient_PatientInformation = new Patient_PatientInformation()
                    };
                    patientInformation.Patient_PatientInformation = patient;

                    if (_entities.Patient_PatientBiometricData.Any(x => x.PepId == patient.PepId))
                    {
                        patientInformation.Patient_PatientBiometricData = new Patient_PatientBiometricData();
                        patientInformation.Patient_PatientBiometricData =
                            _entities.Patient_PatientBiometricData.FirstOrDefault(x => x.PepId == patient.PepId);
                    }

                    if (_entities.Patient_PatientNearFieldCommunicationData.Any(x => x.PepId == patient.PepId))
                    {
                        patientInformation.Patient_PatientNearFieldCommunicationData =
                            new Patient_PatientNearFieldCommunicationData();
                        patientInformation.Patient_PatientNearFieldCommunicationData =
                            _entities.Patient_PatientNearFieldCommunicationData.FirstOrDefault(x =>
                                x.PepId == patient.PepId);
                    }

                    returnNewPatients.Add(patientInformation);
                }

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Found {returnNewPatients.Count} Patient Information for Updating",
                            Data = returnNewPatients
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

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
                            Message = $"Found {states.Count} States",
                            Data = states
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetLocalGovermentArea()
        {
            try
            {
                var lga = _entities.System_LocalGovermentArea.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.LocalGovermentAreaName).ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Found {lga.Count} LGAs",
                            Data = lga
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSiteData()
        {
            try
            {
                var sites = _entities.Administration_SiteInformation.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.SiteNameOfficial).ToList();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Found {sites.Count} LGAs",
                            Data = sites
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ClientPushAppointmentData(
            IntegrationAppointmentDataIngressPayLoad integrationAppointmentDataIngressPayLoad)
        {
            try
            {
                var manifest = new Integration_AppointmentDataManifest()
                {
                    ClientId = integrationAppointmentDataIngressPayLoad.ClientId,
                    SiteId = integrationAppointmentDataIngressPayLoad.SiteId,
                    DateLog = DateTime.Now,
                    IsDeleted = false,
                    UserId = integrationAppointmentDataIngressPayLoad.UserId
                };
                _entities.Integration_AppointmentDataManifest.Add(manifest);
                _entities.SaveChanges();

                _entities.Integration_AppointmentDataItem.AddRange(
                    integrationAppointmentDataIngressPayLoad.IntegrationAppointmentDataIngresses.Select(item =>
                        new Integration_AppointmentDataItem()
                        {
                            IsDeleted = false,
                            AppointmentData = Newtonsoft.Json.JsonConvert.SerializeObject(item.AppointmentData),
                            AppointmentDataManifestId = manifest.Id,
                            AppointmentOffice = item.AppointmentOffice,
                            DateAppointment = DateTime.ParseExact(item.AppointmentDate, "yyyy-MM-dd",
                                CultureInfo.InvariantCulture),
                            DateVisit = DateTime.ParseExact(item.VisitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            PepId = item.PepId
                        }));
                _entities.SaveChanges();

                return
                    Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}