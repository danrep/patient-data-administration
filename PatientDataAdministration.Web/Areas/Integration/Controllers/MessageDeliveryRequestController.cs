using System;
using System.Collections.Generic;
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
                                    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                else if (integrationItem.AppointmentDate < DateTime.Now)
                                    integrationItem.MessageStatus = (int)MessageResponse.Failed;
                                else
                                {
                                    appointment.PhoneNumber = Transforms.FormatPhoneNumber(appointment.PhoneNumber);
                                    integrationItem.PhoneNumber = Transforms.FormatPhoneNumber(appointment.PhoneNumber);

                                    if (!entities.Integration_SystemPhoneNumberBlacklist.Any(x =>
                                        !x.IsDeleted && x.PhoneNumber == integrationItem.PhoneNumber))
                                    {
                                        dynamic payload;

                                        integrationItem.OperationStatus = Messaging.SendSms(appointment.PhoneNumber,
                                            integrationItem.GeneratedMessage, out payload);

                                        if (payload == null)
                                            integrationItem.MessageStatus = (int)MessageResponse.Pending;
                                        else
                                        {
                                            integrationItem.MessageId = payload["msg_id"].ToString().ToUpper();
                                            integrationItem.InitialResponsePayload =
                                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                                            integrationItem.MessageStatus = (int)MessageResponse.Processing;
                                            
                                            entities.Integration_SystemAppointmentDataItem.Add(integrationItem);
                                            entities.SaveChanges();

                                            RegisterMessage(integrationItem, appointment, MessageResponse.Pending);
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

        public JsonResult LoadUnits(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return Json(ResponseData.SendFailMsg("Provide Lincense Key"), JsonRequestBehavior.AllowGet);

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
    }
}