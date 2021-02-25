using System;
using System.Collections.Generic;
using System.Linq;
using Codesistance.UniqueBioSearchSecugen;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.SecondaryBioDataModels;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Service.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioDataSecondary
    {
        private static List<Patient_PatientBiometricSecondaryIntegrityCase> BioDataIntegrityCases { get; set; }

        public static bool IsProcessing { get; set; }

        public static bool IsAlive { get; set; }

        public static void ProcessDataIntegrityBiometric()
        {
            IsProcessing = true;
            IsAlive = true;

            if (BioDataIntegrityCases == null)
                BioDataIntegrityCases = new List<Patient_PatientBiometricSecondaryIntegrityCase>();

            //Load Pending Cases
            RefreshBioDataIntegrityCases();

            //Run Biometric Check
            RunBiometricCheck();

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
                    var patientsWithUnresolvedCases = entities.Patient_PatientBiometricSecondaryIntegrityCaseMember
                        .Where(x => !x.IsDeleted && !x.IsTreated).Select(x => x.SuspectPepId).ToList();

                    var allPatientBiometrics = new List<PatientData>();

                    #region Secondary Processing

                    var now = DateTime.Now;
                    var limiter = new DateTime(now.Year, now.Month, 01);

                    var allSecondaryBiometrics = entities.Patient_PatientBiometricDataSecondary
                        .Where(x => !x.IsDeleted && x.DateUploaded > limiter)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(1000)
                        .ToList();

                    foreach(var secondaryBiometrics in allSecondaryBiometrics)
                    {
                        allPatientBiometrics.AddRange(ResolveSecondaryBioData(secondaryBiometrics));
                    }

                    #endregion

                    // select random candidates
                    var patientBiometricDataChunks =
                        Transforms.ListChunk(allPatientBiometrics.OrderBy(x => Guid.NewGuid()).ToList(), 200);
                    
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
                                .Where(x => x.BioDataSuspect.Filename != result.Pivot && x.MatchScore >= 7000)
                                .ToList();

                            ActivityLogger.Log("INFO", $"Found {validCases.Count} Relevant Matches");

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

                        biomtricSearchEngine = null;
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

        private static List<PatientData> ResolveSecondaryBioData(Patient_PatientBiometricDataSecondary secondaryData)
        {
            try
            {
                switch ((SecondaryBioDataSources)secondaryData.DataModel)
                {
                    case SecondaryBioDataSources.NmrsBioDataXml:
                        return ResolveNmrsBioDataXml(secondaryData.BioDataExtract, 
                            secondaryData.PepId, 
                            secondaryData.Id);

                    default:
                        return new List<PatientData>();
                }

            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        } 

        private static List<PatientData> ResolveNmrsBioDataXml(string bioDataExtract, string pepId, long rowId)
        {
            try
            {
                var patientData = new List<PatientData>();

                var fingerPrints = Newtonsoft.Json.JsonConvert.DeserializeObject<FingerPrints>(bioDataExtract);

                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftIndex))
                    patientData.Add(new PatientData() {
                        FingerPosition = FingerPrintPosition.LeftIndex,
                        FingerPrintData = fingerPrints.LeftHand.LeftIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId, 
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftMiddle,
                        FingerPrintData = fingerPrints.LeftHand.LeftMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftSmall,
                        FingerPrintData = fingerPrints.LeftHand.LeftSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftThumb,
                        FingerPrintData = fingerPrints.LeftHand.LeftThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.LeftHand?.LeftWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.LeftRing,
                        FingerPrintData = fingerPrints.LeftHand.LeftWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightIndex))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightIndex,
                        FingerPrintData = fingerPrints.RightHand.RightIndex,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightMiddle))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightMiddle,
                        FingerPrintData = fingerPrints.RightHand.RightMiddle,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightSmall))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightSmall,
                        FingerPrintData = fingerPrints.RightHand.RightSmall,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightThumb))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightThumb,
                        FingerPrintData = fingerPrints.RightHand.RightThumb,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });
                if (!string.IsNullOrEmpty(fingerPrints.RightHand?.RightWedding))
                    patientData.Add(new PatientData()
                    {
                        FingerPosition = FingerPrintPosition.RightRing,
                        FingerPrintData = fingerPrints.RightHand.RightWedding,
                        FingerPrintStore = FingerPrintStore.Secondary,
                        PepId = pepId,
                        RowId = rowId
                    });

                return patientData;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        public static void KillProcessing()
        {
            IsAlive = false;
        }
    }
}