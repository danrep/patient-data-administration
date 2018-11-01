using System;
using System.Collections.Generic;
using System.Linq;
using Codesistance.UniqueBioSearchSecugen;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Service.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioData
    {
        private static List<Patient_PatientBiometricIntegrityCase> BioDataIntegrityCases { get; set; }

        public static bool IsProcessing { get; set; }

        public static bool IsAlive { get; set; }

        public static void ProcessDataIntegrityBiometric()
        {
            IsProcessing = true;
            IsAlive = true;

            if (BioDataIntegrityCases == null)
                BioDataIntegrityCases = new List<Patient_PatientBiometricIntegrityCase>();

            //Load Pending Cases
            RefreshBioDataIntegrityCases();

            //Run Biomeetric Check
            RunBiometricCheck();

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

                    var allPatientBiometrics = new List<PatientData>();

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && !patientsWithUnresolvedCases.Contains(x.PepId)).Select(
                            x => new PatientData()
                            {
                                PepId = x.PepId,
                                FingerPrintData = x.FingerPrimary,
                                FingerPosition = FingerPrintPosition.Left
                            }));

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && !patientsWithUnresolvedCases.Contains(x.PepId)).Select(
                            x => new PatientData()
                            {
                            PepId = x.PepId,
                            FingerPrintData = x.FingerSecondary,
                            FingerPosition = FingerPrintPosition.Right
                        }));

                    var patientBiometricDataChunks = Transforms.ListChunk(allPatientBiometrics.OrderBy(x => Guid.NewGuid()).ToList(), 800);
                    
                    ActivityLogger.Log("INFO", $"Processing {allPatientBiometrics.Count} in {patientBiometricDataChunks.Count}");

                    foreach (var patientBiometricDataChunk in patientBiometricDataChunks)
                    {
                        ActivityLogger.Log("INFO", $"Current Chunk Size is {patientBiometricDataChunks.Count}");

                        if (!IsAlive)
                            break;

                        var biomtricSearchEngine = new SearchEngine();
                        biomtricSearchEngine.LoadTemplates(patientBiometricDataChunk);

                        var resultSet = biomtricSearchEngine.Process();
                        biomtricSearchEngine.DeInitialize();

                        if (!IsAlive)
                            break;

                        foreach (var result in resultSet)
                        {
                            var integrityCase =
                                BioDataIntegrityCases.FirstOrDefault(x =>
                                    x.PivotPepId == result.Pivot);

                            //gunning for a 60% above match
                            var validCases = result.SuspectedCandidates
                                .Where(x => x.BioDataSuspect.Filename != result.Pivot && x.MatchScore >= 6000).ToList();

                            ActivityLogger.Log("INFO", $"Found {validCases.Count} Relevant Matches");

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
                                                MemberTreatmentTypeId = (int)CaseMemberStatus.Undecided
                                            });

                                        entities.SaveChanges();
                                    }
                                }
                            }
                        }

                        biomtricSearchEngine = null;

                        GC.Collect();
                    }

                    ActivityLogger.Log("INFO", $"Processing is Completed at this time");

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