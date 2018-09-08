using System;
using System.Collections.Generic;
using System.Diagnostics;
using PatientDataAdministration.Core;
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
                EnableRotation = true
            };

            // license file - Ask SecuGen with your volume number which bin\VolNoReader.exe returns.
            //param.LicenseFile = "./license.dat";

            SSError error = SSearch.InitializeEngine(param);
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
        }

        public void DeInitialize()
        {
            if (!Initialized)
                return;

            SSearch.TerminateEngine();
            Initialized = false;
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

        private SSError RegisterSearchModel()
        {
            ActivityLogger.Log("INFO", "Registering templates ...>>>");
            var error = SSError.NONE;

            for (var i = 0; i < SearchModel.Size; i++)
            {
                var templateId = SearchModel.GetTemplate(i).Index;
                var templateBuff = SearchModel.GetTemplate(i).TemplatesBuffer;
                error = SSearch.RegisterFP(templateBuff, templateId);
                if (error != SSError.NONE)
                {
                    break;
                }
            }

            int fpCount = 0;
            error = SSearch.GetFPCount(ref fpCount);
            Debug.Assert(error == SSError.NONE);
            
            ActivityLogger.Log("INFO", $"Fingerprint Count: {fpCount}");

            return error;
        }
        
        private SSError RegisterSearchModelBatch()
        {
            int fpCountBefore = 0, fpCount = 0;

            var error = SSearch.GetFPCount(ref fpCountBefore);
            Debug.Assert(error == SSError.NONE);

            ActivityLogger.Log("INFO", "Registering templates in Batches");
            const int batchCount = 1000;
            var pairs = new SSIdTemplatePair[batchCount];
            var i = 0;

            while (i < SearchModel.Size)
            {
                int k;
                for (k = 0; k < batchCount && i < SearchModel.Size; k++, i++)
                {
                    pairs[k].Id = (uint)i;
                    pairs[k].Template = SearchModel.GetTemplate(i).TemplatesBuffer;
                }
                error = SSearch.RegisterFPBatch(pairs, k);
                if (error != SSError.NONE)
                {
                    break;
                }
            }

            error = SSearch.GetFPCount(ref fpCount);

            ActivityLogger.Log("INFO", $"Fingerprint Count: {fpCount}");

            return error;
        }

        public bool Process()
        {
            if (!Initialized)
                return false;

            var error = SSError.NONE;

            Console.WriteLine("API version: {0}\n", SSearch.GetVersion());

            // Read template files
            // Register
            error = RegisterSearchModel();
            Debug.Assert(error == SSError.NONE);

            // how many templates in mdb
            var templateSearchModelSize = SearchModel.Size;

            // how many templates in secusearch
            var fpCount = 0;
            error = SSearch.GetFPCount(ref fpCount);
            Debug.Assert(error == SSError.NONE);

            // List IDs of templates registered
            var idList = new List<uint>();
            error = SSearch.GetIDList(idList);

            foreach (var id in idList)
            {
                Console.WriteLine("id = {0}", id);
            }

            // search : the candidate count must be zero because secusearch has no templates.
            var candList = new SSCandList();

            for (var i = 0; i < templateSearchModelSize; i++)
            {
                var templateId = (uint)i;
                ActivityLogger.Log("INFO", $"Currently working on {SearchModel.GetTemplate(i).Filename} of {templateId}");

                error = SSearch.SearchFP(SearchModel.GetTemplate(i).TemplatesBuffer, ref candList);
                if (error != SSError.NONE)
                {
                    Console.WriteLine("search failed error: {0}", error);
                    ActivityLogger.Log("WARN", $"Failed ==> {SearchModel.GetTemplate(i).Filename} | {error}");
                    continue;
                }

                if (candList.Count > 0)
                {
                    Console.Write("{0}   ", candList.Candidates[0].Id);
                    Console.WriteLine("matchScore={0}", candList.Candidates[0].MatchScore);
                }
                else
                {
                    Console.WriteLine(" no candidate");
                }
            }

            return true;
        }
    } 
} 
