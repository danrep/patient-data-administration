﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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

                    var biomtricSearchEngine = new SearchEngine();

                    #region Secondary Processing

                    var allSecondaryBiometrics = new List<Patient_PatientBiometricDataSecondary>();
                    var allPatientBiometrics = new List<PatientData>();

                    var states = entities.Patient_PatientBiometricDataSecondary
                        .Where(x => x.StateId != 0)
                        .Select(x => x.StateId).Distinct().ToList();

                    foreach(var state in states)
                    {
                        var stateName = entities.System_State.FirstOrDefault(x => x.Id == state)?.StateName ?? "GLOBAL";
                        ActivityLogger.Log("INFO", $"{TraceId}: Entering {stateName} State");

                        var records = entities.Patient_PatientBiometricDataSecondary
                            .Where(x => !x.IsDeleted && x.BioDataScore >= Setting.DedupBioDataScore && x.StateId == state)
                            .OrderBy(x => Guid.NewGuid());

                        ActivityLogger.Log("INFO", $"{TraceId}: Commence Loading of Secondary Biometrics Patients");

                        for (int i = 0; i <= records.Count(); i += Setting.DedupDataLimit)
                        {
                            try
                            {
                                allPatientBiometrics.Clear();
                                allSecondaryBiometrics = records.Skip(i).Take(Setting.DedupDataLimit).ToList();

                                ActivityLogger.Log("INFO", $"{TraceId}: Loaded {allSecondaryBiometrics.Count} Patients Currently");

                                foreach (var secondaryBiometrics in allSecondaryBiometrics)
                                {
                                    allPatientBiometrics.AddRange(Resolvers.ResolveSecondaryBioData(secondaryBiometrics));
                                }

                                ActivityLogger.Log("INFO", $"{TraceId}: Loaded {allPatientBiometrics.Count} Patients Biomatric Templates Currently");

                                var patientBiometricDataChunks =
                                    Transforms.ListChunk(allPatientBiometrics, Setting.DedupProcLimit);

                                ActivityLogger.Log("INFO", $"{TraceId}: Processing {allPatientBiometrics.Count} in {patientBiometricDataChunks.Count} chunks");

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

                                                    //if (entities.Patient_PatientBiometricSecondaryIntegrityCaseMember.Any(x =>
                                                    //    !x.IsDeleted && !x.IsTreated &&
                                                    //    x.PivotPepId == validCase.BioDataSuspect.Filename &&
                                                    //    x.SuspectPepId == result.Pivot)) continue;

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

                                patientBiometricDataChunks.Clear();
                                patientBiometricDataChunks = null;

                                ActivityLogger.Log("INFO", $"{TraceId}: Processing is Completed at this time for batch at {i}");
                            }
                            catch (EntityCommandExecutionException ecee)
                            {
                                ActivityLogger.Log(ecee);
                                i -= Setting.DedupDataLimit;
                                ActivityLogger.Log("INFO", $"{TraceId}: Retrying batch due to EntityCommandExecutionException on {stateName} State");
                            }
                            catch (Exception e)
                            {
                                ActivityLogger.Log(e);
                            }
                        }

                        ActivityLogger.Log("INFO", $"{TraceId}: Leaving {stateName} State");
                    }
                    
                    #endregion

                    biomtricSearchEngine.DeInitialize();
                    ActivityLogger.Log("INFO", $"{TraceId}: Processing is Completed at this time");

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