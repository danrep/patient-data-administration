using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Core.PubSub;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.SecondaryBioDataModels;
using PatientDataAdministration.EnumLibrary;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Xml;

namespace PatientDataAdministration.DeduplicationEngine.Engines.FileOperations
{
    public class FileOperations
    {
        public static PubSubResponse ProcessRouter(ChannelMessage channelMessage)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<CommunicationModel>(channelMessage.Message.ToString());

                switch(message.PubSubAction)
                {
                    case PubSubAction.ProcessSecondaryDataUploadedFile:
                        new Thread(()=> ProcessFile(JsonConvert.DeserializeObject<SecondaryFileData>(message.Data))).Start();
                        break;

                    case PubSubAction.DeleteUploadedFile:
                        new Thread(() => DeleteFile(JsonConvert.DeserializeObject<DeleteFile>(message.Data))).Start();
                        break;

                    default:
                        break;
                }

                return PubSubResponse.Success;
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return PubSubResponse.Error;
            }
        }

        public static void ProcessFile(SecondaryFileData data)
        {
            try
            {
                var directory = new DirectoryInfo(Setting.FileLanding);

                foreach (var file in data.Files)
                {
                    var path = $"{directory.FullName}\\{file}";
                    var trackingGuid = Guid.NewGuid().ToString().Trim();

                    if (!File.Exists(path))
                    {
                        ActivityLogger.Log("INFO", $"Upload of {file} must have failed. The file was not found.");
                        continue;
                    }

                    try
                    {
                        ActivityLogger.Log("INFO", $"Upload of {file} Started. Tracking ID: {trackingGuid}");

                        switch (data.SecondaryBioDataSources)
                        {
                            case SecondaryBioDataSources.NmrsBioDataXml:
                                ProcessPatientRecordsNmrsBioDataXml(path, data.ForceReplace, data.NotifyDestination);
                                break;

                            case SecondaryBioDataSources.NdrBioDataCsv:
                                ProcessPatientRecordsNdrBioDataCsv(path, data.ForceReplace, data.NotifyDestination);
                                break;

                            default:
                                ActivityLogger.Log("WARN", "Invalid Processing Type");
                                break;
                        }

                        File.Delete(path);

                        ActivityLogger.Log("INFO", $"Upload of {file} Completed. Tracking ID: {trackingGuid}");
                    }
                    catch (Exception ex)
                    {
                        ActivityLogger.Log(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        public static void DeleteFile(DeleteFile data)
        {
            try
            {
                var directory = new DirectoryInfo(Setting.FileLanding);

                var path = $"{directory.FullName}\\{data.File}";
                File.Delete(path);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        #region Processing Models & Logic

        private static void ProcessPatientRecordsNmrsBioDataXml(string file, bool forceReplace, string notifyDestination)
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

        private static void ProcessPatientRecordsNdrBioDataCsv(string file, bool forceReplace, string notifyDestination)
        {
            var fileInfo = new FileInfo(file);

            switch (fileInfo.Extension)
            {
                case ".zip":
                    ProcessPatientRecordsNdrBioDataCsvMultiple(fileInfo, forceReplace, notifyDestination);
                    break;

                case ".csv":
                    ProcessPatientRecordsNdrBioDataCsvSingle(file, forceReplace, notifyDestination);
                    break;

                default:
                    ActivityLogger.Log("WARN", "Unsupported File Type Detected");
                    break;
            }
        }

        private static string ProcessPatientRecordsNmrsBioDataXmlSingle(string file, bool forceReplace,
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
                                    existingRecord.DataModel = (int)SecondaryBioDataSources.NmrsBioDataXml;
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
                                            DataModel = (int)SecondaryBioDataSources.NmrsBioDataXml,
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

        private static string ProcessPatientRecordsNdrBioDataCsvSingle(string file, bool forceReplace,
            string notifyDestination, bool isBulk = false)
        {
            var messageBody = "";
            if (isBulk == false)
                messageBody += $"Hello {notifyDestination},<br />";

            messageBody += $"The file {new FileInfo(file).Name} has been processed with the following results. <br />";
            List<NdrCsvPatientDemographics> records;

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, config))
                {
                    records = csv.GetRecords<NdrCsvPatientDemographics>().ToList();
                }

                foreach (var record in records)
                {
                    try
                    {
                        if (record.PatientIdentifier.Length == 11 && int.TryParse(record.PatientIdentifier.Substring(3), out int result))
                        {
                            using (var entities = new Entities())
                            {
                                var score = 0;

                                if (!string.IsNullOrEmpty(record.LeftIndex))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.LeftMiddle))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.LeftSmall))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.LeftThumb))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.LeftWedding))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.RightIndex))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.RightMiddle))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.RightSmall))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.RightThumb))
                                    score += 10;
                                if (!string.IsNullOrEmpty(record.RightWedding))
                                    score += 10;

                                if (forceReplace && entities.Patient_PatientBiometricDataSecondary
                                        .Any(x => x.PepId == record.PatientIdentifier && !x.IsDeleted))
                                {
                                    var existingRecord = entities.Patient_PatientBiometricDataSecondary
                                        .FirstOrDefault(x => x.PepId == record.PatientIdentifier
                                                             && !x.IsDeleted);

                                    existingRecord.BioDataExtract =
                                        JsonConvert.SerializeObject(new
                                        {
                                            record.LeftIndex,
                                            record.LeftMiddle,
                                            record.LeftSmall,
                                            record.LeftThumb,
                                            record.LeftWedding,
                                            record.RightIndex,
                                            record.RightMiddle,
                                            record.RightSmall,
                                            record.RightThumb,
                                            record.RightWedding
                                        });
                                    existingRecord.BioDataScore = score;
                                    existingRecord.DataModel = (int)SecondaryBioDataSources.NdrBioDataCsv;
                                    existingRecord.DataSet = JsonConvert.SerializeObject(record);
                                    existingRecord.DateRegistered = DateTime.Now;
                                    existingRecord.DateUploaded = DateTime.Now;

                                    entities.Entry(existingRecord).State = EntityState.Modified;

                                    messageBody += $"{record.PatientIdentifier} was updated successfully.<br />";
                                }
                                else if (entities.Patient_PatientBiometricDataSecondary
                                             .Any(x => x.PepId == record.PatientIdentifier && !x.IsDeleted)
                                         && !forceReplace)
                                {
                                    messageBody +=
                                        $"{record.PatientIdentifier} was not loaded because a record has been preloaded and the Force Replace flag was not set.<br />";
                                }
                                else
                                {
                                    //var facility = entities.Administration_SiteInformation
                                    //    .FirstOrDefault(x => x.SiteNameOfficial == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim() ||
                                    //    x.SiteNameInformal == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim());

                                    var facility = new Administration_SiteInformation();

                                    entities.Patient_PatientBiometricDataSecondary.Add(
                                        new Patient_PatientBiometricDataSecondary()
                                        {
                                            BioDataExtract =
                                                JsonConvert.SerializeObject(new
                                                {
                                                    record.LeftIndex,
                                                    record.LeftMiddle,
                                                    record.LeftSmall,
                                                    record.LeftThumb,
                                                    record.LeftWedding,
                                                    record.RightIndex,
                                                    record.RightMiddle,
                                                    record.RightSmall,
                                                    record.RightThumb,
                                                    record.RightWedding
                                                }),
                                            BioDataScore = score,
                                            DataModel = (int)SecondaryBioDataSources.NdrBioDataCsv,
                                            DataSet = JsonConvert.SerializeObject(record),
                                            DateRegistered = DateTime.Now,
                                            DateUploaded = DateTime.Now,
                                            IsDeleted = false,
                                            PepId = record.PatientIdentifier,
                                            FacilityId = facility?.Id ?? 0,
                                            StateId = facility?.StateId ?? 0
                                        });

                                    messageBody += $"{record.PatientIdentifier} was loaded successfully.<br />";
                                }

                                entities.SaveChanges();
                            }

                            continue;
                        }

                        messageBody += $"{record.PatientIdentifier} was not loaded. The PEPID is Invalid.";
                        ActivityLogger.Log("WARN", $"{record.PatientIdentifier} does not have a valid PEPID.");
                    }
                    catch (Exception e)
                    {
                        ActivityLogger.Log(e);
                        messageBody += $"Record {record.Pid} was not loaded. The schema seems to be Invalid.";
                        ActivityLogger.Log("WARN", $"{record.Pid} has an Invalid Schema");
                    }
                }

                ActivityLogger.Log("INFO", $"{file} has been uploaded successfully.");

                if (isBulk == false)
                    Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody,
                        null);

                records = null;
                return messageBody;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                messageBody += $"The file {new FileInfo(file).Name} was not loaded. The schema seems to be Invalid.";
                ActivityLogger.Log("WARN", $"{file} has an Invalid Schema");
                Messaging.SendMail(notifyDestination, null, null, "File Processing Status Error", messageBody, file);
            }

            records = null;
            return messageBody;
        }

        private static void ProcessPatientRecordsNmrsBioDataXmlMultiple(FileInfo fileInfo, bool forceReplace,
            string notifyDestination)
        {
            try
            {
                var extractedFiles = ExtractZip(fileInfo, ".xml");
                ActivityLogger.Log("INFO", $"Found {extractedFiles.Count} files for processing.");

                var messageBody = $"Hello {notifyDestination},<br />";
                messageBody +=
                    $"Find below the processing status of the files in the archive {fileInfo.Name}.<br /><br />";

                foreach (var extractedFile in extractedFiles)
                {
                    var subFileInfo = new FileInfo(extractedFile);

                    messageBody += $"{extractedFiles.IndexOf(extractedFile) + 1}: ";

                    if (subFileInfo.Extension == ".xml")
                        messageBody +=
                            ProcessPatientRecordsNmrsBioDataXmlSingle(extractedFile, forceReplace, notifyDestination, true);

                    else messageBody += $"{subFileInfo.Name} has an Unrecognized File Extension.";

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

        private static void ProcessPatientRecordsNdrBioDataCsvMultiple(FileInfo fileInfo, bool forceReplace,
            string notifyDestination)
        {
            try
            {
                var extractedFiles = ExtractZip(fileInfo, ".csv");
                ActivityLogger.Log("INFO", $"Found {extractedFiles.Count} files for processing.");

                var messageBody = $"Hello {notifyDestination},<br />";
                messageBody +=
                    $"Find below the processing status of the files in the archive {fileInfo.Name}.<br /><br />";

                foreach (var extractedFile in extractedFiles)
                {
                    var subFileInfo = new FileInfo(extractedFile);

                    messageBody += $"{extractedFiles.IndexOf(extractedFile) + 1}: ";

                    if (subFileInfo.Extension == ".csv")
                        messageBody +=
                            ProcessPatientRecordsNdrBioDataCsvSingle(extractedFile, forceReplace, notifyDestination, true);

                    else messageBody += $"{subFileInfo.Name} has an Unrecognized File Extension.";

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

        private static List<string> ExtractZip(FileInfo file, string extension)
        {
            try
            {
                var extractionSite = Path.Combine(file.DirectoryName, file.Name.Replace(file.Extension, ""));
                var extractionSiteInfo = new DirectoryInfo(extractionSite);

                if (extractionSiteInfo.Exists)
                    extractionSiteInfo.Delete(true);

                ZipFile.ExtractToDirectory(file.FullName, extractionSite);
                extractionSiteInfo.Attributes = FileAttributes.Normal;

                var fileList = new DirectoryInfo(extractionSite).EnumerateFiles($"*{extension}", SearchOption.AllDirectories)
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
                return new List<string>();
            }
        }

        #endregion
    }
}
