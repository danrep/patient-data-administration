using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Client
{
    public class LocalCache
    {
        private static ObjectCache _cache = MemoryCache.Default;
        private static CacheItemPolicy _policy = null;
        private static readonly LocalPDAEntities _localPdaEntities = new LocalPDAEntities();

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
                case "System_Setting":
                    Set(cacheKeyName, _localPdaEntities.System_Setting.Where(x => !x.IsDeleted).ToList());
                    break;

                case "ClientId":
                    Set(cacheKeyName, ConfigurationManager.AppSettings["ClientId"] ?? "");
                    break;

                default:
                    break;
            }
        }
    }
}
