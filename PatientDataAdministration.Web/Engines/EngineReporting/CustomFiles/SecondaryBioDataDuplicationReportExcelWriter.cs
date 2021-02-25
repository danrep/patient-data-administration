using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;
using OfficeOpenXml;
using PatientDataAdministration.Core;

namespace PatientDataAdministration.Web.Engines.EngineReporting.CustomFiles
{
    public static class SecondaryBioDataDuplicationReportExcelWriter
    {
        public static string GetFile(string fileName, IEnumerable<SecondaryBioDataDuplicationReport> secondaryBioData)
        {
            try
            {
                var localDirectory =
                    new DirectoryInfo($"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage");

                if (!localDirectory.Exists)
                    localDirectory.Create();

                var exportFilename = $"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage\\{fileName}.xlsx";

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

                            worksheet.Cells[suspectRows, 3].LoadFromText($"---");
                            worksheet.Cells[suspectRows, 4].LoadFromText($"---");
                            worksheet.Cells[suspectRows, 5].LoadFromText($"---");

                            var pivotXmlData = JsonConvert.DeserializeXmlNode(secondaryBioDatum.PivotData.DataSet);

                            var currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("TreatmentFacility")[0],
                                Formatting.None, true);
                            var currentSection =
                                JsonConvert.DeserializeObject<dynamic>(currentSectionJson);
                            if (currentSection != null)
                                worksheet.Cells[suspectRows, 6].LoadFromText($"{currentSection["FacilityName"]?.ToString() ?? "NA"}");

                            currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("HIVQuestions")[0],
                                Formatting.None, true);
                            currentSection =
                                JsonConvert.DeserializeObject<dynamic>(currentSectionJson);
                            if (currentSection != null)
                            {
                                worksheet.Cells[suspectRows, 7].Style.Numberformat.Format = "@";
                                worksheet.Cells[suspectRows, 7].LoadFromText($"[{currentSection["ARTStartDate"]?.ToString() ?? "NA"}]");
                            }

                            worksheet.Cells[suspectRows, 8].LoadFromText($"---");

                            foreach (var caseMember in secondaryBioDatum.CaseMembers)
                            {
                                worksheet.Cells[suspectRows, 9].LoadFromText($"{caseMember.SuspectData.PepId}");

                                worksheet.Cells[suspectRows, 10].LoadFromText($"---");
                                worksheet.Cells[suspectRows, 11].LoadFromText($"---");
                                worksheet.Cells[suspectRows, 12].LoadFromText($"---");

                                pivotXmlData = JsonConvert.DeserializeXmlNode(caseMember.SuspectData.DataSet);

                                currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("TreatmentFacility")[0],
                                    Formatting.None, true);
                                currentSection =
                                    JsonConvert.DeserializeObject<dynamic>(currentSectionJson);
                                if (currentSection != null)
                                    worksheet.Cells[suspectRows, 13].LoadFromText($"{currentSection["FacilityName"]?.ToString() ?? "NA"}");

                                currentSectionJson = JsonConvert.SerializeXmlNode(pivotXmlData.GetElementsByTagName("HIVQuestions")[0],
                                    Formatting.None, true);
                                currentSection =
                                    JsonConvert.DeserializeObject<dynamic>(currentSectionJson);
                                if (currentSection != null)
                                {
                                    worksheet.Cells[suspectRows, 14].Style.Numberformat.Format = "@"; 
                                    worksheet.Cells[suspectRows, 14].LoadFromText($"[{currentSection["ARTStartDate"]?.ToString() ?? "NA"}]");
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
