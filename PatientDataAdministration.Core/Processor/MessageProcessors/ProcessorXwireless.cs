using System;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorXwireless
    {
        public static void SendResponse(string destination, string message, out object responsePayload, string senderId = "STAYHEALTHY")
        {
            try
            {
                if (string.IsNullOrEmpty(senderId))
                    senderId = "STAYHEALTHY";

                var oprTrack = Guid.NewGuid().ToString().Replace('-', '0');
                //var url =
                //    "http://panel.xwireless.net/API/WebSMS/Http/v1.0a/index.php?username=codesistance&password=skyRunn3r&sender=StayHealthy&to=" +
                //    destination + "&message=" + message + "&reqid=1&format=json&route_id=2";

                var url =
                    "http://45.77.146.255:6005/api/v2/SendSMS?SenderId="+ senderId + "&Is_Unicode=false&Is_Flash=false&Message=" +
                    message +
                    "&MobileNumbers=" + destination +
                    "&ApiKey=ulLP6gO0xxClzTwRtw88tw%2B%2FvAWAJDzl1xSjEjEfUkM%3D&ClientId=f039365c-940e-4971-8fec-6700076178b4";

                ActivityLogger.Log($"MSG_PROC_XWIRE_REQ_{oprTrack}".ToUpper(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(url));

                var operation = RemoteRequest.RequestGet(url);

                if (!operation.Result.Status)
                {
                    responsePayload = null;
                    ActivityLogger.Log($"MSG_PROC_XWIRE_RES_{oprTrack}".ToUpper(), "Low Balance");
                }
                else
                {
                    ActivityLogger.Log($"MSG_PROC_XWIRE_RES_{oprTrack}".ToUpper(),
                        Newtonsoft.Json.JsonConvert.SerializeObject(operation.Result));

                    responsePayload = operation.Result.Data;
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                responsePayload = null;
            }
        }

        public static void Inquiry(string messageId, out dynamic responsePayload)
        {
            try
            {
                //var url =
                //    $"http://smsc.xwireless.net/API/WebSMS/Http/v3.1/index.php?method=show_dlr&username=codesistance&password=skyRunn3r&msg_id={messageId}&seq_id=1,2&limit=0,10&format=json";

                var url =
                    "http://45.77.146.255:6005/api/v2/MessageStatus?ApiKey=ulLP6gO0xxClzTwRtw88tw%2B%2FvAWAJDzl1xSjEjEfUkM%3D&ClientId=f039365c-940e-4971-8fec-6700076178b4&MessageId=" +
                    messageId;

                ActivityLogger.Log($"MSG_PROC_XWIRE_REQ".ToUpper(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(url));

                var operation = RemoteRequest.RequestGet(url);

                responsePayload = operation.Result.Data;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                responsePayload = null;
            }
        }
    }
}