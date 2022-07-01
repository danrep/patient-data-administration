using System;
using System.IO;
using System.Net.Mime;
using System.Web.Hosting;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class FileDeliveryController : Controller
    {
        public ActionResult DownloadFile(string auth, string fileName)
        {
            try
            {
                if (SecurityModel.GetUserInSession.AdministrationStaffInformation.PasswordSalt != auth)
                    return null;

                var fullName =
                    Path.Combine($"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage\\{fileName}");

                var fileBytes = GetFile(fullName);
                return File(
                    fileBytes, MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }

        private static byte[] GetFile(string s)
        {
            var fs = System.IO.File.OpenRead(s);
            var data = new byte[fs.Length];
            var br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}
