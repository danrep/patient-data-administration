using PatientDataAdministration.Core.PubSub;
using PatientDataAdministration.EnumLibrary;
using StackExchange.Redis;

namespace PatientDataAdministration.DeduplicationEngine.Engines.FileOperations
{
    public class ProcessFile
    {
        public static PubSubResponse ProcessSecondaryData(ChannelMessage channelMessage)
        {
            try
            {
                var message = Newtonsoft.Json.JsonConvert.DeserializeObject<CommunicationModel>(channelMessage.Message.ToString());
                object data;

                switch(message.PubSubAction)
                {
                    case PubSubAction.ProcessSecondaryDataUploadedFile:
                        data = (SecondaryFileData)message.Data;
                        break;

                    default:
                        break;
                }

                return PubSubResponse.Success;
            }
            catch (System.Exception)
            {
                return PubSubResponse.Error;
            }
        }
    }
}
