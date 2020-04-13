using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Controllers
{
    public class MessagingCenterController : BaseController
    {
        // GET: MessagingCenter0
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostMessage(string message, List<string> listOfPhoneNumbers)
        {
            try
            {
                new Thread(()=> {
                    using(var entities = new Entities())
                    {
                        foreach (var rawPhoneNumber in listOfPhoneNumbers)
                        {
                            var phoneNumber = Transforms.FormatPhoneNumber(rawPhoneNumber);

                            if (phoneNumber.Contains("00000"))
                            {
                                RegisterMessage(new { Status = "Successful 98" }, phoneNumber, MessageResponse.Invalid);
                                continue;
                            }

                            if (!entities.Integration_SystemPhoneNumberBlacklist.Any(x =>
                                                !x.IsDeleted && x.PhoneNumber == phoneNumber))
                            {
                                dynamic payload;

                                Messaging.SendSms(phoneNumber, message, out payload);

                                if (payload == null)
                                    RegisterMessage(new { Status = "Successful 99" }, phoneNumber, MessageResponse.Pending);
                                else
                                    RegisterMessage(payload, phoneNumber, MessageResponse.Pending);
                            }
                            else
                                RegisterMessage(new { Status = "Successful 00"}, phoneNumber, MessageResponse.Delivered);
                        }
                    }
                }).Start();
                return Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendExceptionMsg(e), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMessageManifest()
        {
            try
            {
                using (var entities = new Entities())
                {
                    var manifest = entities.Integration_SystemDeliveryManifest
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.IsDelivered)
                    .ThenByDescending(x => x.MessageDate)
                    .Take(1000).ToList();

                    return Json(new
                    {
                        Status = true,
                        Message = "Successful",
                        Data = manifest
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMessageManifestItems(string messageId)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var manifest = entities.Integration_SystemDeliveryManifest
                    .FirstOrDefault(x => x.MessageId == messageId);

                    return Json(new
                    {
                        Status = true,
                        Message = "Successful",
                        Data = new
                        {
                            Manifest = manifest,
                            ManifestItems = entities.Integration_SystemProviderDeliveryLogs.Where(x => x.MessageId == messageId && !x.IsDeleted)
                            .ToList()
                            .Select(x => new
                            {
                                TimeStamp = x.OperationDate.Value.ToString("dd-MM-yyyy hh:mm:ss tt"),
                                Status = ((MessageResponse)x.OperationStatus).DisplayName(),
                                x.OperationDate
                            })
                            .OrderByDescending(x => x.OperationDate)
                            .ToList()
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ResendMessage(string messageId)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var appointmentDataItem = entities.Integration_SystemAppointmentDataItem
                    .FirstOrDefault(x => x.MessageId == messageId);

                    if(appointmentDataItem != null)
                    {
                        appointmentDataItem.MessageStatus = (int)MessageResponse.Pending;
                        entities.Entry(appointmentDataItem).State = System.Data.Entity.EntityState.Modified;
                        entities.SaveChanges();
                    }

                    return Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void RegisterMessage(object payload, string phoneNumber, MessageResponse messageResponse)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var messageId = Guid.NewGuid().ToString().Replace('-', '0').ToUpper();

                    var logDate = DateTime.Now;
                    entities.Integration_SystemDeliveryManifest.Add(new Integration_SystemDeliveryManifest()
                    {
                        IsDeleted = false,
                        IsDelivered = messageResponse == MessageResponse.Delivered,
                        MessageDate = logDate,
                        MessageId = messageId,
                        MessageSupportParams = Newtonsoft.Json.JsonConvert.SerializeObject(payload),
                        PepId = "NA",
                        PhoneNumber = phoneNumber
                    });

                    entities.Integration_SystemProviderDeliveryLogs.Add(new Integration_SystemProviderDeliveryLogs()
                    {
                        IsDeleted = false,
                        IsFinalStatus = true,
                        MessageId = messageId,
                        OperationDate = logDate,
                        OperationStatus = (int)messageResponse
                    });

                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}