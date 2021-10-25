using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Core
{
    public class LocalCache
    {
        private static ObjectCache _cache = MemoryCache.Default;
        private static CacheItemPolicy _policy = null;

        public static void Set(string cacheKeyName, object cacheItem, int absoluteExpiration = 3600)
        {
            _policy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.Default,
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(absoluteExpiration)
            };

            _cache.Set(cacheKeyName, cacheItem, _policy);
        }

        public static T Get<T>(string cacheKeyName)
        {
            if (_cache[cacheKeyName] == null)
                RefreshCache(cacheKeyName);

            return (T)_cache[cacheKeyName];
        }

        public static void RefreshCache(string cacheKeyName)
        {
            switch (cacheKeyName)
            {
                case "System_ClientPulse":
                    Set(cacheKeyName, new List<System_ClientPulse>());
                    break;

                case "Sp_System_Indicators_PopulationDistro_SexSiteState_Result":
                    using (var entities = new Entities())
                    {
                        // 5 mins
                        Set(cacheKeyName,
                            entities.Sp_System_Indicators_PopulationDistro_SexSiteState(null, null).ToList(), 300);
                    }
                    break;

                case "Administration_SiteInformation":
                    using (var entities = new Entities())
                    {
                        // 1 Hour
                        Set(cacheKeyName, entities.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList()
                            , 3200);
                    }
                    break;

                case "System_ReportingLog":
                    using (var entities = new Entities())
                    {
                        // 12 Hours
                        Set(cacheKeyName, entities.System_ReportingLog.Where(x => x.IsCurrent && !x.IsDeleted).ToList()
                            , 43200);
                    }
                    break;

                case "System_OperationLog":
                    using (var entities = new Entities())
                    {
                        // 12 Hours
                        Set(cacheKeyName, entities.System_OperationLog.Where(x => x.IsCurrent && !x.IsDeleted).ToList()
                            , 43200);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
