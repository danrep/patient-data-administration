using PatientDataAdministration.Core;
using System;
using System.Data.Entity;
using System.Linq;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Web.Engines
{
    public class Cron
    {
        private static bool _isTriggered;
        private static int _currentHour;

        private readonly System.Timers.Timer _recurrentExecution;

        public Cron()
        {
            var timer = new System.Timers.Timer(60000)
            {
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

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

            #region Pending SMS Confirmation
            StatusCheck();
            #endregion

            #region Pending SMS Execute
            PendingSend();
            #endregion

            _recurrentExecution.Enabled = true;
        }

        private void StatusCheck()
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

                    foreach (var processingMessage in pendingConfirmations)
                    {
                        dynamic payload;
                        var messageStatuSms = Messaging.InquireSms(processingMessage.MessageId, out payload);

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
                                 messageStatuSms == MessageResponse.Expired)
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
                        }

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

        private void PendingSend()
        {
            try
            {
                using (var entity = new Entities())
                {
                    var pendingStatus = (int)MessageResponse.Pending;
                    var pendingSends =
                        entity.Integration_SystemAppointmentDataItem.Where(x =>
                            x.MessageStatus == pendingStatus && string.IsNullOrEmpty(x.MessageId) &&
                            x.AppointmentDate > DateTime.Now.Date).ToList();

                    foreach (var pendingSend in pendingSends)
                    {
                        dynamic payload;

                        pendingSend.OperationStatus = Messaging.SendSms(pendingSend.PhoneNumber,
                            pendingSend.GeneratedMessage, out payload);

                        if (payload == null)
                            pendingSend.MessageStatus = (int)EnumLibrary.MessageResponse.Pending;
                        else
                        {
                            var messageId = payload["msg_id"].ToString();

                            pendingSend.MessageId = messageId;
                            pendingSend.InitialResponsePayload =
                                Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                            pendingSend.MessageStatus = (int)EnumLibrary.MessageResponse.Processing;
                        }

                        entity.Entry(pendingSend).State = EntityState.Modified;
                        entity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }
    }
}