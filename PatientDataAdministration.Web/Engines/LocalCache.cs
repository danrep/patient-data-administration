using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Engines
{
    public class LocalCache
    {
        private static ObjectCache _cache = MemoryCache.Default;
        private static CacheItemPolicy _policy = null;

        public static void Set(string cacheKeyName, object cacheItem, int absoluteExpiration = 1000)
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
                        Set(cacheKeyName, entities.Sp_System_Indicators_PopulationDistro_SexSiteState().ToList(), 100);
                    }
                    break;

                case "Administration_SiteInformation":
                    using (var entities = new Entities())
                    {
                        Set(cacheKeyName, entities.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList()
                            , 10000);
                    }
                    break;

                case "System_State":
                    using (var entities = new Entities())
                    {
                        Set(cacheKeyName, entities.System_State.Where(x => !x.IsDeleted).ToList()
                            , 10000);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
