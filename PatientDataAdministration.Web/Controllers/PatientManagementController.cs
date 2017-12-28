using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class PatientManagementController : BaseController
    {
        // GET: ReportManagement
        public ActionResult PatientData()
        {
            return View();
        }
    }
}