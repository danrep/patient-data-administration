using System;
using System.Collections.Generic;
using System.Linq;
using Codesistance.UniqueBioSearchSecugen;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.DeduplicationEngine.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioDataSecondary
    {
        public static List<Patient_PatientBiometricSecondaryIntegrityCase> BioDataIntegrityCases { get; set; }

        public static bool IsProcessing { get; set; }

        public static bool IsAlive { get; set; }

        private static string TraceId { get; set; }

        public static void ProcessDataIntegrityBiometric()
        {
            IsProcessing = true;
            IsAlive = true;
            TraceId = Guid.NewGuid().ToString().ToUpper().Replace('-', '0');

            if (BioDataIntegrityCases == null)
                BioDataIntegrityCases = new List<Patient_PatientBiometricSecondaryIntegrityCase>();

            //Load Pending Cases
            RefreshBioDataIntegrityCases();

            //Run Biometric Check
            ActivityLogger.Log("INFO", $"Starting Secondary Dedup Engine Session with ID {TraceId}");
            RunBiometricCheck();
            ActivityLogger.Log("INFO", $"Completing Secondary Dedup Engine Session with ID {TraceId}");

            IsProcessing = false;
        }

        private static void RefreshBioDataIntegrityCases()
        {
            try
            {
                using (var entities = new Entities())
                {
                    BioDataIntegrityCases = entities.Patient_PatientBiometricSecondaryIntegrityCase
                        .Where(x => !x.IsDeleted && x.CaseStatus == (int) CaseStatus.Open).ToList();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void RunBiometricCheck()
        {
            try
            {
                using (var entities = new Entities())
                {
                    //var patientsWithUnresolvedCases = entities.Patient_PatientBiometricSecondaryIntegrityCaseMember
                    //    .Where(x => !x.IsDeleted && !x.IsTreated).Select(x => x.SuspectPepId).ToList();

                    var limiter = DateTime.Now.AddHours(-7 * 24);
                    var allPatientBiometrics = new List<PatientData>();

                    #region Secondary Processing

                    if (!entities.Patient_PatientBiometricDataSecondary.Any(x => !x.IsDeleted && x.DateUploaded > limiter))
                        return;

                    var allSecondaryBiometrics = entities.Patient_PatientBiometricDataSecondary
                        .Where(x => !x.IsDeleted && x.DateUploaded > limiter && x.BioDataScore >= Setting.DedupBioDataScore)
                        .OrderBy(x => Guid.NewGuid())
                        .ToList();

                    ActivityLogger.Log("INFO", $"{TraceId}: Loaded {allSecondaryBiometrics.Count} Patients");
                    foreach (var secondaryBiometrics in allSecondaryBiometrics)
                    {
                        allPatientBiometrics.AddRange(Resolvers.ResolveSecondaryBioData(secondaryBiometrics));
                    }

                    #endregion

                    // select random candidates
                    var patientBiometricDataChunks =
                        Transforms.ListChunk(allPatientBiometrics.OrderBy(x => Guid.NewGuid()).ToList(), Setting.DedupProcLimit);
                    
                    ActivityLogger.Log("INFO", $"{TraceId}: Processing {allPatientBiometrics.Count} in {patientBiometricDataChunks.Count} chunks");

                    var biomtricSearchEngine = new SearchEngine();

                    foreach (var patientBiometricDataChunk in patientBiometricDataChunks)
                    {
                        try
                        {
                            ActivityLogger.Log("INFO", $"{TraceId}: Current Chunk Member Size is {patientBiometricDataChunk.Count}");

                            if (!IsAlive)
                                break;

                            biomtricSearchEngine.LoadTemplates(patientBiometricDataChunk);

                            var resultSet = biomtricSearchEngine.BulkProcess();

                            if (!IsAlive)
                                break;

                            if (resultSet == null)
                                continue;

                            foreach (var result in resultSet)
                            {
                                var integrityCase =
                                    BioDataIntegrityCases.FirstOrDefault(x =>
                                        x.PivotPepId == result.Pivot);

                                //gunning for a 90% above match
                                var validCases = result.SuspectedCandidates
                                    .Where(x => x.BioDataSuspect.Filename != result.Pivot && x.MatchScore >= Setting.DedupMatchScore)
                                    .ToList();

                                ActivityLogger.Log("INFO", $"{TraceId}: Found {validCases.Count} Relevant Matches");

                                if (validCases.Any())
                                {
                                    foreach (var validCase in validCases)
                                    {
                                        if (integrityCase == null)
                                        {
                                            integrityCase =
                                                new Patient_PatientBiometricSecondaryIntegrityCase()
                                                {
                                                    CaseStatus = (int)CaseStatus.Open,
                                                    DateGenerated = DateTime.Now,
                                                    PivotPepId = result.Pivot,
                                                    IsDeleted = false
                                                };
                                            entities.Patient_PatientBiometricSecondaryIntegrityCase.Add(integrityCase);
                                            entities.SaveChanges();
                                        }

                                        if (entities.Patient_PatientBiometricSecondaryIntegrityCaseMember.Any(x =>
                                            !x.IsDeleted && !x.IsTreated &&
                                            x.PivotPepId == result.Pivot &&
                                            x.SuspectPepId == validCase.BioDataSuspect.Filename)) continue;

                                        entities.Patient_PatientBiometricSecondaryIntegrityCaseMember.Add(
                                            new Patient_PatientBiometricSecondaryIntegrityCaseMember()
                                            {
                                                SuspectPepId = validCase.BioDataSuspect.Filename,
                                                IsDeleted = false,
                                                DateTreated = DateTime.Now,
                                                IsTreated = false,
                                                PatientBiometricIntegrityCaseId = integrityCase.Id,
                                                PivotPepId = result.Pivot,
                                                MatchingScore = validCase.MatchScore,
                                                MemberTreatmentTypeId = (int)CaseMemberStatus.Undecided,
                                                PivotData = Newtonsoft.Json.JsonConvert.SerializeObject(result.PivotData),
                                                SuspectData = Newtonsoft.Json.JsonConvert.SerializeObject((PatientData)validCase.BioDataSuspect.Data)
                                            });

                                        entities.SaveChanges();
                                    }
                                }

                                RefreshBioDataIntegrityCases();
                            }

                            resultSet = null;
                            biomtricSearchEngine.Clear();
                            ActivityLogger.Log("INFO", $"{TraceId}: Current Chunk Member Size is {patientBiometricDataChunk.Count} is Complete");
                        }
                        catch (Exception e)
                        {
                            ActivityLogger.Log(e);
                        }
                    }

                    biomtricSearchEngine.DeInitialize();
                    ActivityLogger.Log("INFO", $"{TraceId}: Processing is Completed at this time");

                    patientBiometricDataChunks.Clear();
                    patientBiometricDataChunks = null;

                    allPatientBiometrics.Clear();
                    allPatientBiometrics = null;

                    ActivityLogger.Log("INFO", $"{TraceId}: Temp Data Cleared");

                    RefreshBioDataIntegrityCases();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static void KillProcessing()
        {
            IsAlive = false;
        }
    }
}