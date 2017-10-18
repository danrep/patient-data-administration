using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class PatentController : BaseController
    {
        readonly Entities _entities = new Entities();
        public JsonResult RegisterPatient(Patient_PatientInformation patientInformation)
        {
            try
            {
                if (patientInformation.Id == 0)
                {

                }
                else
                {
                    var existing =
                        _entities.Patient_PatientInformation.FirstOrDefault(
                            x => !x.IsDeleted && x.Id == patientInformation.Id);
                }
                _entities.SaveChanges();

                return Json(new { Status = false, Message = "Successful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}