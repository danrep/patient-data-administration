using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.Mvc;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class DataProcessorHandlerController : BaseServerCommController
    {
        private List<string> _returnDataPerTable = new List<string>();

        // GET: ServerCommunication/DataProcessorHandler
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
                var directory = new System.IO.DirectoryInfo(location);

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

                        var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                        var reader = ExcelReaderFactory.CreateReader(stream);

                        if ((int)FileProcessingMethod.ExPat == fileProcessingMethod)
                        {
                            var dataSet = reader.AsDataSet();
                            dataSet.DataSetName = file;

                            ProcessPatientRecords(dataSet, forceReplace, notifyDestination);
                        }

                        if ((int)FileProcessingMethod.PhoneNumberUpdate == fileProcessingMethod)
                        {
                            var dataSet = reader.AsDataSet();
                            dataSet.DataSetName = file;

                            UpdatePatientPhoneNumber(dataSet, notifyDestination);
                        }

                        if ((int)FileProcessingMethod.DateBirthUpdate == fileProcessingMethod)
                        {
                            var dataSet = reader.AsDataSet();
                            dataSet.DataSetName = file;

                            UpdatePatientDateOfBirth(dataSet, notifyDestination);
                        }

                        reader.Close();
                        reader.Dispose();

                        stream.Close();
                        stream.Dispose();

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

        private void ProcessPatientRecords(DataSet dataSet, bool forceReplace, string notifyDestination)
        {
            try
            {
                var innerentities = new Entities();
                var returnData = new List<List<string>>();

                foreach (DataTable table in dataSet.Tables)
                {
                    _returnDataPerTable = new List<string>();

                    var site =
                        innerentities.Administration_SiteInformation.FirstOrDefault(x => x.SiteCode == table.TableName);

                    if (site == null)
                        continue;

                    foreach (DataRow dataRow in table.Rows)
                    {
                        var pepId = dataRow[2].ToString().Trim().ToUpper();

                        if (!pepId.Contains('-'))
                            continue;

                        var patientInformation =
                            innerentities.Patient_PatientInformation.FirstOrDefault(
                                x => !x.IsDeleted && x.PepId == pepId);

                        if (string.IsNullOrEmpty(dataRow[7].ToString().Trim()))
                        {
                            ActivityLogger.Log("WARN",
                                $"Error with Record {pepId} in {table.TableName}: Invalid Date of Birth [{dataRow[7]?.ToString().Trim() ?? "null"}]");
                            dataRow[7] = "...";
                        }

                        if (dataRow[7].ToString().Trim() == "0000-00-00")
                        {
                            ActivityLogger.Log("WARN",
                                $"Error with Record {pepId} in {table.TableName}: Invalid Date of Birth [{dataRow[7]?.ToString().Trim() ?? "null"}]");
                            dataRow[7] = "...";
                        }

                        if (dataRow[7].ToString().Trim().Contains("T"))
                        {
                            ActivityLogger.Log("WARN",
                                $"Error with Record {pepId} in {table.TableName}: Invalid Character found [{dataRow[7]?.ToString().Trim() ?? "null"}]");
                            dataRow[7] = dataRow[7].ToString().Trim().Replace("T00:00:00", " ").Trim();
                        }

                        if (dataRow[7].ToString().Trim().Contains("/00") ||
                            dataRow[7].ToString().Trim().Contains("00/") ||
                            dataRow[7].ToString().Trim().Contains("00-") ||
                            dataRow[7].ToString().Trim().Contains("-00"))
                        {
                            ActivityLogger.Log("WARN",
                                $"Error with Record {pepId} in {table.TableName}: Invalid Character found [{dataRow[7]?.ToString().Trim() ?? "null"}]");
                            dataRow[7] = "...";
                        }

                        try
                        {
                            if (forceReplace)
                            {
                                if (patientInformation == null)
                                {
                                    var newPatientInfo = new Patient_PatientInformation()
                                    {
                                        DateOfBirth =
                                            Transforms.NormalizeDate(dataRow[7].ToString().Trim().Split(' ')[0].Trim()),
                                        HospitalNumber = Transforms.TrimSpacesBetweenString(dataRow[3]?.ToString().Trim() ?? "NA"),
                                        HouseAddresLga = 0,
                                        HouseAddressState = 0,
                                        HouseAddress = Transforms.TrimSpacesBetweenString(dataRow.ItemArray.Length < 10 ? "NA" : dataRow[9]?.ToString().Trim() ?? "NA"),
                                        IsDeleted = false,
                                        LastUpdated = DateTime.Now,
                                        MaritalStatus = "",
                                        Othername = Transforms.TrimSpacesBetweenString(dataRow[5]?.ToString().Trim() ?? "NA"),
                                        PassportData = null,
                                        PepId = pepId,
                                        PhoneNumber =
                                            Transforms.NormalizePhoneNumber(
                                                dataRow.ItemArray.Length < 9 ? "NA" : dataRow[8]?.ToString().Trim() ?? "00000000000"),
                                        PreviousId = "",
                                        Sex = dataRow[6]?.ToString().Trim() ?? "female",
                                        SiteId = site.Id,
                                        StateOfOrigin = 0,
                                        Surname = Transforms.TrimSpacesBetweenString(dataRow[4]?.ToString().Trim() ?? "NA"),
                                        Title = "",
                                        WhenCreated = DateTime.Now
                                    };

                                    innerentities.Patient_PatientInformation.Add(newPatientInfo);
                                    innerentities.SaveChanges();
                                }
                                else
                                {
                                    try
                                    {
                                        patientInformation.DateOfBirth =
                                            Transforms.NormalizeDate(dataRow[7].ToString().Trim().Split(' ')[0].Trim());
                                        patientInformation.HospitalNumber = Transforms.TrimSpacesBetweenString(dataRow[3].ToString().Trim());
                                        patientInformation.HouseAddress = Transforms.TrimSpacesBetweenString(dataRow.ItemArray.Length < 10 ? "NA" : dataRow[9].ToString().Trim());
                                        patientInformation.LastUpdated = DateTime.Now;
                                        patientInformation.Othername = Transforms.TrimSpacesBetweenString(dataRow[5].ToString().Trim());
                                        patientInformation.PhoneNumber =
                                            Transforms.NormalizePhoneNumber(dataRow.ItemArray.Length < 9 ? "NA" : dataRow[8].ToString().Trim());
                                        patientInformation.Sex = dataRow[6].ToString().Trim();
                                        patientInformation.SiteId = site.Id;
                                        patientInformation.Surname = Transforms.TrimSpacesBetweenString(dataRow[4].ToString().Trim());

                                        innerentities.Entry(patientInformation).State = EntityState.Modified;
                                        innerentities.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        innerentities = new Entities();
                                        ActivityLogger.Log(ex);
                                        TreatProcessingError(ex, dataRow.ItemArray);
                                    }
                                }
                            }
                            else
                            {
                                var newPatientInfo = new Patient_PatientInformation()
                                {
                                    DateOfBirth =
                                        Transforms.NormalizeDate(dataRow[7].ToString().Trim().Split(' ')[0].Trim()),
                                    HospitalNumber = Transforms.TrimSpacesBetweenString(dataRow[3]?.ToString().Trim() ?? "NA"),
                                    HouseAddresLga = 0,
                                    HouseAddressState = 0,
                                    HouseAddress = Transforms.TrimSpacesBetweenString(dataRow.ItemArray.Length < 10 ? "NA" : dataRow[9]?.ToString().Trim() ?? "NA"),
                                    IsDeleted = false,
                                    LastUpdated = DateTime.Now,
                                    MaritalStatus = "",
                                    Othername = Transforms.TrimSpacesBetweenString(dataRow[5]?.ToString().Trim() ?? "NA"),
                                    PassportData = null,
                                    PepId = pepId,
                                    PhoneNumber =
                                        Transforms.NormalizePhoneNumber(
                                            dataRow.ItemArray.Length < 9 ? "NA" : dataRow[8]?.ToString().Trim() ?? "00000000000"),
                                    PreviousId = "",
                                    Sex = dataRow[6]?.ToString().Trim() ?? "female",
                                    SiteId = site.Id,
                                    StateOfOrigin = 0,
                                    Surname = Transforms.TrimSpacesBetweenString(dataRow[4]?.ToString().Trim() ?? "NA"),
                                    Title = "",
                                    WhenCreated = DateTime.Now
                                };

                                innerentities.Patient_PatientInformation.Add(newPatientInfo);
                                innerentities.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            ActivityLogger.Log(ex);
                            innerentities = new Entities();
                            TreatProcessingError(ex, dataRow.ItemArray);
                        }
                    }

                    returnData.Add(_returnDataPerTable);
                }

                innerentities.Sp_System_CleanUp();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\fileprocesscomplete.html");
                messageBody = messageBody.Replace("{{file_name}}", dataSet.DataSetName);

                if (returnData.Count > 0)
                {
                    var errors = "";
                    foreach (var returnDatum in returnData)
                    {
                        foreach (var returnDatumItem in returnDatum)
                        {
                            errors += $"{returnDatumItem}<br />";
                        }
                    }
                    messageBody = messageBody.Replace("{{table_other_info}}", errors);
                }
                else
                    messageBody = messageBody.Replace("{{table_other_info}}", "No Notable Errors");

                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private void UpdatePatientPhoneNumber(DataSet dataSet, string notifyDestination)
        {
            try
            {
                var innerentities = new Entities();
                var returnData = new List<List<string>>();

                foreach (DataTable table in dataSet.Tables)
                {
                    _returnDataPerTable = new List<string>();

                    foreach (DataRow dataRow in table.Rows)
                    {
                        var pepId = dataRow[1].ToString().Trim().ToUpper();

                        if (!pepId.Contains('-'))
                            continue;

                        try
                        {
                            var patientInformation =
                                innerentities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == pepId);

                            if (patientInformation == null)
                                continue;

                            patientInformation.PhoneNumber = Transforms.NormalizePhoneNumber(dataRow[2].ToString().Trim());
                            innerentities.Entry(patientInformation).State = EntityState.Modified;
                            innerentities.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            innerentities = new Entities();
                            ActivityLogger.Log(ex);
                            TreatProcessingError(ex, dataRow.ItemArray);
                        }
                    }

                    returnData.Add(_returnDataPerTable);
                }

                innerentities.Sp_System_CleanUp();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\fileprocesscomplete.html");
                messageBody = messageBody.Replace("{{file_name}}", dataSet.DataSetName);

                if (returnData.Count > 0)
                {
                    var errors = "";
                    foreach (var returnDatum in returnData)
                    {
                        foreach (var returnDatumItem in returnDatum)
                        {
                            errors += $"{returnDatumItem}<br />";
                        }
                    }
                    messageBody = messageBody.Replace("{{table_other_info}}", errors);
                }
                else
                    messageBody = messageBody.Replace("{{table_other_info}}", "No Notable Errors");

                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private void UpdatePatientDateOfBirth(DataSet dataSet, string notifyDestination)
        {
            try
            {
                var innerentities = new Entities();
                var returnData = new List<List<string>>();

                foreach (DataTable table in dataSet.Tables)
                {
                    _returnDataPerTable = new List<string>();

                    foreach (DataRow dataRow in table.Rows)
                    {
                        var pepId = dataRow[1].ToString().Trim().ToUpper();

                        if (!pepId.Contains('-'))
                            continue;

                        try
                        {
                            var patientInformation =
                                innerentities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == pepId);

                            if (patientInformation == null)
                                continue;

                            patientInformation.DateOfBirth =
                                Transforms.NormalizeDate(dataRow[2].ToString().Trim().Split(' ')[0].Trim());
                            innerentities.Entry(patientInformation).State = EntityState.Modified;
                            innerentities.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            innerentities = new Entities();
                            ActivityLogger.Log(ex);
                            TreatProcessingError(ex, dataRow.ItemArray);
                        }
                    }

                    returnData.Add(_returnDataPerTable);
                }

                innerentities.Sp_System_CleanUp();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\fileprocesscomplete.html");
                messageBody = messageBody.Replace("{{file_name}}", dataSet.DataSetName);

                if (returnData.Count > 0)
                {
                    var errors = "";
                    foreach (var returnDatum in returnData)
                    {
                        foreach (var returnDatumItem in returnDatum)
                        {
                            errors += $"{returnDatumItem}<br />";
                        }
                    }
                    messageBody = messageBody.Replace("{{table_other_info}}", errors);
                }
                else
                    messageBody = messageBody.Replace("{{table_other_info}}", "No Notable Errors");

                Messaging.SendMail(notifyDestination, null, null, "File Processing Status", messageBody, null);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private void TreatProcessingError(Exception e, object data)
        {
            ActivityLogger.Log(e);
            ActivityLogger.Log("Trace on Failed Upload",
                JsonConvert.SerializeObject(data));
            _returnDataPerTable.Add("Error in Data");
            _returnDataPerTable.Add(JToken.Parse(JsonConvert.SerializeObject(data)).ToString(Formatting.Indented));
        }
    }
}