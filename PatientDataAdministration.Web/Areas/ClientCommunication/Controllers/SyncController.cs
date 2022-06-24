using PatientDataAdministration.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.Web.Models;

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

        public JsonResult Pull(int cycle = 0, string dateMarker = "")
        {
            try
            {
                var dateRegMarker = string.IsNullOrEmpty(dateMarker) ? new DateTime(2018, 01, 01) : DateTime.ParseExact(dateMarker, "yyyyMMdd", CultureInfo.InvariantCulture);

                var listOfNewPatients = _entities.Patient_PatientInformation
                    .Where(x => !x.IsDeleted && x.LastUpdated > dateRegMarker && _entities.Patient_PatientBiometricData.Any(y => y.PepId == x.PepId))
                    .OrderBy(x => x.LastUpdated)
                    .Skip(100 * cycle)
                    .Take(100).ToList();

                var count = _entities.Patient_PatientInformation
                     .Where(x => !x.IsDeleted && x.LastUpdated > dateRegMarker && _entities.Patient_PatientBiometricData.Any(y => x.PepId == y.PepId))
                     .Count();

                if (!listOfNewPatients.Any())
                    return Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"All Done",
                            Data = null
                        }, JsonRequestBehavior.AllowGet);

                var returnNewPatients = new List<PatientInformation>();

                foreach (var patient in listOfNewPatients)
                {
                    var patientInformation = new PatientInformation
                    {
                        Patient_PatientInformation = new Patient_PatientInformation()
                    };

                    patientInformation.Patient_PatientInformation = patient;
                    patientInformation.Patient_PatientBiometricData = new Patient_PatientBiometricData();
                    patientInformation.Patient_PatientBiometricData =
                        _entities.Patient_PatientBiometricData.FirstOrDefault(x => x.PepId == patient.PepId);

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
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
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
                            AppointmentData =
                                Newtonsoft.Json.JsonConvert.SerializeObject(item.AppointmentDataItems),
                            AppointmentDataManifestId = manifest.Id,
                            AppointmentOffice = item.AppointmentOffice,
                            DateAppointment = DateTime.ParseExact(item.AppointmentDate, "yyyy-MM-dd",
                                CultureInfo.InvariantCulture),
                            DateVisit = DateTime.ParseExact(item.VisitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            PepId = item.PepId,
                            IsValid = item.SendMessage
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

        public JsonResult DetatchPatientTag(string tagUid, int userId)
        {
            try
            {
                if (!SecurityModel.CheckIfUserIsValid(userId))
                    return
                        Json(ResponseData.SendFailMsg("You are not authorised to perform this action"),
                            JsonRequestBehavior.AllowGet);

                SecurityModel.LogAudit(AuditCategory.TagReInit, $@"Formatted Tag with UID {tagUid}", userId);

                _entities.Patient_PatientNearFieldCommunicationData.RemoveRange(
                    _entities.Patient_PatientNearFieldCommunicationData.Where(x => x.CardId == tagUid).ToList());

                _entities.SaveChanges();

                return
                    Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}