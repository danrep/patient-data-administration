using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class PatientManagementController : BaseServerCommController
    {
        private readonly Entities _db = new Entities();

        public JsonResult GetPatient(string query, int stateId = 0, int siteId = 0)
        {
            try
            {
                var sites = new List<Administration_SiteInformation>();

                if (stateId != 0)
                    sites = _db.Administration_SiteInformation.Where(x => !x.IsDeleted && x.StateId == stateId).ToList();

                if (siteId != 0)
                    sites = sites.Where(x => x.Id == siteId).ToList();

                if (!sites.Any())
                    sites = _db.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList();

                var siteIds = sites.Select(x => x.Id).ToList();

                var patients = _db.Patient_PatientInformation.Where(x => !x.IsDeleted && siteIds.Contains(x.SiteId) && (
                                                                             x.HospitalNumber.Contains(query) ||
                                                                             x.HouseAddress.Contains(query) ||
                                                                             x.Othername.Contains(query) ||
                                                                             x.PepId.Contains(query) ||
                                                                             x.PhoneNumber.Contains(query) ||
                                                                             x.PreviousId.Contains(query) ||
                                                                             x.Surname.Contains(query) ||
                                                                             x.Title.Contains(query) ||
                                                                             x.Sex.Contains(query)
                                                                         )).Take(50).ToList();

                var state = _db.System_State.Where(x => !x.IsDeleted).ToList();
                var lga = _db.System_LocalGovermentArea.Where(x => !x.IsDeleted).ToList();

                return Json(new ResponseData
                {
                    Status = true,
                    Message = "Successful",
                    Data = patients.Select(p => new
                        {
                            PatientInfo = p,
                            SiteInfo = sites.FirstOrDefault(x => x.Id == p.SiteId) ?? new Administration_SiteInformation()
                            {
                                SiteNameOfficial = "[Unassigned]"
                            },
                            SiteInfoState =
                            state.FirstOrDefault(x => x.Id == sites.FirstOrDefault(y => y.Id == p.SiteId)?.StateId),
                            State = state.FirstOrDefault(x => x.Id == p.StateOfOrigin) ?? new System_State()
                            {
                                StateName = "[Unassigned]"
                            },
                            LGA = lga.FirstOrDefault(x => x.Id == p.HouseAddresLga) ?? new System_LocalGovermentArea()
                            {
                                LocalGovermentAreaName = "[Unassigned]"
                            },
                            Age = (DateTime.Now.Subtract(p.DateOfBirth).TotalDays / 365)
                        })
                        .OrderBy(x => x.PatientInfo.Surname)
                        .ThenBy(x => x.PatientInfo.Othername)
                        .ThenBy(x => x.PatientInfo.PhoneNumber).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSinglePatient(int patientId)
        {
            try
            {
                var patient = _db.Patient_PatientInformation.FirstOrDefault(x => !x.IsDeleted && x.Id == patientId);

                if (patient == null)
                    return Json(new ResponseData {Status = false, Message = "Patient not Found"},
                        JsonRequestBehavior.AllowGet);

                var site = _db.Administration_SiteInformation.FirstOrDefault(x => x.Id == patient.SiteId) ??
                           new Administration_SiteInformation()
                           {
                               SiteNameOfficial = "[Unassigned]"
                           };
                var siteState = _db.System_State.FirstOrDefault(x => x.Id == site.StateId) ?? new System_State()
                {
                    StateName = "[Unassigned]"
                };
                var stateOfOriginState = _db.System_State.FirstOrDefault(x => x.Id == patient.StateOfOrigin) ??
                                         new System_State()
                                         {
                                             StateName = "[Unassigned]"
                                         };
                var addressState = _db.System_State.FirstOrDefault(x => x.Id == patient.HouseAddressState) ??
                                   new System_State()
                                   {
                                       StateName = "[Unassigned]"
                                   };
                var addressLga = _db.System_LocalGovermentArea.FirstOrDefault(x => x.Id == patient.HouseAddresLga) ??
                                 new System_LocalGovermentArea()
                                 {
                                     LocalGovermentAreaName = "[Unassigned]"
                                 };

                var biometricData =
                    _db.Patient_PatientBiometricData.FirstOrDefault(x => !x.IsDeleted && x.PepId == patient.PepId);
                var nfcData =
                    _db.Patient_PatientNearFieldCommunicationData.FirstOrDefault(x => !x.IsDeleted && x.PepId == patient.PepId);

                return Json(new ResponseData
                {
                    Status = true,
                    Message = "Successful",
                    Data = new
                    {
                        PatientInfo = patient,
                        Site = site,
                        SiteState = siteState,
                        SiteOriginState = stateOfOriginState,
                        AddressFull = $"{addressState}. {addressState.StateName} LGA. {addressLga.LocalGovermentAreaName} State",
                        AddressState = addressState,
                        AddressLga = addressLga,
                        BiometricData = biometricData,
                        NfcData = nfcData, 
                        Dob = patient.DateOfBirth.ToString("MM/dd/yyyy")
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateSinglePatient(Patient_PatientInformation patientPatientInformation, string dob)
        {
            try
            {
                var patient = _db.Patient_PatientInformation.FirstOrDefault(x => !x.IsDeleted && x.PepId == patientPatientInformation.PepId);

                if (patient == null)
                    return Json(new ResponseData { Status = false, Message = "Patient not Found" },
                        JsonRequestBehavior.AllowGet);

                patient.DateOfBirth = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                patient.HouseAddresLga = patientPatientInformation.HouseAddresLga;
                patient.HouseAddressState = patientPatientInformation.HouseAddressState;
                patient.Othername = patientPatientInformation.Othername;
                patient.PhoneNumber = patientPatientInformation.PhoneNumber;
                patient.Sex = patientPatientInformation.Sex;
                patient.Surname = patientPatientInformation.Surname;

                _db.Entry(patient).State = EntityState.Modified;
                _db.SaveChanges();

                return Json(new ResponseData
                {
                    Status = true,
                    Message = "Successful"
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