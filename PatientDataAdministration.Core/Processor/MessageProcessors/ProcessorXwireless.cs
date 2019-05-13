using System;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorXwireless
    {
        public static void SendResponse(string destination, string message, out object responsePayload)
        {
            try
            {
               var oprTrack = Guid.NewGuid().ToString().Replace('-', '0');
                var url =
                    "http://panel.xwireless.net/API/WebSMS/Http/v1.0a/index.php?username=codesistance&password=skyRunn3r&sender=StayHealthy&to=" +
                    destination + "&message=" + message + "&reqid=1&format=json&route_id=2";

                ActivityLogger.Log($"MSG_PROC_XWIRE_REQ_{oprTrack}".ToUpper(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(url));

                var operation = RemoteRequest.RequestGet(url);

                ActivityLogger.Log($"MSG_PROC_XWIRE_RES_{oprTrack}".ToUpper(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(operation.Result));

                responsePayload = operation.Result.Data;
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
                var url =
                    $"http://smsc.xwireless.net/API/WebSMS/Http/v3.1/index.php?method=show_dlr&username=codesistance&password=skyRunn3r&msg_id={messageId}&seq_id=1,2&limit=0,10&format=json";
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