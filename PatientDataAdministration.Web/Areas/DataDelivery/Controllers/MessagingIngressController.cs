using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Core.DataTranslation;
using PatientDataAdministration.Core.Processor.MessageProcessors;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.DataDelivery.Controllers
{
    public class MessagingIngressController : Controller
    {
        [System.Web.Http.HttpPost]
        public JsonResult MessageIngressInfoBipPost(string sender, string id, string text, string keyword = null)
        {
            return OperationProcessor(sender, id, text, keyword);
        }

        [System.Web.Http.HttpGet]
        public JsonResult MessageIngressInfoBipGet(string sender, string id, string text, string keyword = null)
        {
            return OperationProcessor(sender, id, text, keyword);
        }

        private JsonResult OperationProcessor(string sender, string id, string text, string keyword = null)
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
                return Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return Json(ResponseData.SendExceptionMsg(e), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
