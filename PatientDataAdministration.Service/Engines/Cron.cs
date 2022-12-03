using PatientDataAdministration.Core;
using System;
using System.Data.Entity;
using System.Linq;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using System.Threading;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Service.Engines
{
    public class Cron
    {
        private static bool _isTriggered;
        private static int _currentHour;

        private readonly System.Timers.Timer _recurrentExecution;

        public Cron()
        {
            //var timer = new System.Timers.Timer(60000)
            //{
            //    Enabled = true
            //};
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();

            _currentHour = 0;
            _isTriggered = false;

            _recurrentExecution = new System.Timers.Timer(60000)
            {
                Enabled = true
            };
            _recurrentExecution.Elapsed += RecurrentExecution_Elapsed;
            _recurrentExecution.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isTriggered)
                return;
            _isTriggered = true;

            if (_currentHour == DateTime.Now.Hour)
                return;

            _currentHour = DateTime.Now.Hour;

            #region Data Integrity Engine

            try
            {
                new EngineDataIntegrity.EngineDataIntegrity();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Reporting Engine

            try
            {
                new EngineReporting.EngineReporting();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Operation Engine

            try
            {
                new EngineOperationManagement.EngineOperation();
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            #region Misc Operations

            try
            {
                using (var pdaEntities = new Entities())
                {
                    var pendingHashes = pdaEntities.Patient_PatientBiometricData
                        .Where(x => !x.IsDeleted && string.IsNullOrEmpty(x.FingerDataHash)).Take(100).ToList();

                    foreach (var pendingHash in pendingHashes)
                    {
                        pendingHash.FingerDataHash =
                            Sha512Engine.GenerateSHA512String(
                                pendingHash.FingerPrimary + "|" + pendingHash.FingerSecondary);
                        pdaEntities.Entry(pendingHash).State = System.Data.Entity.EntityState.Modified;
                        pdaEntities.SaveChanges();
                    }

                    pendingHashes = null;
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }

            #endregion

            GC.Collect();
            GC.WaitForPendingFinalizers();

            _isTriggered = false;
        }

        private void RecurrentExecution_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _recurrentExecution.Enabled = false;

            #region Pending SMS Appointment Confirmation
            StatusCheckAppointment();
            #endregion

            #region Pending SMS Appointment Execute
            PendingSendAppointment();
            #endregion

            #region Pending SMS General 
            PendingSendGeneral();
            #endregion

            _recurrentExecution.Enabled = true;
        }

        private void StatusCheckAppointment()
        {
            #region SMS Status Check
            try
            {
                using (var entity = new Entities())
                {
                    var pendingStatus = (int)MessageResponse.Processing;

                    var pendingConfirmations =
                        entity.Integration_SystemAppointmentDataItem.Where(x =>
                            x.OperationStatus && x.MessageStatus == pendingStatus).ToList();

                    if (!pendingConfirmations.Any())
                    {
                        pendingStatus = (int)MessageResponse.Invalid;
                        pendingConfirmations =
                            entity.Integration_SystemAppointmentDataItem.Where(x =>
                                x.MessageStatus == pendingStatus && !string.IsNullOrEmpty(x.MessageId)).ToList();
                    }

                    if (!pendingConfirmations.Any())
                    {
                        pendingStatus = (int)MessageResponse.Pending;
                        pendingConfirmations =
                            entity.Integration_SystemAppointmentDataItem.Where(x =>
                                x.MessageStatus == pendingStatus && !string.IsNullOrEmpty(x.MessageId)).ToList();
                    }

                    ActivityLogger.Log("INFO", $"Processing {pendingConfirmations.Count()} messages in StatusCheckAppointment");

                    foreach (var processingMessage in pendingConfirmations)
                    {
                        dynamic payload;
                        var messageStatuSms = Messaging.InquireSms(processingMessage.MessageId, out payload);

                        ActivityLogger.Log("TRACE", $"{processingMessage.PhoneNumber}:{processingMessage.MessageId}:{messageStatuSms.DisplayName()}");

                        if (messageStatuSms == MessageResponse.Error)
                            continue;

                        processingMessage.MessageStatus = (int)messageStatuSms;
                        processingMessage.FinalResponsePayload =
                            Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                        if (messageStatuSms == MessageResponse.Delivered)
                        {
                            processingMessage.OperationStatus = true;
                        }
                        else if (messageStatuSms == MessageResponse.DoNotDisturb ||
                                 messageStatuSms == MessageResponse.Rejected ||
                                 messageStatuSms == MessageResponse.Invalid ||
                                 messageStatuSms == MessageResponse.Undelivered ||
                                 messageStatuSms == MessageResponse.Submitted ||
                                 messageStatuSms == MessageResponse.Expired ||
                                 (messageStatuSms == MessageResponse.Processing &&
                                  DateTime.Now.Subtract(processingMessage.DateLogged).TotalHours > 12))
                        {
                            if (!entity.Integration_SystemPhoneNumberBlacklist
                                .Any(x => x.PhoneNumber == processingMessage.PhoneNumber && 
                                            !x.IsDeleted && 
                                            x.LastOperationStatus == processingMessage.MessageStatus))
                            {
                                entity.Integration_SystemPhoneNumberBlacklist.Add(
                                new Integration_SystemPhoneNumberBlacklist()
                                {
                                    PhoneNumber = processingMessage.PhoneNumber,
                                    IsDeleted = false,
                                    DateLogged = DateTime.Now,
                                    LastOperationStatus = processingMessage.MessageStatus
                                });

                                processingMessage.OperationStatus = false;

                                if (messageStatuSms == MessageResponse.Invalid)
                                    processingMessage.MessageStatus = (int)MessageResponse.Expired;
                            }
                        }

                        RegisterMessage(processingMessage.Id, processingMessage.MessageId, MessageResponse.Delivered);
                        entity.Entry(processingMessage).State = EntityState.Modified;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
            #endregion
        }

        private void PendingSendAppointment()
        {
            try
            {
                using (var entity = new Entities())
                {
                    var currentDate = DateTime.Now.Date;
                    var pendingStatus = (int)MessageResponse.Pending;
                    var pendingSends =
                        entity.Integration_SystemAppointmentDataItem.Where(x =>
                            x.MessageStatus == pendingStatus && string.IsNullOrEmpty(x.MessageId) &&
                            x.AppointmentDate > currentDate).ToList();

                    ActivityLogger.Log("INFO", $"Processing {pendingSends.Count()} messages in PendingSendAppointment");

                    if (!entity.Sp_Integration_GetCreditStatus(pendingSends.Count).FirstOrDefault().Value)
                        return;

                    foreach (var pendingSend in pendingSends)
                    {
                        dynamic payload;

                        pendingSend.OperationStatus = Messaging.SendSms(pendingSend.PhoneNumber,
                            pendingSend.GeneratedMessage, out payload);

                        if (payload == null)
                            pendingSend.MessageStatus = (int)MessageResponse.Pending;
                        else
                        {
                            var messageId = ((dynamic)payload)["Data"][0]["MessageId"];

                            pendingSend.MessageId = messageId;
                            pendingSend.InitialResponsePayload =
                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                            pendingSend.MessageStatus = (int)MessageResponse.Processing;

                            RegisterMessage(pendingSend.Id, messageId, MessageResponse.Processing);
                        }

                        entity.Entry(pendingSend).State = EntityState.Modified;
                        entity.SaveChanges();

                        ActivityLogger.Log("TRACE", $"{pendingSend.PhoneNumber}:{pendingSend.MessageId}:{((MessageResponse)pendingSend.MessageStatus).DisplayName()}");

                        new Thread(()=> {
                            using(var entities = new Entities())
                            {
                                entities.Sp_Integration_DeductLicenseCredit(1);
                            }
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        private void PendingSendGeneral()
        {
            try
            {
                using (var entity = new Entities())
                {
                    var currentDate = DateTime.Now.Date;

                    var pendingSends =
                        entity.Integration_SystemDeliveryManifest.Where(x =>
                            !x.IsDelivered && 
                            DbFunctions.TruncateTime(x.MessageDate) == currentDate).ToList();

                    ActivityLogger.Log("INFO", $"Processing {pendingSends.Count()} messages in PendingSendGeneral");

                    if (!entity.Sp_Integration_GetCreditStatus(pendingSends.Count).FirstOrDefault().Value)
                        return;

                    foreach (var pendingSend in pendingSends)
                    {
                        dynamic payload;
                        var message = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(pendingSend.MessageSupportParams)["GeneratedMessage"];

                        pendingSend.IsDelivered = Messaging.SendSms(pendingSend.PhoneNumber, message.ToString(), out payload);

                        if (payload != null)
                        {
                            var messageId = payload["Data"][0]["MessageId"].ToString().ToUpper();

                            pendingSend.MessageId = messageId;
                            pendingSend.MessageSupportParams =
                                Newtonsoft.Json.JsonConvert.SerializeObject(new { StatusMessage = "Successful 00", GeneratedMessage = message, Payload = payload });

                            RegisterMessage(pendingSend.Id, pendingSend.MessageId, MessageResponse.Delivered);
                            ActivityLogger.Log("TRACE", $"{pendingSend.PhoneNumber}:{pendingSend.MessageId}:{MessageResponse.Delivered.DisplayName()}");
                        }

                        entity.Entry(pendingSend).State = EntityState.Modified;
                        entity.SaveChanges();

                        new Thread(() => {
                            using (var entities = new Entities())
                            {
                                entities.Sp_Integration_DeductLicenseCredit(1);
                            }
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        private void RegisterMessage(long id, string messageId, MessageResponse messageResponse)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var currentManifest =
                        entities.Integration_SystemDeliveryManifest.FirstOrDefault(x =>
                            x.Id == id || x.MessageId == messageId);

                    if(currentManifest != null)
                    {
                        currentManifest.IsDelivered = messageResponse == MessageResponse.Delivered;
                        entities.Entry(currentManifest).State = EntityState.Modified;
                    }

                    entities.Integration_SystemProviderDeliveryLogs.Add(new Integration_SystemProviderDeliveryLogs()
                    {
                        IsDeleted = false,
                        IsFinalStatus = true,
                        MessageId = messageId,
                        OperationDate = DateTime.Now,
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