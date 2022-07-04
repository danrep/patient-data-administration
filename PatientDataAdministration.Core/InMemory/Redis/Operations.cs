using Newtonsoft.Json;
using System;

namespace PatientDataAdministration.Core.InMemory.Redis
{
    public class Operations
    {
        public static T ReadData<T>(string key)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                return JsonConvert.DeserializeObject<T>(cache.StringGet(key));
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return default;
            }
        }

        public static bool SaveData<T>(string key, T value, int expiryInSeconds = 1800)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                cache.StringSet(key, JsonConvert.SerializeObject(value), new TimeSpan(0, 0, expiryInSeconds));
                return true;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return false;
            }
        }

        public static bool DeleteData(string key)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                cache.KeyDelete(new StackExchange.Redis.RedisKey(key));
                return true;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return false;
            }
        }
    }
}
