using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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