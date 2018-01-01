using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Controllers
{
    public class PatientManagementController : BaseController
    {
        private readonly Entities _entities = new Entities();

        // GET: ReportManagement
        public ActionResult PatientOverview()
        {
            return View();
        }

        public ActionResult PatientData(string pepId = null)
        {
            if (string.IsNullOrEmpty(pepId))
                return RedirectToAction("PatientOverview");

            var patient = _entities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == pepId);
            if (patient == null)
                return RedirectToAction("PatientOverview");

            return View(patient);
        }
    }
}