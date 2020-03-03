using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PatientDataAdministration.Core.Processor.MessageProcessors
{
    public class ProcessorClickatell
    {
        public static void SendResponse(string destination, string message, out object responsePayload)
        {
            try
            {
                var oprTrack = Guid.NewGuid().ToString().Replace('-', '0');
                //var url = "https://platform.clickatell.com/messages/http/send?apiKey=vcTmDvbGQV6ovHPXzKMGqg==&to=" + destination + "&content=" + message;

                var url = "https://platform.clickatell.com/v1/message";
                ActivityLogger.Log($"MSG_PROC_CLICK_REQ_{oprTrack}".ToUpper(),
                    JsonConvert.SerializeObject(url));

                var listOfMessages = new List<object>
                {
                    new
                    {
                        channel = "sms",
                        to = destination,
                        content = message
                    }
                };

                using (var client = new HttpClient())
                {
                    var res = client.PostAsync(url,
                      new StringContent(JsonConvert.SerializeObject(
                        new
                        {
                            messages = listOfMessages
                        }
                    )));

                    res.Result.EnsureSuccessStatusCode();

                    ActivityLogger.Log($"MSG_PROC_CLICK_RES_{oprTrack}".ToUpper(),
                        JsonConvert.SerializeObject(res.Result));

                    responsePayload = res.Result;
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