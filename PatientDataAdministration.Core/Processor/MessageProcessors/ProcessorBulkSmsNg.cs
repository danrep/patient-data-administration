using System;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorBulkSmsNg
    {
        public static void SendResponse(string destination, string message, out object responsePayload)
        {
           try
            {
                var oprTrack = Guid.NewGuid().ToString().Replace('-', '0');
                var url =
                    "https://www.bulksmsnigeria.com/api/v1/sms/create?api_token=tR4FS5chs7vukyZRXJYwn0kaYXlkeW4bAhLir6shs1deS1C2ZFUZpH74tUkv&from=STAYHEALTHY&to=" + destination + "&body=" + message + "&dnd=2";

                ActivityLogger.Log($"MSG_PROC_XWIRE_REQ_{oprTrack}".ToUpper(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(url));

                var operation = RemoteRequest.RequestGetSecure(url);

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