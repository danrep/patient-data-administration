using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using PatientDataAdministration.Core.DataTranslation;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorInfoBip
    {
        public static void ProcessIngress(string requestPayLoad)
        {
            try
            {
                var message =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<InfoBipIngresUserPush>(requestPayLoad);

                if (!message.Results.Any())
                {
                    ActivityLogger.Log("MSG_PROC_INFOBIP", $@"No Messages");
                    return;
                }

                Parallel.ForEach(message.Results, ShortMessagingBridgeExecutor.ProcessMessage);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static void ProcessIngress(InfoBipIngresUserPush requestPayLoad)
        {
            try
            {
                if (requestPayLoad.Results.FirstOrDefault() == null)
                {
                    ActivityLogger.Log("INFO", $@"No Messages to Process");
                    return;
                }

                if (string.IsNullOrEmpty(requestPayLoad.Results.FirstOrDefault()?.Text))
                {
                    ActivityLogger.Log("INFO", $@"No Messages to Process");
                    return;
                }

                ActivityLogger.Log("MSG_PROC_INFOBIP", Newtonsoft.Json.JsonConvert.SerializeObject(requestPayLoad));

                ShortMessagingBridgeExecutor.ProcessMessage(requestPayLoad.Results.FirstOrDefault());
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static void SendResponse(string destination, string message)
        {
            try
            {
                const string url = "https://api.infobip.com/sms/1/text/single";

                var collection = new NameValueCollection {{"authorization", "Basic Q29kZXNpc3RhbmNlOnNreVJ1bm4zcg=="}};
                var response = RemoteRequest.RequestPost(url, Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    from = "CloudPortal",
                    to = destination,
                    text = message
                }), collection);
                ActivityLogger.Log("MSG_PROC_INFOBIP", Newtonsoft.Json.JsonConvert.SerializeObject(response));
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}