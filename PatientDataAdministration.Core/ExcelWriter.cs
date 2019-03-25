using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using OfficeOpenXml;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Core
{
    public static class ExcelWriter
    {
        public static string GetFilePatientRecords(string fileName, List<ExcelPatientRecord> patientDataManifestList)
        {
            return GetFile(fileName, new List<string[]>()
            {
                new[]
                {
                    "Pep Id", "SurName", "Other Names", "Sex", "Date of Birth", "Phone Number",
                    "Date Biometric Recorded"
                }
            }, patientDataManifestList);
        }

        private static string GetFile(string fileName, IReadOnlyList<string[]> headerRow, IEnumerable<ExcelPatientRecord> patientDataManifestList)
        {
            try
            {
                var exportFilename = $"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage\\{fileName}.xlsx";

                if (File.Exists(exportFilename))
                {
                    var fileInfo = new FileInfo(exportFilename);
                    if (DateTime.Now.Subtract(fileInfo.CreationTime).TotalMinutes <= 60)
                        return exportFilename;
                }

                using (var excel = new ExcelPackage())
                {
                    foreach (var patientDataManifest in patientDataManifestList)
                    {
                        excel.Workbook.Worksheets.Add(patientDataManifest.SiteName);

                        var headerRange = "A1:" + char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                        var worksheet = excel.Workbook.Worksheets[patientDataManifest.SiteName];
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                        worksheet.Cells[headerRange].Style.Font.Bold = true;
                        worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.SteelBlue);

                        var biometricRecords = patientDataManifest
                            .PatientDataManifest.Where(x => Convert.ToBoolean(x.HasBioMetrics)).Select(x => new object[]
                            {
                                x.PepId, x.Surname, x.Othername, x.Sex, x.DateOfBirth?.ToLongDateString(),
                                x.PhoneNumber, x.BiometricRegistrationDate.ToLongDateString()
                            }).ToArray();
                        worksheet.Cells[2, 1].LoadFromArrays(new List<string[]>()
                        {
                            new[]
                            {
                                "BIOMETRIC DATA CAPTURED"
                            }
                        });
                        worksheet.Cells[headerRange].Style.Font.Bold = true;
                        worksheet.Cells[2, 1].Style.Font.Color.SetColor(System.Drawing.Color.SteelBlue);

                        worksheet.Cells[3, 1].LoadFromArrays(biometricRecords);

                        worksheet.Cells[biometricRecords.Length + 5, 1].LoadFromArrays(new List<string[]>()
                        {
                            new[]
                            {
                                "BIOMETRIC DATA NOT YET CAPTURED"
                            }
                        });
                        worksheet.Cells[biometricRecords.Length + 5, 1].Style.Font.Bold = true;
                        worksheet.Cells[biometricRecords.Length + 5, 1].Style.Font.Color.SetColor(System.Drawing.Color.DarkRed);

                        var nonBiometricRecords = patientDataManifest
                            .PatientDataManifest.Where(x => !Convert.ToBoolean(x.HasBioMetrics)).Select(x => new object[]
                            {
                                x.PepId, x.Surname, x.Othername, x.Sex, x.DateOfBirth?.ToLongDateString(),
                                x.PhoneNumber, "NOT_REGISTERED"
                            }).ToArray();
                        worksheet.Cells[6 + biometricRecords.Length, 1].LoadFromArrays(nonBiometricRecords);
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

        public class ExcelPatientRecord
        {
            public string SiteName { get; set; }
            public List<Sp_Administration_GetPatientDataManifest_Result> PatientDataManifest { get; set; }
        }
    }
}
