using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Engines
{
    public class LocalCache
    {
        private static ObjectCache _cache = MemoryCache.Default;
        private static CacheItemPolicy _policy = null;

        public static void Set(string cacheKeyName, object cacheItem)
        {
            _policy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.Default,
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1000)
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

                default:
                    break;
            }
        }
    }
}
