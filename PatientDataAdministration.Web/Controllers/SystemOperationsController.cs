using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
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
    }
}