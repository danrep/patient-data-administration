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
                using (var entities = new Entities())
                {
                    if (!entities.Sp_Integration_GetCreditStatus(listOfPhoneNumbers.Count).FirstOrDefault().Value)
                        return Json(ResponseData.SendFailMsg(message: "Insufficient Units for Processing"), JsonRequestBehavior.AllowGet);
                }

                new Thread(()=> {
                    using(var entities = new Entities())
                    {
                        foreach (var rawPhoneNumber in listOfPhoneNumbers)
                        {
                            var phoneNumber = Transforms.FormatPhoneNumber(rawPhoneNumber);

                            if (phoneNumber.Contains("00000"))
                            {
                                RegisterMessage(new { StatusMessage = "Successful 96", GeneratedMessage = message }, 
                                    phoneNumber, MessageResponse.Invalid);
                                continue;
                            }

                            if (!entities.Integration_SystemPhoneNumberBlacklist.Any(x =>
                                                !x.IsDeleted && x.PhoneNumber == phoneNumber))
                            {
                                dynamic payload;

                                var messageState = Messaging.SendSms(phoneNumber, message, out payload);

                                if (payload == null)
                                    RegisterMessage(new { StatusMessage = "Successful 98", GeneratedMessage = message }, 
                                        phoneNumber, MessageResponse.Pending);
                                else
                                {
                                    if (messageState)
                                        RegisterMessage(new { StatusMessage = "Successful 00", GeneratedMessage = message, Payload = payload },
                                            phoneNumber, MessageResponse.Delivered);
                                    else
                                        RegisterMessage(new { StatusMessage = "Successful 99", GeneratedMessage = message, Payload = payload },
                                            phoneNumber, MessageResponse.Pending);
                                }
                            }
                            else
                                RegisterMessage(new { StatusMessage = "Successful 97", GeneratedMessage = message }, 
                                    phoneNumber, MessageResponse.Delivered);

                            new Thread(() => {
                                using (var innerentities = new Entities())
                                {
                                    innerentities.Sp_Integration_DeductLicenseCredit(1);
                                }
                            }).Start();
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
                    .OrderByDescending(x => x.MessageDate)
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

        public JsonResult GetMessageMeta()
        {
            try
            {
                using (var entities = new Entities())
                {
                    var startDate = DateTime.Now.Date.AddDays((DateTime.Now.Date.Day - 1) * -1);
                    var endDate = startDate.AddMonths(1);
                    var today = DateTime.Today.Date;
                    var tomorrow = today.AddDays(1);

                    var manifest = entities.Integration_SystemGatewayLicence.FirstOrDefault(x => x.IsCurrentStatus) ?? new Integration_SystemGatewayLicence();
                    var thisMonth = entities.Integration_SystemProviderDeliveryLogs
                        .Where(x => x.OperationDate > startDate && x.OperationDate < endDate)
                        .GroupBy(x => x.MessageId)
                        .Select(x => new {
                            MessageId = x.Key,
                            OperationDate = x.Max(y => y.OperationDate)
                        })
                        .ToList();
                    var thisDay = thisMonth.Count(x => x.OperationDate > today && x.OperationDate < tomorrow);

                    return Json(new
                    {
                        Status = true,
                        Message = "Successful",
                        Data = new
                        {
                            TotalUnitsLeft = manifest.CurrentBalance.ToString("#,##0"),
                            SentThisDay = thisDay,
                            SentThisMonth = thisMonth.Count
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
                    if (!entities.Sp_Integration_GetCreditStatus(1).FirstOrDefault().Value)
                        return Json(ResponseData.SendFailMsg(message: "Insufficient Units for Processing"), JsonRequestBehavior.AllowGet);

                    var appointmentDataItem = entities.Integration_SystemAppointmentDataItem
                        .FirstOrDefault(x => x.MessageId == messageId);

                    if (appointmentDataItem != null)
                    {
                        appointmentDataItem.MessageStatus = (int)MessageResponse.Pending;
                        entities.Entry(appointmentDataItem).State = System.Data.Entity.EntityState.Modified;
                        entities.SaveChanges();
                    }

                    var previousOperation = entities.Integration_SystemDeliveryManifest
                        .FirstOrDefault(x => x.MessageId == messageId);

                    if (previousOperation != null)
                    {
                        previousOperation.MessageDate = DateTime.Now;
                        previousOperation.IsDelivered = false;
                        entities.Entry(previousOperation).State = System.Data.Entity.EntityState.Modified;
                        entities.SaveChanges();
                    }

                    new Thread(() => {
                        using (var innerentities = new Entities())
                        {
                            innerentities.Sp_Integration_DeductLicenseCredit(1);
                        }
                    }).Start();

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