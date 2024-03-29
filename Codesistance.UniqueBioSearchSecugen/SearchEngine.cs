﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using SecuGen.SecuSearchSDK3;

namespace Codesistance.UniqueBioSearchSecugen
{
    public class SearchEngine
    {
        public SecuSearch SSearch;
        public bool Initialized;
        public SearchModel SearchModel;

        public SearchEngine()
        {
            SSearch = new SecuSearch();
            SearchModel = new SearchModel();
            Initialized = false;
            Initialize();
        }

        ~SearchEngine()
        {
            DeInitialize();
        }

        public void Initialize()
        {
            Initialized = false;

            var param = new SSParam
            {
                CandidateCount = 10,
                Concurrency = 0,
                EnableRotation = true,
                LicenseFile = "license.dat"
            };

            // license file - Ask SecuGen with your volume number which bin\VolNoReader.exe returns.
            // param.LicenseFile = "./license.dat";

            SSError error = SSearch.InitializeEngine(param);
            
            if (error == SSError.INVALID_PARAM)
            {
                param = new SSParam
                {
                    CandidateCount = 10,
                    Concurrency = 0,
                    EnableRotation = true
                };
                
                error = SSearch.InitializeEngine(param);
            }

            switch (error)
            {
                case SSError.NONE:
                    Initialized = true;
                    break;
                case SSError.SECUSEARCHAPI_DLL_UNLOADED:
                    ActivityLogger.Log("WARN",
                        IntPtr.Size == 8
                            ? $"{SSConstants.SECUSEARCH_API_DLL_NAME_64BIT} is not loaded."
                            : $"{SSConstants.SECUSEARCH_API_DLL_NAME_32BIT} is not loaded.");
                    break;
                case SSError.SET_LOCK_PAGE_PRIVILEGE:
                    ActivityLogger.Log("WARN", "Cannot enable the SE_LOCK_MEMORY privilege");
                    break;
                case SSError.LICENSE_LOAD:
                case SSError.LICENSE_KEY:
                case SSError.LICENSE_EXPIRED:
                    ActivityLogger.Log("WARN", $"License file({param.LicenseFile}) is missing or not valid");
                    break;
                default:
                    ActivityLogger.Log("WARN", $"failed to initialize SecuSearch(code = {error})");
                    break;
            }

            ActivityLogger.Log("INFO", $"Version ==> {SSearch.GetVersion()}");
        }

        public void DeInitialize()
        {
            if (!Initialized)
                return;

            SSearch.ClearFPDB();
            SSearch.TerminateEngine();
            Initialized = false;
        }

        public void Clear()
        {
            for (uint i = 0; i < SearchModel.Size; i++)
                SSearch.RemoveFP(i);

            SSearch.ClearFPDB();

            SearchModel.Clear();
        }

        public void LoadTemplates(List<PatientData> patientData)
        {
            if (!Initialized)
                Initialize();

            ActivityLogger.Log("INFO", $"Loaded {patientData.Count}");
            var loaded = SearchModel.Load(patientData); 

            if (!loaded)
            {
                ActivityLogger.Log("WARN", $"Failed to Load Biometric Templates");
                return;
            }

            ActivityLogger.Log("INFO", $"Loaded Data Size {SearchModel.Size}");
        }

        [HandleProcessCorruptedStateExceptions]
        private SSError RegisterSearchModel()
        {
            ActivityLogger.Log("INFO", "Registering Templates");
            var error = SSError.NONE;

            try
            {
                for (var i = 0; i < SearchModel.Size; i++)
                {
                    try
                    {
                        var templateId = SearchModel.GetTemplate(i).Index;
                        var templateBuff = SearchModel.GetTemplate(i).TemplatesBuffer;
                        var patientData = (PatientData)SearchModel.GetTemplate(i).Data;

                        if (patientData.FingerPrintStore == FingerPrintStore.Primary)
                        {
                            error = SSearch.RemoveFP(templateId);
                            error = SSearch.RegisterFP(templateBuff, templateId);
                            
                            if (error != SSError.NONE)
                            {
                                ActivityLogger.Log("ERROR", $"Finger Registration Error: {error.DisplayName()}");
                            }
                        }
                        else
                        {
                            byte[] sgTemplate = new byte[SSConstants.TEMPLATE_SIZE];
                            uint numberOfViews = 0;

                            SSearch.GetNumberOfView(templateBuff, SSTemplateType.ISO19794, ref numberOfViews);

                            for (uint indexOfView = 0; indexOfView < numberOfViews; indexOfView++)
                            {
                                try
                                {
                                    error = SSearch.ExtractTemplate(templateBuff, SSTemplateType.ISO19794, indexOfView, sgTemplate);
                                    if (error != SSError.NONE)
                                    {
                                        ActivityLogger.Log("ERROR", $"Finger Extraction Error: {error.DisplayName()}");
                                        continue;
                                    }

                                    error = SSearch.RemoveFP(templateId);
                                    error = SSearch.RegisterFP(sgTemplate, templateId);
                                    if (error != SSError.NONE)
                                    {
                                        ActivityLogger.Log("ERROR", $"Finger Registration Error: {error.DisplayName()}");
                                    }
                                }
                                catch (Exception exx)
                                {
                                    ActivityLogger.Log("ERROR >> Codesistance.UniqueBioSearch", $"{exx.Message}: {exx}");
                                }
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        ActivityLogger.Log("ERROR >> Codesistance.UniqueBioSearch", $"{exx.Message}: {exx}");
                    }
                }

                int fpCount = 0;
                error = SSearch.GetFPCount(ref fpCount);

                ActivityLogger.Log("INFO", $"Fingerprint Count: {fpCount}");
            }
            catch (Exception ex)
            {
                ActivityLogger.Log("ERROR >> Codesistance.UniqueBioSearch", $"{ex.Message}: {ex}");
                ActivityLogger.Log(ex);
            }

            return error;
        }

        [HandleProcessCorruptedStateExceptions]
        public bool TestData(byte[] template)
        {
            byte[] sgTemplate = new byte[SSConstants.TEMPLATE_SIZE];
            bool result = false;

            try
            {
                uint numberOfViews = 0;

                SSearch.GetNumberOfView(template, SSTemplateType.ISO19794, ref numberOfViews);

                for (uint indexOfView = 0; indexOfView < numberOfViews; indexOfView++)
                {
                    try
                    {
                        var error = SSearch.ExtractTemplate(template, SSTemplateType.ISO19794, indexOfView, sgTemplate);
                        result = error == SSError.NONE;
                    }
                    catch (Exception exx)
                    {
                        ActivityLogger.Log("ERROR >> Codesistance.UniqueBioSearch", $"{exx.Message}: {exx}");
                    }
                }
            }
            catch (Exception)
            {
            }

            sgTemplate = template = null;
            return result;
        }

        [HandleProcessCorruptedStateExceptions]
        public List<MatchModel> BulkProcess()
        {
            if (!Initialized)
                return null;

            var matchModels = new List<MatchModel>();

            try
            {
                var error = SSError.NONE;

                // Read template files
                // Register
                error = RegisterSearchModel();

                // how many templates in mdb
                var templateSearchModelSize = SearchModel.Size;

                // how many templates in secusearch
                var fpCount = 0;
                error = SSearch.GetFPCount(ref fpCount);

                // List IDs of templates registered
                var idList = new List<uint>();
                error = SSearch.GetIDList(idList);

                // search : the candidate count must be zero because secusearch has no templates.
                var candList = new SSCandList();
                byte[] sgTemplate = new byte[SSConstants.TEMPLATE_SIZE];

                for (var i = 0; i < templateSearchModelSize; i++)
                {
                    var templateId = (uint)i;
                    ActivityLogger.Log("INFO", $"Currently working on {SearchModel.GetTemplate(i).Filename} of {templateId}");

                    var templateBuff = SearchModel.GetTemplate(i).TemplatesBuffer;
                    uint numberOfViews = 0;
                    SSearch.GetNumberOfView(templateBuff, SSTemplateType.ISO19794, ref numberOfViews);
                    for (uint indexOfView = 0; indexOfView < numberOfViews; indexOfView++)
                    {
                        error = SSearch.ExtractTemplate(templateBuff, SSTemplateType.ISO19794, indexOfView, sgTemplate);
                        if (error != SSError.NONE)
                        {
                            ActivityLogger.Log("ERROR", $"Finger Extraction Error: {error.DisplayName()}");
                            continue;
                        }

                        error = SSearch.SearchFP(sgTemplate, ref candList);

                        if (error != SSError.NONE)
                        {
                            ActivityLogger.Log("ERROR", $"Finger Confirmation Error: {error.DisplayName()}");
                        }
                    }
                    

                    if (error != SSError.NONE)
                    {
                        ActivityLogger.Log("WARN", $"Failed ==> {SearchModel.GetTemplate(i).Filename} | {error.DisplayName()}");
                        continue;
                    }

                    ActivityLogger.Log("INFO",
                        candList.Count > 0
                            ? $"Matching: Found {candList.Count} Matches"
                            : "Matching: No Matching Candidate");

                    if (candList.Count > 0)
                    {
                        matchModels.Add(new MatchModel()
                        {
                            Pivot = SearchModel.GetTemplate(i).Filename,
                            PivotData = (PatientData)SearchModel.GetTemplate(i).Data,
                            SuspectedCandidates = candList.Candidates
                                .Where(x => x.ConfidenceLevel != SSConfLevel.INVALID)
                                .Select(x => new SuspectedCandidate()
                                {
                                    BioDataSuspect = SearchModel.GetTemplate((int) x.Id),
                                    MatchScore = x.MatchScore
                                }).ToList()
                        });
                    }
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }

            return matchModels;
        }

        [HandleProcessCorruptedStateExceptions]
        public MatchModel SingleProcess(PatientData patientData, uint id)
        {
            if (!Initialized)
                return null;

            var matchModel = new MatchModel();

            try
            {
                var error = SSError.NONE;

                // Read template files
                // Register
                error = RegisterSearchModel();

                // how many templates in mdb
                var templateSearchModelSize = SearchModel.Size;

                // how many templates in secusearch
                var fpCount = 0;
                error = SSearch.GetFPCount(ref fpCount);

                // List IDs of templates registered
                var idList = new List<uint>();
                error = SSearch.GetIDList(idList);

                // search : the candidate count must be zero because secusearch has no templates.
                var candList = new SSCandList();
                byte[] sgTemplate = new byte[SSConstants.TEMPLATE_SIZE];

                var template = new Template(patientData.PepId, id,
                        Convert.FromBase64String(patientData.FingerPrintData), patientData);

                var templateBuff = template.TemplatesBuffer;

                uint numberOfViews = 0;
                SSearch.GetNumberOfView(templateBuff, SSTemplateType.ISO19794, ref numberOfViews);

                if (numberOfViews > 0)
                    for (uint indexOfView = 0; indexOfView < numberOfViews; indexOfView++)
                    {
                        error = SSearch.ExtractTemplate(templateBuff, SSTemplateType.ISO19794, indexOfView, sgTemplate);
                        if (error != SSError.NONE)
                        {
                            ActivityLogger.Log("ERROR", $"Finger Extraction Error: {error.DisplayName()}");
                            continue;
                        }

                        error = SSearch.SearchFP(sgTemplate, ref candList);

                        if (error != SSError.NONE)
                        {
                            ActivityLogger.Log("ERROR", $"Finger Confirmation Error: {error.DisplayName()}");
                        }
                    }
                else{
                    error = SSearch.SearchFP(templateBuff, ref candList);

                    if (error != SSError.NONE)
                    {
                        ActivityLogger.Log("ERROR", $"Finger Confirmation Error: {error.DisplayName()}");
                    }
                }

                if (error != SSError.NONE)
                {
                    ActivityLogger.Log("WARN", $"Failed ==> {template.Filename} | {error.DisplayName()}");
                    return new MatchModel();
                }

                ActivityLogger.Log("INFO",
                    candList.Count > 0
                        ? $"Matching: Found {candList.Count} Matches"
                        : "Matching: No Matching Candidate");

                if (candList.Count > 0)
                {
                    matchModel = new MatchModel()
                    {
                        Pivot = template.Filename,
                        PivotData = (PatientData)template.Data,
                        SuspectedCandidates = candList.Candidates
                            .Where(x => x.ConfidenceLevel != SSConfLevel.INVALID)
                            .Select(x => new SuspectedCandidate()
                            {
                                BioDataSuspect = SearchModel.GetTemplate((int)x.Id),
                                MatchScore = x.MatchScore
                            }).ToList()
                    };
                }
                else
                {
                    matchModel.SuspectedCandidates = new List<SuspectedCandidate>();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }

            return matchModel;
        }
    }
}
