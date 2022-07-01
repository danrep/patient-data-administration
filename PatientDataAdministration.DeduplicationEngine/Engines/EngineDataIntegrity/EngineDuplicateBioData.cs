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
    public class EngineDuplicateBioData
    {
        public static List<Patient_PatientBiometricIntegrityCase> BioDataIntegrityCases { get; set; }

        public static bool IsProcessing { get; set; }

        public static bool IsAlive { get; set; }

        private static string TraceId { get; set; }

        public static void ProcessDataIntegrityBiometric()
        {
            IsProcessing = true;
            IsAlive = true;
            TraceId = Guid.NewGuid().ToString().ToUpper().Replace('-', '0');

            if (BioDataIntegrityCases == null)
                BioDataIntegrityCases = new List<Patient_PatientBiometricIntegrityCase>();

            //Load Pending Cases
            RefreshBioDataIntegrityCases();

            //Run Biometric Check
            ActivityLogger.Log("INFO", $"Starting Primary Dedup Engine Session with ID {TraceId}");
            RunBiometricCheck();
            ActivityLogger.Log("INFO", $"Completing Primary Dedup Engine Session with ID {TraceId}");

            IsProcessing = false;
        }

        private static void RefreshBioDataIntegrityCases()
        {
            try
            {
                using (var entities = new Entities())
                {
                    BioDataIntegrityCases = entities.Patient_PatientBiometricIntegrityCase
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
                    var patientsWithUnresolvedCases = entities.Patient_PatientBiometricIntegrityCaseMember
                        .Where(x => !x.IsDeleted && !x.IsTreated).Select(x => x.SuspectPepId).ToList();

                    var limiter = DateTime.Now.AddHours(-7 * 24);
                    var allPatientBiometrics = new List<PatientData>();

                    #region Primary Processing

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && x.DateRegistered > limiter && !patientsWithUnresolvedCases.Contains(x.PepId)).Select(
                            x => new PatientData()
                            {
                                RowId = x.Id,
                                PepId = x.PepId,
                                FingerPrintData = x.FingerPrimary,
                                FingerPosition = FingerPrintPosition.LeftThumb, 
                                FingerPrintStore = FingerPrintStore.Primary
                            }));

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && x.DateRegistered > limiter && !patientsWithUnresolvedCases.Contains(x.PepId)).Select(
                            x => new PatientData()
                            {
                                RowId = x.Id,
                                PepId = x.PepId,
                                FingerPrintData = x.FingerSecondary,
                                FingerPosition = FingerPrintPosition.RightThumb,
                                FingerPrintStore = FingerPrintStore.Primary
                            }));
					
					ActivityLogger.Log("INFO", $"{TraceId}: Loaded {allPatientBiometrics.Count} Patients");
					
                    #endregion

                    // select random candidates
                    var patientBiometricDataChunks = Transforms.ListChunk(allPatientBiometrics.OrderBy(x => Guid.NewGuid()).ToList(), 800);
                    
                    ActivityLogger.Log("INFO", $"{TraceId}: Processing {allPatientBiometrics.Count} in {patientBiometricDataChunks.Count} chunks");

                    var biomtricSearchEngine = new SearchEngine();

                    foreach (var patientBiometricDataChunk in patientBiometricDataChunks)
                    {
                        try
                        {
                            ActivityLogger.Log("INFO", $"Current Chunk Size is {patientBiometricDataChunks.Count}");

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
                                    .Where(x => x.BioDataSuspect.Filename != result.Pivot && x.MatchScore >= 9000)
                                    .ToList();

                                ActivityLogger.Log("INFO", $"{TraceId}: Found {validCases.Count} Relevant Matches");

                                if (validCases.Any())
                                {
                                    foreach (var validCase in validCases)
                                    {
                                        if (integrityCase == null)
                                        {
                                            integrityCase =
                                                new Patient_PatientBiometricIntegrityCase()
                                                {
                                                    CaseStatus = (int)CaseStatus.Open,
                                                    DateGenerated = DateTime.Now,
                                                    PivotPepId = result.Pivot,
                                                    IsDeleted = false
                                                };
                                            entities.Patient_PatientBiometricIntegrityCase.Add(integrityCase);
                                            entities.SaveChanges();
                                        }

                                        if (!entities.Patient_PatientBiometricIntegrityCaseMember.Any(x =>
                                            !x.IsDeleted && !x.IsTreated &&
                                            x.PivotPepId == result.Pivot &&
                                            x.SuspectPepId == validCase.BioDataSuspect.Filename))
                                        {
                                            entities.Patient_PatientBiometricIntegrityCaseMember.Add(
                                                new Patient_PatientBiometricIntegrityCaseMember()
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
                                }
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