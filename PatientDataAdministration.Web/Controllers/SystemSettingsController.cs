using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Controllers
{
    public class SystemSettingsController : BaseController
    {
        // GET: SystemSettings
        public ActionResult UpdateChannelSetUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateChannelSetUp(System_Update systemUpdate)
        {
            using (var entity = new Entities())
            {
                var setting = entity.System_Update.FirstOrDefault(x => x.IsNew);
                if (setting != null)
                {
                    setting.IsNew = false;
                    entity.SaveChanges();
                }

                if (systemUpdate.ServerLocation == null)
                    return View();
                if (systemUpdate.FolderLocation == null)
                    return View();
                if (systemUpdate.ServerUsername == null)
                    return View();
                if (systemUpdate.ServerPassword == null)
                    return View();
                if (systemUpdate.VersionNumber == null)
                    return View();

                systemUpdate.IsNew = true;
                systemUpdate.DateDownloaded = DateTime.Now;
                systemUpdate.DateProvided = DateTime.Now;
                systemUpdate.IsDbRefreshRequired = false;
                
                entity.System_Update.Add(systemUpdate);
                entity.SaveChanges();
            }

            return RedirectToAction("UpdateChannelSetUp", "SystemSettings");
        }
    }
}