using StackExchange.Redis;
using System;
using System.Configuration;

namespace PatientDataAdministration.Core.InMemory.Redis
{
    public class RedisConnectorHelper
    {
        static RedisConnectorHelper()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {                
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisConnectionString"].ToString());
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
