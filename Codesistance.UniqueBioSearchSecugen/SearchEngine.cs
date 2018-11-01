using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                EnableRotation = true,
                LicenseFile = "license.dat"
            };

            // license file - Ask SecuGen with your volume number which bin\VolNoReader.exe returns.
            // param.LicenseFile = "./license.dat";

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

            SSearch.ClearFPDB();
            SSearch.TerminateEngine();
            Initialized = false;
        }

        public void LoadTemplates(List<PatientData> patientData)
        {
            if (!Initialized)
                Initialize();

            ActivityLogger.Log("INFO", $"Version ==> {SSearch.GetVersion()}");

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
            ActivityLogger.Log("INFO", "Registering Templates");
            var error = SSError.NONE;

            for (var i = 0; i < SearchModel.Size; i++)
            {
                var templateId = SearchModel.GetTemplate(i).Index;
                var templateBuff = SearchModel.GetTemplate(i).TemplatesBuffer;
                error = SSearch.RegisterFP(templateBuff, templateId);
                if (error != SSError.NONE)
                {
                }
            }

            int fpCount = 0;
            error = SSearch.GetFPCount(ref fpCount);

            ActivityLogger.Log("INFO", $"Fingerprint Count: {fpCount}");

            return error;
        }

        public List<MatchModel> Process()
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

                for (var i = 0; i < templateSearchModelSize; i++)
                {
                    var templateId = (uint)i;
                    ActivityLogger.Log("INFO", $"Currently working on {SearchModel.GetTemplate(i).Filename} of {templateId}");

                    error = SSearch.SearchFP(SearchModel.GetTemplate(i).TemplatesBuffer, ref candList);

                    if (error != SSError.NONE)
                    {
                        ActivityLogger.Log("WARN", $"Failed ==> {SearchModel.GetTemplate(i).Filename} | {error}");
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
                throw;
            }

            return matchModels;
        }
    }
}
