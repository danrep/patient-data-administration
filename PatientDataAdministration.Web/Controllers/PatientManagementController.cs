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
using System.IO.Compression;

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
                    ? new {Message = "File(s) was Uploaded Successfully", Status = true}
                    : new {Message = "Error in saving file", Status = false});
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

                        switch ((SecondaryBioDataSources) fileProcessingMethod)
                        {
                            case SecondaryBioDataSources.NmrsBioDataXml:
                                ProcessPatientRecordsNmrsBioDataXml(path, forceReplace, notifyDestination);
                                break;

                            default:
                                ActivityLogger.Log("WARN", "Invalid Processing Type");
                                break;
                        }

                        System.IO.File.Delete(path);

                        ActivityLogger.Log("INFO", $"Upload of {file} Completed. Tracking ID: {trackingGuid}");
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
                return Json(new {Message = "Error in Processing file", Status = false});
            }
        }

        private void ProcessPatientRecordsNmrsBioDataXml(string file, bool forceReplace, string notifyDestination)
        {
            var fileInfo = new FileInfo(file);

            switch (fileInfo.Extension)
            {
                case ".xml":
                    ProcessPatientRecordsNmrsBioDataXmlSingle(file, forceReplace, notifyDestination);
                    break;

                case ".zip":
                    ProcessPatientRecordsNmrsBioDataXmlMultiple(fileInfo, forceReplace, notifyDestination);
                    break;

                default: 
                    ActivityLogger.Log("WARN", "Unsupported File Type Detected");
                    break;
            }
        }

        private string ProcessPatientRecordsNmrsBioDataXmlSingle(string file, bool forceReplace,
            string notifyDestination, bool isBulk = false)
        {
            var messageBody = "";
            if (isBulk == false)
                messageBody += $"Hello {notifyDestination},<br />";

            messageBody += $"The file {new FileInfo(file).Name} ";

            try
            {
                var rawXmlDocument = new XmlDocument();
                rawXmlDocument.Load(file);

                var patientDemographics = rawXmlDocument.GetElementsByTagName("PatientDemographics")[0];

                if (patientDemographics != null)
                    if (patientDemographics.InnerXml.Contains("FingerPrints"))
                    {
                        var patientDemographicsJson = JsonConvert.SerializeXmlNode(patientDemographics,
                            Newtonsoft.Json.Formatting.None, true);
                        var patientDemographicsParsed =
                            JsonConvert.DeserializeObject<NmrsXmlPatientDemographics>(patientDemographicsJson);

                        if (patientDemographicsParsed.PatientIdentifier.Length >= 10 && patientDemographicsParsed
                            .PatientIdentifier.ToCharArray().Count(x => x == '-') == 2)
                        {
                            using (var entities = new Entities())
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
                                if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand
                                    ?.RightMiddle))
                                    score += 10;
                                if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightSmall))
                                    score += 10;
                                if (!string.IsNullOrEmpty(patientDemographicsParsed.FingerPrints.RightHand?.RightThumb))
                                    score += 10;
                                if (!string.IsNullOrEmpty(
                                    patientDemographicsParsed.FingerPrints.RightHand?.RightWedding))
                                    score += 10;

                                if (entities.Patient_PatientBiometricDataSecondary
                                        .Any(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                                  && !x.IsDeleted)
                                    && forceReplace)
                                {
                                    var existingRecord = entities.Patient_PatientBiometricDataSecondary
                                        .FirstOrDefault(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                                             && !x.IsDeleted);

                                    existingRecord.BioDataExtract =
                                        JsonConvert.SerializeObject(patientDemographicsParsed.FingerPrints);
                                    existingRecord.BioDataScore = score;
                                    existingRecord.DataModel = (int) SecondaryBioDataSources.NmrsBioDataXml;
                                    existingRecord.DataSet = JsonConvert.SerializeXmlNode(rawXmlDocument.ChildNodes[1]);
                                    existingRecord.DateRegistered = DateTime.ParseExact(
                                        patientDemographicsParsed.FingerPrints.DateCaptured, "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture);
                                    existingRecord.DateUploaded = DateTime.Now;

                                    entities.Entry(existingRecord).State = EntityState.Modified;

                                    messageBody += "was updated successfully.";
                                }
                                else if (entities.Patient_PatientBiometricDataSecondary
                                             .Any(x => x.PepId == patientDemographicsParsed.PatientIdentifier
                                                       && !x.IsDeleted)
                                         && !forceReplace)
                                {
                                    messageBody +=
                                        "was not loaded because a record has been preloaded and the Force Replace flag was not set.";
                                }
                                else
                                {
                                    var facility = entities.Administration_SiteInformation
                                        .FirstOrDefault(x => x.SiteNameOfficial == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim() || 
                                        x.SiteNameInformal == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim());

                                    entities.Patient_PatientBiometricDataSecondary.Add(
                                        new Patient_PatientBiometricDataSecondary()
                                        {
                                            BioDataExtract =
                                                JsonConvert.SerializeObject(patientDemographicsParsed.FingerPrints),
                                            BioDataScore = score,
                                            DataModel = (int) SecondaryBioDataSources.NmrsBioDataXml,
                                            DataSet = JsonConvert.SerializeXmlNode(rawXmlDocument.ChildNodes[1]),
                                            DateRegistered = DateTime.ParseExact(
                                                patientDemographicsParsed.FingerPrints.DateCaptured, "yyyy-MM-dd",
                                                CultureInfo.InvariantCulture),
                                            DateUploaded = DateTime.Now,
                                            IsDeleted = false,
                                            PepId = patientDemographicsParsed.PatientIdentifier, 
                                            FacilityId = facility?.Id ?? 0, 
                                            StateId = facility?.StateId ?? 0
                                        });

                                    messageBody += "was loaded successfully.";
                                }

                                entities.SaveChanges();
                            }

                            if (isBulk == false)
                                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody,
                                    null);

                            ActivityLogger.Log("INFO", $"{file} has been uploaded successfully.");
                            return messageBody;
                        }

                        messageBody += "was not loaded. The PEPID is Invalid.";
                        ActivityLogger.Log("WARN", $"{file} does not have a valid PEPID.");
                    }
                    else
                    {
                        messageBody += "was not loaded. There was no section containing Fingerprints in it.";
                        ActivityLogger.Log("WARN", $"{file} has NO section containing Fingerprints in it.");
                    }
                else
                {
                    messageBody += "was not loaded. There was NO section containing an IndividualReport in it.";
                    ActivityLogger.Log("WARN", $"{file} has NO section containing an IndividualReport in it");
                }

                if (isBulk == false)
                    Messaging.SendMail(notifyDestination, null, null, "File Processing Status Error", messageBody, file);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                messageBody += "was not loaded. The schema seems to be Invalid.";
                ActivityLogger.Log("WARN", $"{file} has an Invalid Schema");
                Messaging.SendMail(notifyDestination, null, null, "File Processing Status Error", messageBody, file);
            }

            return messageBody;
        }

        private void ProcessPatientRecordsNmrsBioDataXmlMultiple(FileInfo fileInfo, bool forceReplace,
            string notifyDestination)
        {
            try
            {
                var extractedFiles = ExtractZip(fileInfo);
                ActivityLogger.Log("INFO", $"Found {extractedFiles.Count} files for processing.");

                var messageBody = $"Hello {notifyDestination},<br />";
                messageBody +=
                    $"Find below the processing status of the files in the archive {fileInfo.Name}.<br /><br />";

                foreach (var extractedFile in extractedFiles)
                {
                    messageBody += $"{extractedFiles.IndexOf(extractedFile) + 1}: ";
                    messageBody +=
                        ProcessPatientRecordsNmrsBioDataXmlSingle(extractedFile, forceReplace, notifyDestination, true);
                    messageBody += "<br />";
                }

                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);

                var extractionSite =
                    Path.Combine(fileInfo.DirectoryName, fileInfo.Name.Replace(fileInfo.Extension, ""));
                var extractionSiteInfo = new DirectoryInfo(extractionSite);

                if (extractionSiteInfo.Exists)
                    extractionSiteInfo.Delete(true);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private List<string> ExtractZip(FileInfo file)
        {
            try
            {
                var extractionSite = Path.Combine(file.DirectoryName, file.Name.Replace(file.Extension, ""));
                var extractionSiteInfo = new DirectoryInfo(extractionSite);

                if (extractionSiteInfo.Exists)
                    extractionSiteInfo.Delete(true);

                ZipFile.ExtractToDirectory(file.FullName, extractionSite);
                extractionSiteInfo.Attributes = FileAttributes.Normal;

                var fileList = new DirectoryInfo(extractionSite).EnumerateFiles("*.xml", SearchOption.AllDirectories)
                    .OrderBy(x => x.Name)
                    .Select(x => x.FullName)
                    .ToList();

                if (!fileList.Any())
                    fileList = new DirectoryInfo(Path.Combine(extractionSite, file.Name.Replace(file.Extension, "")))
                        .EnumerateFiles("*.xml", SearchOption.AllDirectories)
                        .OrderBy(x => x.Name)
                        .Select(x => x.FullName)
                        .ToList();

                return fileList;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return  new List<string>();
            }
        }
    }
}