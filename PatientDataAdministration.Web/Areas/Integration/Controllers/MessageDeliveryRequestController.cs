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

                new Thread(() =>
                {
                    try
                    {
                        using (var entities = new Entities())
                        {
                            foreach (var appointment in appointments)
                            {
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
                                    integrationItem.MessageStatus = (int)EnumLibrary.MessageResponse.Failed;
                                else if (integrationItem.AppointmentDate < DateTime.Now)
                                    integrationItem.MessageStatus = (int)EnumLibrary.MessageResponse.Failed;
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
                                            integrationItem.MessageStatus = (int) EnumLibrary.MessageResponse.Pending;
                                        else
                                        {
                                            var messageId = payload["msg_id"].ToString();

                                            integrationItem.MessageId = messageId;
                                            integrationItem.InitialResponsePayload =
                                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                                            integrationItem.MessageStatus = (int) EnumLibrary.MessageResponse.Processing;
                                        }
                                    }
                                }

                                entities.Integration_SystemAppointmentDataItem.Add(integrationItem);
                            }

                            entities.SaveChanges();
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

        public JsonResult PushFailedAppointment()
        {
            try
            {
                return Json(ResponseData.SendSuccessMsg(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ResponseData.SendFailMsg(e.ToString()), JsonRequestBehavior.AllowGet);
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
    }
}