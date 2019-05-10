using System;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorXwireless
    {
        public static void SendResponse(string destination, string message)
        {
            try
            {
                var oprTrack = Guid.NewGuid().ToString().Replace('-', '0');
                var url =
                    "http://panel.xwireless.net/API/WebSMS/Http/v1.0a/index.php?username=codesistance&password=skyRunn3r&sender=StayHealthy&to=" +
                    destination + "&message=" + message + "&reqid=1&format=json&route_id=2";

                ActivityLogger.Log($"MSG_PROC_XWIRE_REQ_{oprTrack}",
                    Newtonsoft.Json.JsonConvert.SerializeObject(url));
                ActivityLogger.Log($"MSG_PROC_XWIRE_RES_{oprTrack}",
                    Newtonsoft.Json.JsonConvert.SerializeObject(RemoteRequest.RequestGet(url).Result));
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}