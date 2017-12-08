using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class UserController : BaseClientCommController
    {
        // GET: ClientCommunication/User
        public ActionResult Index()
        {
            return View();
        }
    }
}