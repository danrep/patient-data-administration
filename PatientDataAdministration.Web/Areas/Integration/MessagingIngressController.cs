using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using PatientDataAdministration.Core;
using PatientDataAdministration.Core.DataTranslation;
using PatientDataAdministration.Core.Processor.MessageProcessors;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.Integration
{
    public class MessagingIngressController : ApiController
    {
        [HttpPost]
        public ResponseData MessageIngressInfoBipPost([FromBody] object rawPayLoad)
        {
            try
            {
                var requestPayload = "";

                if (rawPayLoad == null)
                    requestPayload = Request.Content.ReadAsStringAsync().Result;
                else
                    requestPayload = Newtonsoft.Json.JsonConvert.SerializeObject(rawPayLoad);

                new Thread(() => ProcessorInfoBip.ProcessIngress(requestPayload)).Start();
                return ResponseData.SendSuccessMsg();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return ResponseData.SendExceptionMsg(e);
            }
        }

        [HttpGet]
        public ResponseData MessageIngressInfoBipGet(string sender, string id, string text)
        {
            try
            {
                var requestPayload = new InfoBipIngresUserPush()
                {
                    MessageCount = 1,
                    PendingMessageCount = 0,
                    Results = new List<Result>()
                    {
                        new Result()
                        {
                            From = sender,
                            MessageId = id,
                            Text = text
                        }
                    }
                };

                new Thread(() => ProcessorInfoBip.ProcessIngress(requestPayload)).Start();
                return ResponseData.SendSuccessMsg();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return ResponseData.SendExceptionMsg(e);
            }
        }
    }
}
