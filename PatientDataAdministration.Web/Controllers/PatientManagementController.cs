using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.Data.SecondaryBioDataModels;

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

        public ActionResult PatientData(string pepId = null)
        {
            if (string.IsNullOrEmpty(pepId))
                return RedirectToAction("PatientOverview");

            var patient = _entities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == pepId);
            if (patient == null)
                return RedirectToAction("PatientOverview");

            return View(patient);
        }
        
        public ActionResult SecondaryBioDataUpload()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            var isSavedSuccessfully = true;

            try
            {
                foreach (string fileName in Request.Files)
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
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                isSavedSuccessfully = false;
            }

            return
                Json(isSavedSuccessfully
                    ? new { Message = "File(s) was Uploaded Successfully", Status = true }
                    : new { Message = "Error in saving file", Status = false });
        }

        public ActionResult DeleteFile(string name)
        {
            try
            {
                var location =
                    $"{HostingEnvironment.ApplicationPhysicalPath}DataFiles";
                var directory = new DirectoryInfo(location);

                var path = $"{directory.FullName}\\{name}";
                System.IO.File.Delete(path);

                return Json(new { Message = "File(s) was Deleted Successfully", Status = true });
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new { Message = "Error in Deleting file", Status = false });
            }
        }

        public ActionResult ProcessFile(int fileProcessingMethod, List<string> files, bool forceReplace, string notifyDestination)
        {
            try
            {
                var location =
                    $"{HostingEnvironment.ApplicationPhysicalPath}DataFiles";

                var directory = new DirectoryInfo(location);

                foreach (var file in files)
                {
                    var path = $"{directory.FullName}\\{file}";
                    var trackingGuid = Guid.NewGuid().ToString().Trim();

                    if (!System.IO.File.Exists(path))
                    {
                        ActivityLogger.Log("INFO", $"Upload of {file} must have failed. The file was not found.");
                        continue;
                    }

                    var asyncProc = new Thread(() =>
                    {
                        ActivityLogger.Log("INFO", $"Upload of {file} Started. Tracking ID: {trackingGuid}");

                        switch ((SecondaryBioDataSources)fileProcessingMethod)
                        {
                            case SecondaryBioDataSources.NmrsBioDataXml:
                                ProcessPatientRecordsNmrsBioDataXml(path, forceReplace, notifyDestination);
                                break;

                            default:
                                break;
                        }

                        System.IO.File.Delete(path);

                        ActivityLogger.Log("INFO", $"Upload of {file} Colmpleted. Tracking ID: {trackingGuid}");
                    })
                    {
                        Name = $"FP|{trackingGuid}|{file}|{DateTime.Now}"
                    };
                    asyncProc.Start();
                }

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
                return Json(new { Message = "Error in Processing file", Status = false });
            }
        }

        private void ProcessPatientRecordsNmrsBioDataXml(string file, bool forceReplace, string notifyDestination)
        {
            var messageBody = $"Hello {notifyDestination},<br />";
            messageBody += $"The file {new FileInfo(file).Name} ";

            try
            {
                XmlDocument rawXmlDocument = new XmlDocument();
                rawXmlDocument.Load(file);

                var patientDemographics = rawXmlDocument.GetElementsByTagName("PatientDemographics")[0];

                if (patientDemographics != null)
                    if (patientDemographics.InnerXml.Contains("FingerPrints"))
                    {
                        var patientDemographicsJson = JsonConvert.SerializeXmlNode(patientDemographics, Newtonsoft.Json.Formatting.None, true);
                        var patientDemographicsParsed = JsonConvert.DeserializeObject<NmrsXmlPatientDemographics>(patientDemographicsJson);

                        using(var entities = new Entities())
                        {
                            var score = 0;

                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.LeftHand?.LeftIndex))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.LeftHand?.LeftMiddle))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.LeftHand?.LeftSmall))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.LeftHand?.LeftThumb))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.LeftHand?.LeftWedding))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightIndex))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightMiddle))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightSmall))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightThumb))
                                score += 10;
                            if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightWedding))
                                score += 10;

                            if (entities.Patient_PatientBiometricDataSecondary
                                .Any(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                && !x.IsDeleted)
                                && forceReplace)
                            {
                                var existingRecord = entities.Patient_PatientBiometricDataSecondary
                                .FirstOrDefault(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                && !x.IsDeleted);

                                existingRecord.BioDataExtract = JsonConvert.SerializeObject(patientDemographicsParsed.FingerPrints);
                                existingRecord.BioDataScore = score;
                                existingRecord.DataModel = (int)SecondaryBioDataSources.NmrsBioDataXml;
                                existingRecord.DataSet = JsonConvert.SerializeXmlNode(rawXmlDocument.ChildNodes[1]);
                                existingRecord.DateRegistered = DateTime.ParseExact(patientDemographicsParsed.FingerPrints.DateCaptured, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                existingRecord.DateUploaded = DateTime.Now;

                                entities.Entry(existingRecord).State = EntityState.Modified;

                                messageBody += "was updated successfully.";
                            }
                            else if (entities.Patient_PatientBiometricDataSecondary
                                .Any(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                && !x.IsDeleted)
                                && !forceReplace)
                            {
                                messageBody += "was not loaded because a record has been preloaded and the Force Replace flag was not set.";
                            }
                            else
                            {
                                entities.Patient_PatientBiometricDataSecondary.Add(new Patient_PatientBiometricDataSecondary()
                                {
                                    BioDataExtract = JsonConvert.SerializeObject(patientDemographicsParsed.FingerPrints),
                                    BioDataScore = score,
                                    DataModel = (int)SecondaryBioDataSources.NmrsBioDataXml,
                                    DataSet = JsonConvert.SerializeXmlNode(rawXmlDocument.ChildNodes[1]),
                                    DateRegistered = DateTime.ParseExact(patientDemographicsParsed.FingerPrints.DateCaptured, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                    DateUploaded = DateTime.Now,
                                    IsDeleted = false,
                                    PepId = patientDemographicsParsed.PatientIdentifier
                                });

                                messageBody += "was loaded successfully.";
                            }

                            entities.SaveChanges();
                        }

                        Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
                        return;
                    }
                    else
                        messageBody += "was not loaded. There was no section containing Fingerprints in it.";
                else
                    messageBody += "was not loaded. There was no section containing an IndividualReport in it.";

                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                messageBody += "was not loaded. The schema seems to be Invalid.";
                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
            }
        }
    }
}