using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Web.Areas.Integration.Models.BaseIntegration;
using Exception = System.Exception;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Web.Areas.Integration.Controllers
{
    public class MessageDeliveryRequestController : BaseIntegrationController
    {
        // GET: Integration/MessageDeliveryRequest
        [HttpPost]
        public JsonResult PushNextAppointment(List<NextAppointment> appointments)
        {
            try
            {
                if (appointments == null)
                    return
                        Json(ResponseData.SendFailMsg(), JsonRequestBehavior.AllowGet);

                ActivityLogger.Log("INTEGRATION_REQUEST_PNA", Newtonsoft.Json.JsonConvert.SerializeObject(appointments));

                using(var entities = new Entities())
                {
                    if (!entities.Sp_Integration_GetCreditStatus(appointments.Count).FirstOrDefault().Value)
                        return Json(ResponseData.SendFailMsg(message: "Insufficient Units for Processing"), JsonRequestBehavior.AllowGet);
                }

                new Thread(() =>
                {
                    try
                    {
                        using (var entities = new Entities())
                        {
                            foreach (var appointment in appointments)
                            {
                                // If PhoneNumber is not passed then search for it.
                                if (string.IsNullOrEmpty(appointment.PhoneNumber))
                                {
                                    appointment.PhoneNumber = entities.Patient_PatientInformation
                                        .FirstOrDefault(x => !x.IsDeleted && x.PepId == appointment.PepId)?.PhoneNumber;
                                }

                                var integrationItem = new Integration_SystemAppointmentDataItem()
                                {
                                    PepId = appointment.PepId,
                                    IsDeleted = false,
                                    AppointmentOffice = appointment.AppointmentOffice,
                                    DateLogged = DateTime.Now,
                                    GeneratedMessage = GenerateMessage(appointment),
                                    AppointmentData =
                                        Newtonsoft.Json.JsonConvert.SerializeObject(appointment.AppointmentData),
                                    AppointmentDate =
                                        Transforms.NormalizeDate(appointment.AppointmentDate) ?? DateTime.Now,
                                    PhoneNumber = appointment.PhoneNumber, 
                                    OperationStatus = false
                                };

                                if (string.IsNullOrEmpty(appointment.PhoneNumber) ||
                                    appointment.PhoneNumber.Length < 10 ||
                                    appointment.PhoneNumber.Contains("000000") ||
                                    !System.Text.RegularExpressions.Regex.IsMatch(appointment.PhoneNumber, "^[0-9]*$"))
                                {
                                    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                    RegisterMessage(integrationItem, appointment, MessageResponse.Failed);
                                }
                                //else if (integrationItem.AppointmentDate < DateTime.Now)
                                //    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                else
                                {
                                    appointment.PhoneNumber = Transforms.FormatPhoneNumber(appointment.PhoneNumber);
                                    integrationItem.PhoneNumber = Transforms.FormatPhoneNumber(appointment.PhoneNumber);

                                    if (!entities.Integration_SystemPhoneNumberBlacklist.Any(x =>
                                        !x.IsDeleted && x.PhoneNumber == integrationItem.PhoneNumber))
                                    {
                                        dynamic payload;

                                        // send message
                                        integrationItem.OperationStatus = Messaging.SendSms(appointment.PhoneNumber,
                                            integrationItem.GeneratedMessage, out payload);

                                        if (payload == null)
                                            integrationItem.MessageStatus = (int)MessageResponse.Pending;
                                        else
                                        {
                                            integrationItem.MessageId = payload["Data"][0]["MessageId"].ToString().ToUpper();
                                            integrationItem.InitialResponsePayload =
                                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                                            integrationItem.MessageStatus = (int)MessageResponse.Processing;
                                            
                                            entities.Integration_SystemAppointmentDataItem.Add(integrationItem);
                                            entities.SaveChanges();

                                            RegisterMessage(integrationItem, appointment, MessageResponse.Processing);
                                        }
                                    }
                                    else
                                        RegisterMessage(integrationItem, appointment, MessageResponse.Delivered);
                                }

                                new Thread(() => {
                                    using (var innerentities = new Entities())
                                    {
                                        innerentities.Sp_Integration_DeductLicenseCredit(1);
                                    }
                                }).Start();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ActivityLogger.Log(e);
                    }
                }).Start();

                return
                    Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendFailMsg(e.ToString()), JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult Push(List<Message> messages)
        {
            try
            {
                if (messages == null)
                    return
                        Json(ResponseData.SendFailMsg(), JsonRequestBehavior.AllowGet);

                ActivityLogger.Log("INTEGRATION_REQUEST_PNA", Newtonsoft.Json.JsonConvert.SerializeObject(messages));

                using (var entities = new Entities())
                {
                    if (!entities.Sp_Integration_GetCreditStatus(messages.Count).FirstOrDefault().Value)
                        return Json(ResponseData.SendFailMsg(message: "Insufficient Units for Processing"), JsonRequestBehavior.AllowGet);
                }

                new Thread(() =>
                {
                    try
                    {
                        using (var entities = new Entities())
                        {
                            foreach (var message in messages)
                            {
                                // If PhoneNumber is not passed then search for it.
                                if (string.IsNullOrEmpty(message.PhoneNumber))
                                {
                                    message.PhoneNumber = entities.Patient_PatientInformation
                                        .FirstOrDefault(x => !x.IsDeleted && x.PepId == message.PepId)?.PhoneNumber;
                                }

                                var integrationItem = new Integration_SystemAppointmentDataItem()
                                {
                                    PepId = message.PepId,
                                    IsDeleted = false,
                                    AppointmentOffice = "M",
                                    DateLogged = DateTime.Now,
                                    GeneratedMessage = message.MessageData,
                                    AppointmentData =
                                        Newtonsoft.Json.JsonConvert.SerializeObject(new object()),
                                    AppointmentDate = DateTime.Now,
                                    PhoneNumber = message.PhoneNumber,
                                    OperationStatus = false
                                };

                                if (string.IsNullOrEmpty(message.PhoneNumber) ||
                                    message.PhoneNumber.Length < 10 ||
                                    message.PhoneNumber.Contains("000000") ||
                                    !System.Text.RegularExpressions.Regex.IsMatch(message.PhoneNumber, "^[0-9]*$"))
                                {
                                    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                    RegisterMessage(integrationItem, message, MessageResponse.Failed);
                                }
                                //else if (integrationItem.AppointmentDate < DateTime.Now)
                                //    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                else
                                {
                                    message.PhoneNumber = Transforms.FormatPhoneNumber(message.PhoneNumber);
                                    integrationItem.PhoneNumber = Transforms.FormatPhoneNumber(message.PhoneNumber);

                                    if (!entities.Integration_SystemPhoneNumberBlacklist.Any(x =>
                                        !x.IsDeleted && x.PhoneNumber == integrationItem.PhoneNumber))
                                    {
                                        dynamic payload;

                                        // send message
                                        integrationItem.OperationStatus = Messaging.SendSms(message.PhoneNumber,
                                            integrationItem.GeneratedMessage, out payload);

                                        if (payload == null)
                                            integrationItem.MessageStatus = (int)MessageResponse.Pending;
                                        else
                                        {
                                            integrationItem.MessageId = payload["Data"][0]["MessageId"].ToString().ToUpper();
                                            integrationItem.InitialResponsePayload =
                                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                                            integrationItem.MessageStatus = (int)MessageResponse.Processing;

                                            entities.Integration_SystemAppointmentDataItem.Add(integrationItem);
                                            entities.SaveChanges();

                                            RegisterMessage(integrationItem, message, MessageResponse.Processing);
                                        }
                                    }
                                    else
                                        RegisterMessage(integrationItem, message, MessageResponse.Delivered);
                                }

                                new Thread(() => {
                                    using (var innerentities = new Entities())
                                    {
                                        innerentities.Sp_Integration_DeductLicenseCredit(1);
                                    }
                                }).Start();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ActivityLogger.Log(e);
                    }
                }).Start();

                return
                    Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendFailMsg(e.ToString()), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetBalance()
        {
            try
            {
                using (var entities = new Entities())
                {
                    return
                        Json(ResponseData.SendSuccessMsg(data: entities.Integration_SystemGatewayLicence
                                .FirstOrDefault(x => x.IsCurrentStatus && !x.IsDeleted)
                                ?.CurrentBalance ?? 0),
                            JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendFailMsg(e.ToString()), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetLog(string startRange, string endRange, int status = 0, string phoneNumber = null)
        {
            try
            {
                using (var entities = new Entities())
                {
                    DateTime startDateRange;
                    DateTime endDateRange;

                    if (!DateTime.TryParseExact(startRange, "yyyyMMddHHmm", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out startDateRange))
                        return Json(
                            ResponseData.SendFailMsg("Invalid Data sent in as Start Date Period. Expected format is yyyyMMddHHmm"), JsonRequestBehavior.AllowGet);

                    if (!DateTime.TryParseExact(endRange, "yyyyMMddHHmm", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out endDateRange))
                        return Json(
                            ResponseData.SendFailMsg("Invalid Data sent in as End Date Period. Expected format is yyyyMMddHHmm"), JsonRequestBehavior.AllowGet);

                    if (startDateRange >= endDateRange)
                        return Json(
                            ResponseData.SendFailMsg("Start Date cannot be later or equal to End Date"), JsonRequestBehavior.AllowGet);

                    if (endDateRange >= DateTime.Now)
                        return Json(
                            ResponseData.SendFailMsg("End Date cannot be later or equal to the future"), JsonRequestBehavior.AllowGet);

                    if (endDateRange.Subtract(startDateRange).TotalDays > 100)
                        return Json(
                            ResponseData.SendFailMsg("Range has exceeded 3 Months. Please revise."), JsonRequestBehavior.AllowGet);

                    var manifest = entities.Integration_SystemDeliveryManifest.Where(x =>
                        !x.IsDeleted && x.MessageDate > startDateRange && x.MessageDate <= endDateRange);

                    // where 1 is true for delivered and 2 is not delivered
                    if (status > 0 && status <= 2)
                        manifest = manifest.Where(x => x.IsDelivered == (status == 1));

                    // further filter by phone number
                    if (!string.IsNullOrEmpty(phoneNumber))
                        manifest = manifest.Where(x => x.PhoneNumber == phoneNumber);

                    return
                        Json(ResponseData.SendSuccessMsg(data: manifest.ToList()
                            .Select(x => new
                            {
                                x.MessageDate, x.IsDelivered, MessageId = x.MessageId.Replace('-', '0').ToUpper(),
                                x.PhoneNumber, RouteKey = Guid.NewGuid().ToString().ToUpper()
                            })
                            .ToList()), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendFailMsg(e.ToString()), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadUnits(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return Json(ResponseData.SendFailMsg("Provide License Key"), JsonRequestBehavior.AllowGet);

                string base64Decoded;
                byte[] data = Convert.FromBase64String(key);
                base64Decoded = System.Text.Encoding.ASCII.GetString(data);

                if (DateTime.Now.ToString("yyyyMMdd").Replace('0', '*') == base64Decoded.Split('|')[0])
                {
                    using (var entities = new Entities())
                    {
                        if (entities.Integration_SystemGatewayLicence.Any(x => x.LoadKey == key))
                            return Json(ResponseData.SendFailMsg("This License Key has been Used"), JsonRequestBehavior.AllowGet);

                        entities.Sp_Integration_AddLicenseCredit(Convert.ToInt64(base64Decoded.Split('|')[1]), key);
                    }

                    return Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(ResponseData.SendFailMsg("Invalid License Key"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(ResponseData.SendFailMsg("Invalid License Key"), JsonRequestBehavior.AllowGet);
            }
        }

        private string GenerateMessage(NextAppointment nextAppointment)
        {
            try
            {
                var message = "Stay Healthy! See your health care provider regularly. ";

                switch (nextAppointment.AppointmentOffice)
                {
                    case "C":
                        message += "CD";
                        break;
                    case "L":
                        message += "VL";
                        break;
                    case "P":
                        message += "DP";
                        break;
                }

                message += $"-{Transforms.NormalizeDate(nextAppointment.AppointmentDate):yyyyMMdd}";

                //if (nextAppointment.AppointmentData != null)
                //    message = nextAppointment.AppointmentData.Aggregate(message,
                //        (current, appointmentDatumItemPayload) =>
                //            current + (appointmentDatumItemPayload.ItemValue + " "));

                message = message.Trim();
                message += ". Get your FinVite today";

                return message;
            }
            catch (Exception e)
            {
               ActivityLogger.Log(e);
                return null;
            }
        }

        private void RegisterMessage(Integration_SystemAppointmentDataItem integrationItem, NextAppointment appointment, MessageResponse messageResponse)
        {
            try
            {
                using(var entities = new Entities())
                {
                    if (messageResponse == MessageResponse.Pending)
                        integrationItem.MessageId = Guid.NewGuid().ToString().Replace('-', '0').ToUpper();

                    var logDate = DateTime.Now;
                    entities.Integration_SystemDeliveryManifest.Add(new Integration_SystemDeliveryManifest()
                    {
                        IsDeleted = false,
                        IsDelivered = messageResponse == MessageResponse.Delivered,
                        MessageDate = logDate,
                        MessageId = integrationItem.MessageId,
                        MessageSupportParams = Newtonsoft.Json.JsonConvert.SerializeObject(integrationItem),
                        PepId = appointment.PepId,
                        PhoneNumber = appointment.PhoneNumber
                    });

                    entities.Integration_SystemProviderDeliveryLogs.Add(new Integration_SystemProviderDeliveryLogs()
                    {
                        IsDeleted = false,
                        IsFinalStatus = true,
                        MessageId = integrationItem.MessageId,
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

        private void RegisterMessage(Integration_SystemAppointmentDataItem integrationItem, Message message, MessageResponse messageResponse)
        {
            try
            {
                using (var entities = new Entities())
                {
                    if (messageResponse == MessageResponse.Pending)
                        integrationItem.MessageId = Guid.NewGuid().ToString().Replace('-', '0').ToUpper();

                    var logDate = DateTime.Now;
                    entities.Integration_SystemDeliveryManifest.Add(new Integration_SystemDeliveryManifest()
                    {
                        IsDeleted = false,
                        IsDelivered = messageResponse == MessageResponse.Delivered,
                        MessageDate = logDate,
                        MessageId = integrationItem.MessageId,
                        MessageSupportParams = Newtonsoft.Json.JsonConvert.SerializeObject(integrationItem),
                        PepId = message.PepId,
                        PhoneNumber = message.PhoneNumber
                    });

                    entities.Integration_SystemProviderDeliveryLogs.Add(new Integration_SystemProviderDeliveryLogs()
                    {
                        IsDeleted = false,
                        IsFinalStatus = true,
                        MessageId = integrationItem.MessageId,
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