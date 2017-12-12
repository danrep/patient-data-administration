using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Controllers
{
    public class UserManagementController : BaseController
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}