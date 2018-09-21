using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Web.Models;
using Exception = System.Exception;

namespace PatientDataAdministration.Web.Controllers
{
    public class SystemOperationsController : BaseController
    {
        // GET: SystemOperations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplicationLogs(string filename = null)
        {
            try
            {
                ViewBag.FileName = filename ?? ActivityLogger.LogFileName;
                return View(new DirectoryInfo(ActivityLogger.LogFilePath).GetFiles().ToList());
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);

                ViewBag.FileName = "";

                return View(new List<FileInfo>());
            }
        }

        public ActionResult ApplicationLogDelete(string filename)
        {
            try
            {
                var fileInfo = new FileInfo(ActivityLogger.LogFilePath + filename);
                if (!fileInfo.Exists)
                    return RedirectToAction("ApplicationLogs");

                var msg = $"Dear Administrator,<br />";
                msg += $"The following file has been marked for deletion<br />";
                msg += $"<strong>Name</strong>: {fileInfo.Name}<br />";
                msg += $"<strong>Size</strong>: {fileInfo.Length:#,##0} bytes<br />";

                Messaging.SendMail(SecurityModel.GetUserInSession.AdministrationStaffInformation.Email, null, null,
                    "File Delete Notice", msg, fileInfo.FullName);

                System.IO.File.Delete(fileInfo.FullName);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }


            return RedirectToAction("ApplicationLogs");
        }
    }
}