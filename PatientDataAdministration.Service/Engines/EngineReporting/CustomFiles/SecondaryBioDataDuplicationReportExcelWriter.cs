﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml;
using PatientDataAdministration.Core;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Service.Engines.EngineReporting.CustomFiles
{
    public static class SecondaryBioDataDuplicationReportExcelWriter
    {
        public static string GetFile(string fileName, IEnumerable<SecondaryBioDataDuplicationReport> secondaryBioData, 
            Dictionary<int, string> stateData, Dictionary<int, string> lgaData)
        {
            try
            {
                var basePath = Setting.FileLanding;

                var localDirectory =
                    new DirectoryInfo($"{basePath}\\LocalFileStorage");

                string stateName, lgaName;

                if (!localDirectory.Exists)
                    localDirectory.Create();

                var exportFilename = $"{basePath}\\LocalFileStorage\\{fileName}.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excel = new ExcelPackage())
                {
                    var worksheetName = "CASE_FILE_" + DateTime.Now.ToString("yyyyMMdd");
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];

                    // SN Cell Merger 
                    worksheet.Cells[1, 1, 2, 1].Merge = true;
                    worksheet.Cells[1, 1, 2, 1].LoadFromText("S/N");
                    
                    //Suspect Header Merger 
                    worksheet.Cells[1, 2, 1, 8].Merge = true;
                    worksheet.Cells[1, 2, 1, 8].LoadFromText("SUSPECTED DUPLICATION CASE");

                    //Match Header Merger 
                    worksheet.Cells[1, 9, 1, 16].Merge = true;
                    worksheet.Cells[1, 9, 1, 16].LoadFromText("SUSPECTED MATCH CASE");

                    var subHeaders = new List<string[]>()
                    {
                        new[]
                        {
                            "PEP ID", "State", "LGA", "DATIM Code", "Facility", "ART Start Date", "Last Pick Up Date"
                        }
                    };
                    worksheet.Cells[2, 2, 2, 8].LoadFromArrays(subHeaders);
                    worksheet.Cells[2, 9, 2, 15].LoadFromArrays(subHeaders);

                    subHeaders = new List<string[]>()
                    {
                        new[]
                        {
                            "% Match"
                        }
                    };
                    worksheet.Cells[2, 16, 2, 16].LoadFromArrays(subHeaders);

                    var suspectRows = 3;
                    var pivotRows = 1;

                    foreach (var secondaryBioDatum in secondaryBioData)
                    {
                        try
                        {
                            worksheet.Cells[suspectRows, 1].LoadFromText($"{pivotRows}");
                            worksheet.Cells[suspectRows, 2].LoadFromText($"{secondaryBioDatum.PivotData.PepId}");

                            stateData.TryGetValue(secondaryBioDatum.PivotData.StateId, out stateName);
                            lgaData.TryGetValue(secondaryBioDatum.PivotData.FacilityId, out lgaName);

                            worksheet.Cells[suspectRows, 3].LoadFromText($"{stateName ?? "NA"}");
                            worksheet.Cells[suspectRows, 4].LoadFromText($"{lgaName ?? "NA"}");

                            if ((SecondaryBioDataSources)secondaryBioDatum.PivotData.DataModel == SecondaryBioDataSources.NmrsBioDataXml)
                            {
                                var pivotXmlData = JsonConvert.DeserializeXmlNode(secondaryBioDatum.PivotData.DataSet);
                                var currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("TreatmentFacility")[0],
                                    Formatting.None, true);
                                var currentSection =
                                    JsonConvert.DeserializeObject<dynamic>(currentSectionJson);

                                if (currentSection != null)
                                {
                                    worksheet.Cells[suspectRows, 5].LoadFromText($"{currentSection["FacilityID"]?.ToString() ?? "NA"}");
                                    worksheet.Cells[suspectRows, 6].LoadFromText($"{currentSection["FacilityName"]?.ToString() ?? "NA"}");
                                }

                                currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("HIVQuestions")[0],
                                    Formatting.None, true);
                                currentSection =
                                    JsonConvert.DeserializeObject<dynamic>(currentSectionJson);

                                if (currentSection != null)
                                {
                                    worksheet.Cells[suspectRows, 7].Style.Numberformat.Format = "@";
                                    worksheet.Cells[suspectRows, 7].LoadFromText($"[{currentSection["ARTStartDate"]?.ToString() ?? "NA"}]");
                                }
                            }

                            worksheet.Cells[suspectRows, 8].LoadFromText($"---");

                            foreach (var caseMember in secondaryBioDatum.CaseMembers)
                            {
                                worksheet.Cells[suspectRows, 9].LoadFromText($"{caseMember.SuspectData.PepId}");

                                stateData.TryGetValue(caseMember.SuspectData.StateId, out stateName);
                                lgaData.TryGetValue(caseMember.SuspectData.StateId, out lgaName);

                                worksheet.Cells[suspectRows, 10].LoadFromText($"{stateName ?? "NA"}");
                                worksheet.Cells[suspectRows, 11].LoadFromText($"{lgaName ?? "NA"}");

                                if ((SecondaryBioDataSources)caseMember.SuspectData.DataModel == SecondaryBioDataSources.NmrsBioDataXml)
                                {
                                    var suspectXmlData = JsonConvert.DeserializeXmlNode(caseMember.SuspectData.DataSet);
                                    var currentSectionJson = JsonConvert.SerializeXmlNode(suspectXmlData.GetElementsByTagName("TreatmentFacility")[0],
                                        Formatting.None, true);
                                    var currentSection =
                                        JsonConvert.DeserializeObject<dynamic>(currentSectionJson);

                                    if (currentSection != null)
                                    {
                                        worksheet.Cells[suspectRows, 12].LoadFromText($"{currentSection["FacilityID"]?.ToString() ?? "NA"}");
                                        worksheet.Cells[suspectRows, 13].LoadFromText($"{currentSection["FacilityName"]?.ToString() ?? "NA"}");
                                    }

                                    currentSectionJson = JsonConvert.SerializeXmlNode(suspectXmlData.GetElementsByTagName("HIVQuestions")[0],
                                        Formatting.None, true);
                                    currentSection =
                                        JsonConvert.DeserializeObject<dynamic>(currentSectionJson);

                                    if (currentSection != null)
                                    {
                                        worksheet.Cells[suspectRows, 14].Style.Numberformat.Format = "@";
                                        worksheet.Cells[suspectRows, 14].LoadFromText($"[{currentSection["ARTStartDate"]?.ToString() ?? "NA"}]");
                                    }
                                }                                

                                worksheet.Cells[suspectRows, 15].LoadFromText($"---");
                                worksheet.Cells[suspectRows, 16].LoadFromText($"{caseMember.CaseMember.MatchingScore / 100}");

                                suspectRows++;
                            }

                            pivotRows++;
                        }
                        catch (Exception e)
                        {
                            ActivityLogger.Log(e);
                            worksheet.Cells[suspectRows, 1].LoadFromText($"ERR000");

                            for(var i=2; i <= 16; i++)
                                worksheet.Cells[suspectRows, i].LoadFromText($"*");
                        }
                    }

                    var excelFile = new FileInfo(exportFilename);
                    excel.SaveAs(excelFile);
                }

                return exportFilename;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }
    }
}
