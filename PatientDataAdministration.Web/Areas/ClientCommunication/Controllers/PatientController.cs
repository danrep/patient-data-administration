using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class PatientController : BaseClientCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult PostPatient(PatientInformation patientInformation)
        {
            try
            {
                var pepId = "";
                if (patientInformation == null)
                    return Json(new ResponseData { Status = false, Message = "No Query Received" }, JsonRequestBehavior.AllowGet);

                if (string.IsNullOrEmpty(patientInformation.Patient_PatientInformation.PepId))
                {
                    pepId = GetPepId(patientInformation.Patient_PatientInformation.SiteId);
                    patientInformation.Patient_PatientInformation.PepId = pepId;
                    _entities.Patient_PatientInformation.Add(patientInformation.Patient_PatientInformation);
                    _entities.SaveChanges();
                }
                else
                {
                    pepId = patientInformation.Patient_PatientInformation.PepId;
                    var existingPatient = _entities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == pepId);

                    if (existingPatient == null)
                        return Json(new ResponseData {Status = false, Message = "Patient Record not Found. Please contact the Administrator"},
                            JsonRequestBehavior.AllowGet);

                    existingPatient.DateOfBirth = patientInformation.Patient_PatientInformation.DateOfBirth;
                    existingPatient.HospitalNumber = patientInformation.Patient_PatientInformation.HospitalNumber;
                    existingPatient.HouseAddresLga = patientInformation.Patient_PatientInformation.HouseAddresLga;
                    existingPatient.HouseAddress = patientInformation.Patient_PatientInformation.HouseAddress;
                    existingPatient.HouseAddressState = patientInformation.Patient_PatientInformation.HouseAddressState;
                    existingPatient.MaritalStatus = patientInformation.Patient_PatientInformation.MaritalStatus;
                    existingPatient.Othername = patientInformation.Patient_PatientInformation.Othername;
                    existingPatient.PassportData = patientInformation.Patient_PatientInformation.PassportData;
                    existingPatient.PhoneNumber = patientInformation.Patient_PatientInformation.PhoneNumber;
                    existingPatient.PreviousId = patientInformation.Patient_PatientInformation.PreviousId;
                    existingPatient.Sex = patientInformation.Patient_PatientInformation.Sex;
                    existingPatient.StateOfOrigin = patientInformation.Patient_PatientInformation.StateOfOrigin;
                    existingPatient.Surname = patientInformation.Patient_PatientInformation.Surname;
                    existingPatient.Title = patientInformation.Patient_PatientInformation.Title;
                }

                if (patientInformation.Patient_PatientBiometricData != null)
                {
                    _entities.Patient_PatientBiometricData.Remove(
                        _entities.Patient_PatientBiometricData.FirstOrDefault(x => x.PepId == pepId) ??
                        new Patient_PatientBiometricData());

                    patientInformation.Patient_PatientBiometricData.PepId = pepId;
                    _entities.Patient_PatientBiometricData.Add(patientInformation.Patient_PatientBiometricData);
                }

                if (patientInformation.Patient_PatientNearFieldCommunicationData != null)
                {
                    _entities.Patient_PatientNearFieldCommunicationData.Remove(
                        _entities.Patient_PatientNearFieldCommunicationData.FirstOrDefault(x => x.PepId == pepId) ??
                        new Patient_PatientNearFieldCommunicationData());

                    patientInformation.Patient_PatientNearFieldCommunicationData.PepId = pepId;
                    _entities.Patient_PatientNearFieldCommunicationData.Add(patientInformation.Patient_PatientNearFieldCommunicationData);
                }

                _entities.Administration_PatientRegistrationLog.Add(new Administration_PatientRegistrationLog()
                {
                    PepId = pepId,
                    ClientId = patientInformation.Administration_StaffInformation.Id,
                    DateLogged = DateTime.Now,
                    IsBioDataCaptured = patientInformation.Patient_PatientBiometricData == null,
                    IsNfcDataCaptured = patientInformation.Patient_PatientNearFieldCommunicationData == null, 
                    IsDeleted = false
                });

                _entities.SaveChanges();
                return Json(new ResponseData { Status = true, Message = "Patient Record Saved Succesfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetPepId(int siteId)
        {
            var count = _entities.Patient_PatientInformation.Count(x => x.SiteId == siteId);
            var siteCode = _entities.Administration_SiteInformation.FirstOrDefault(x => x.Id == siteId)?.SiteCode.ToUpper() + "NA";
            return $@"{siteCode}-{DateTime.Now.Date:yy}-{(++count):0000}";
        }
    }
}