using Newtonsoft.Json;
using System;

namespace PatientDataAdministration.Core.InMemory.Redis
{
    class Operations
    {
        public T ReadData<T>(string key)
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

        public bool SaveData<T>(string key, T value)
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
