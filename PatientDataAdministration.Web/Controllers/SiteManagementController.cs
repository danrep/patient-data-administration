﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class SiteManagementController : BaseController
    {
        // GET: SiteManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}