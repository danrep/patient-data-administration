using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using System.Net;
using PatientDataAdministration.Core.PubSub;
using PatientDataAdministration.EnumLibrary.Dictionary;

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

        public ActionResult SecondaryBioData()
        {
            return View();
        }

        public ActionResult SecondaryBioDataUpload()
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

        public ActionResult UploadFile()
        {
            var isSavedSuccessfully = true;

            foreach (string fileName in Request.Files)
            {
                try
                {
                    var file = Request.Files[fileName];

                    if (file == null || file.ContentLength <= 0)
                        continue;

                    var location =
                        $"{HostingEnvironment.ApplicationPhysicalPath}DataFiles";

                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);

                    var directory = new DirectoryInfo(location);

                    var path = $"{directory.FullName}\\{file.FileName}";
                    file.SaveAs(path);

                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(Setting.FtpUsername, Setting.FtpPassword);
                        client.UploadFile($"ftp://{Setting.FtpHost}:{Setting.FtpServerPort}/{Setting.FtpPath}/PendingDataFiles/{file.FileName}", WebRequestMethods.Ftp.UploadFile, path);
                    }

                    System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {
                    ActivityLogger.Log(ex);
                    isSavedSuccessfully = false;
                }
            }
            
            return
                Json(isSavedSuccessfully
                    ? new {Message = "File(s) was Uploaded Successfully", Status = true}
                    : new {Message = "Error in saving file", Status = false});
        }

        public ActionResult DeleteFile(string name)
        {
            try
            {
                Core.PubSub.Redis.Operations.Publish(PubSubAction.DeleteUploadedFile.NormalizeDisplayName(),
                    new StackExchange.Redis.RedisValue(JsonConvert.SerializeObject(new CommunicationModel()
                    {
                        Data = JsonConvert.SerializeObject(new DeleteFile()
                        {
                            File = name
                        }),
                        PubSubAction = PubSubAction.DeleteUploadedFile
                    })));

                return Json(new {Message = "File(s) was Deleted Successfully", Status = true});
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new {Message = "Error in Deleting file", Status = false});
            }
        }

        public ActionResult ProcessFile(int fileProcessingMethod, List<string> files, bool forceReplace,
            string notifyDestination)
        {
            try
            {
                if (fileProcessingMethod == 0)
                    return
                        Json(
                            new
                            {
                                Message = "Please select a Processing Method!",
                                Status = false
                            });

                Core.PubSub.Redis.Operations.Publish(PubSubAction.ProcessSecondaryDataUploadedFile.NormalizeDisplayName(),
                    new StackExchange.Redis.RedisValue(JsonConvert.SerializeObject(new CommunicationModel()
                    {
                        Data = JsonConvert.SerializeObject(new SecondaryFileData()
                        {
                            Files = files,
                            SecondaryBioDataSources = (SecondaryBioDataSources)fileProcessingMethod,
                            ForceReplace = forceReplace,
                            NotifyDestination = notifyDestination
                        }), 
                        PubSubAction = PubSubAction.ProcessSecondaryDataUploadedFile
                    })));
                
                return
                    Json(
                        new
                        {
                            Message = "Files have been sent for Processing. You will get an Email afterwards!",
                            Status = true
                        });
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new {Message = "Error in Processing file", Status = false});
            }
        }
    }
}