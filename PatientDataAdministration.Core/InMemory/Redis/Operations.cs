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

        public static bool SaveData<T>(string key, T value)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                cache.StringSet(key, JsonConvert.SerializeObject(value));
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
