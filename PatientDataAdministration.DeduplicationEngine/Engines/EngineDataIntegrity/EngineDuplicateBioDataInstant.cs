using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Codesistance.UniqueBioSearchSecugen;
using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Core.PubSub;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using StackExchange.Redis;

namespace PatientDataAdministration.DeduplicationEngine.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioDataInstant
    {
        private List<PatientData> PatientData;
        private SearchEngine BiomtricSearchEngine;

        public EngineDuplicateBioDataInstant()
        {
            ActivityLogger.Log("INFO", $"Starting Instant Dedup Engine");

            PatientData = new List<PatientData>();

            new Thread(()=> {
                PatientData.AddRange(LoadPrimary());
                PatientData.AddRange(LoadSecondary());
            }).Start();

            BiomtricSearchEngine = new SearchEngine();
        }

        ~EngineDuplicateBioDataInstant()
        {
            PatientData = new List<PatientData>();
            BiomtricSearchEngine.Clear();
            BiomtricSearchEngine.DeInitialize();
        }

        private List<PatientData> LoadPrimary()
        {
            try
            {
                using (var entities = new Entities())
                {
                     var allPatientBiometrics = new List<PatientData>();

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && entities.Patient_PatientInformation.Any(y => y.PepId == x.PepId))
                        .Take(Setting.DedupDataLimit / 4)
                        .Select(
                            x => new PatientData()
                            {
                                RowId = x.Id,
                                PepId = x.PepId,
                                FingerPrintData = x.FingerPrimary,
                                FingerPosition = FingerPrintPosition.LeftThumb,
                                FingerPrintStore = FingerPrintStore.Primary, 
                                BioDataSource = 0
                            }));

                    allPatientBiometrics.AddRange(entities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && x.IsValid && entities.Patient_PatientInformation.Any(y => y.PepId == x.PepId))
                        .Take(Setting.DedupDataLimit / 4)
                        .Select(
                            x => new PatientData()
                            {
                                RowId = x.Id,
                                PepId = x.PepId,
                                FingerPrintData = x.FingerSecondary,
                                FingerPosition = FingerPrintPosition.RightThumb,
                                FingerPrintStore = FingerPrintStore.Primary, 
                                BioDataSource = 0
                            }));

                    ActivityLogger.Log("INFO", $"Loaded {allPatientBiometrics.Count} Primary Patient Templates");

                    return allPatientBiometrics;
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        private List<PatientData> LoadSecondary()
        {
            try
            {
                using (var entities = new Entities())
                {
                    var allPatientBiometrics = new List<PatientData>();

                    var allSecondaryBiometrics = entities.Patient_PatientBiometricDataSecondary
                        .Where(x => !x.IsDeleted && x.BioDataScore >= Setting.DedupBioDataScore)
                        .Select(x => new 
                        {
                            x.Id,
                            x.PepId,
                            x.BioDataExtract, 
                            x.DataModel
                        })
                        .OrderBy(x => Guid.NewGuid())
                        .Take(Setting.DedupDataLimit / 20)
                        .ToList();

                    foreach (var secondaryBiometrics in allSecondaryBiometrics)
                    {
                        allPatientBiometrics.AddRange(Resolvers.ResolveSecondaryBioData(secondaryBiometrics));

                        if (allPatientBiometrics.Count > Setting.DedupDataLimit / 2)
                        {
                            ActivityLogger.Log("WARNING", $"250000 Patient Templates Exceeded");
                            break;
                        }
                    }

                    allSecondaryBiometrics = null;
                    ActivityLogger.Log("INFO", $"Loaded {allPatientBiometrics.Count} Secondary Patient Templates");
                    GC.Collect();
                    return allPatientBiometrics;
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PatientData>();
            }
        }

        public PubSubResponse ReceiveMessage(ChannelMessage channelMessage)
        {
            try
            {
                ActivityLogger.Log("INFO>>IDUP", $"Received Message: {channelMessage.Message}");
                var message = JsonConvert.DeserializeObject<CommunicationModel>(channelMessage.Message.ToString());
                DedupSubmission data = JsonConvert.DeserializeObject<DedupSubmission>(message.Data);

                new Thread(() => Process(data.OperationId, data.PatientDataSubmitted)).Start();

                return PubSubResponse.Success;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return PubSubResponse.Error;
            }
        }

        private void Process(string operationId, List<PatientData> patientDataSubmitted)
        {
            try
            {
                ActivityLogger.Log("INFO>>IDUP", $"Processing {operationId}");

                var model = new InstantDudupModel
                {
                    OperationId = operationId,
                    DuplicationSuspects = new List<DuplicationSuspect>(), 
                    IsDuplicated = false,
                    ProcessingStatus = ProcessingStatus.Processing
                };

                PublishResponse(model);

                var patientBiometricDataChunks =
                        Transforms.ListChunk(PatientData.OrderBy(x => Guid.NewGuid()).ToList(), Setting.DedupProcLimit);

                ActivityLogger.Log("INFO", $"{operationId}: Processing {PatientData.Count} in {patientBiometricDataChunks.Count} chunks");

                foreach (var patientBiometricDataChunk in patientBiometricDataChunks)
                {
                    try
                    {
                        BiomtricSearchEngine.Clear();

                        ActivityLogger.Log("INFO", $"{operationId}: Current Chunk Member Size is {patientBiometricDataChunk.Count}");

                        BiomtricSearchEngine.LoadTemplates(patientBiometricDataChunk);

                        uint i = 0;
                        foreach(var patientDataSubmittedinstance in patientDataSubmitted)
                        {
                            var result = BiomtricSearchEngine.SingleProcess(patientDataSubmittedinstance, i);
                            i++;

                            if (result == null)
                                continue;

                            if (!result.SuspectedCandidates.Any())
                                continue;

                            var validCases = result.SuspectedCandidates
                                    .Where(x => x.BioDataSuspect.Filename != result.Pivot && x.MatchScore >= Setting.DedupMatchScore)
                                    .ToList();

                            ActivityLogger.Log("INFO", $"{operationId}: Found {validCases.Count} Relevant Matches");

                            if (validCases.Any())
                            {
                                foreach (var validCase in validCases)
                                {
                                    model.DuplicationSuspects.Add(new DuplicationSuspect()
                                    {
                                        Data = JsonConvert.SerializeObject(PatientInformation((PatientData)validCase.BioDataSuspect.Data)),
                                        MatchScore = validCase.MatchScore,
                                        PepId = validCase.BioDataSuspect.Filename
                                    });
                                }
                            }

                            validCases = null;
                            result = null;
                        }

                        BiomtricSearchEngine.Clear();
                        ActivityLogger.Log("INFO", $"{operationId}: Current Chunk Member Size is {patientBiometricDataChunk.Count} is Complete");
                    }
                    catch (Exception e)
                    {
                        ActivityLogger.Log(e);
                    }
                }

                BiomtricSearchEngine.Clear();
                ActivityLogger.Log("INFO", $"{operationId}: Processing is Completed at this time");

                model.DuplicationSuspects = model.DuplicationSuspects.OrderByDescending(x => x.MatchScore).Take(10).ToList();
                model.IsSuccessful = true;
                model.ProcessingStatus = ProcessingStatus.Completed;

                PublishResponse(model);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);

                PublishResponse(new InstantDudupModel() { 
                    OperationId = operationId, 
                    ErrorMessage = e.Message, 
                    ProcessingStatus = ProcessingStatus.Completed
                });
            }
        }

        private void PublishResponse(InstantDudupModel instantDudupModel)
        {
            try
            {
                ClearResponse(instantDudupModel.OperationId);
                Core.InMemory.Redis.Operations.SaveData(instantDudupModel.OperationId, instantDudupModel);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private void ClearResponse(string operationId)
        {
            try
            {
                Core.InMemory.Redis.Operations.DeleteData(operationId);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private object PatientInformation(PatientData patientData)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var patientSecondaryInformation = 
                        entities.Patient_PatientBiometricDataSecondary.FirstOrDefault(x => x.PepId == patientData.PepId);

                    if (patientSecondaryInformation != null)
                    {
                        return new { 
                            patientSecondaryInformation.PepId,
                            Facility = entities.Administration_SiteInformation
                            .Where(x => x.Id == patientSecondaryInformation.FacilityId)
                            .Select(x => new { x.SiteCode, x.SiteNameOfficial}).FirstOrDefault(),
                            DateUploaded = patientSecondaryInformation.DateUploaded.ToLongDateString(),
                            DateRegistered = patientSecondaryInformation.DateRegistered.ToLongDateString(),
                            Source = ((SecondaryBioDataSources)patientSecondaryInformation.DataModel).DisplayName(),
                            FingerPosition = patientData.FingerPosition.DisplayName()
                        };
                    }

                    var patientPrimaryInformation = 
                        entities.Patient_PatientInformation.FirstOrDefault(x => x.PepId == patientData.PepId);

                    if (patientPrimaryInformation != null)
                    {
                        return new
                        {
                            patientPrimaryInformation.PepId,
                            Facility = entities.Administration_SiteInformation
                            .Where(x => x.Id == patientPrimaryInformation.SiteId)
                            .Select(x => new { x.SiteCode, x.SiteNameOfficial }).FirstOrDefault(),
                            LastUpdated = patientPrimaryInformation.LastUpdated?.ToLongDateString(),
                            Source = "PBS Client",
                            FingerPosition = patientData.FingerPosition.DisplayName(),
                            DateOfBirth = patientPrimaryInformation.DateOfBirth?.ToLongDateString(),
                            patientPrimaryInformation.HouseAddress,
                            patientPrimaryInformation.PhoneNumber,
                            patientPrimaryInformation.Surname,
                            patientPrimaryInformation.Othername
                        };
                    }
                }

                return patientData;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return patientData;
            }
        }
    }
}