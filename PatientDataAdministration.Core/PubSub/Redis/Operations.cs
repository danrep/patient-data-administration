using PatientDataAdministration.EnumLibrary;
using StackExchange.Redis;
using System;

namespace PatientDataAdministration.Core.PubSub.Redis
{
    public class Operations
    {
        public static void Subscribe(string channelName, Func<ChannelMessage, PubSubResponse> channelMessageExecutor)
        {
            try
            {
                var channel = InMemory.Redis.RedisConnectorHelper.Connection.GetSubscriber().Subscribe(channelName);

                channel.OnMessage(message =>
                {
                    channelMessageExecutor(message);
                });

                ActivityLogger.Log("INFO", $"Listening on {channelName} channel");
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }
        
        public static void Unsubscribe(string channelName)
        {
            try
            {
                InMemory.Redis.RedisConnectorHelper.Connection.GetSubscriber().Unsubscribe(channelName);

                ActivityLogger.Log("INFO", $"Stopped listening on {channelName} channel");
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        public static void Publish(string channelName, RedisValue message)
        {
            try
            {
                var response = InMemory.Redis.RedisConnectorHelper.Connection.GetSubscriber().Publish(channelName, message);

                ActivityLogger.Log("INFO", $"Message Successfully Published on {channelName} channel. Message received by {response} client(s)");
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }
    }
}
