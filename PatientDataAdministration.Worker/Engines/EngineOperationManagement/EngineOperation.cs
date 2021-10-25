using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.Web.Engines.EngineModels;

namespace PatientDataAdministration.Web.Engines.EngineOperationManagement
{
    public class EngineOperation
    {
        public static List<OperationConfiguration> OperationConfigurations;

        public EngineOperation()
        {
            try
            {
                if (OperationConfigurations == null)
                    InitializeConfiguration();

                StartThreads();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void InitializeConfiguration()
        {
            try
            {
                OperationConfigurations = new List<OperationConfiguration>
                {
                    //Add Appointment Operation
                    new OperationConfiguration()
                    {
                        RecurrenceInterval = (int) RecurrenceInterval.Day,
                        Thread = new Thread(() =>
                        {
                            ActivityLogger.Log("WARN", "This Thread has not been Initialized.");
                        }),
                        OperationType = (int) OperationType.AppointmentReminder
                    },
                    //Add Biometric Compliance Notification Operation
                    new OperationConfiguration()
                    {
                        RecurrenceInterval = (int) RecurrenceInterval.Day,
                        Thread = new Thread(() =>
                        {
                            ActivityLogger.Log("WARN", "This Thread has not been Initialized.");
                        }),
                        OperationType = (int) OperationType.BiometricComplianceReminder
                    }
                };
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void StartThreads()
        {
            try
            {
                foreach (var sleepingOperationConfiguration in OperationConfigurations.Where(x => !x.IsRunning))
                {
                    var readiness = CheckIfReady(sleepingOperationConfiguration.OperationType,
                        sleepingOperationConfiguration.RecurrenceInterval);

                    if (readiness == null)
                        continue;

                    sleepingOperationConfiguration.Thread = new Thread(() =>
                    {
                        ProcessOperation(sleepingOperationConfiguration.OperationType,
                            sleepingOperationConfiguration.RecurrenceInterval, readiness);
                    });
                    sleepingOperationConfiguration.Thread.Start();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static ReportReadiness CheckIfReady(int operationTrack, int recurrenceInterval)
        {
            try
            {
                var lastKnown = LocalCache.Get<List<System_OperationLog>>("System_OperationLog").FirstOrDefault(x =>
                        x.OperationId == operationTrack &&
                        x.IntervalId == recurrenceInterval);

                var lowerBound = DateTime.Now;
                if (lastKnown == null)
                {
                    switch ((RecurrenceInterval)recurrenceInterval)
                    {
                        case RecurrenceInterval.Day:
                            lowerBound = DateTime.Now.Date.AddDays(-1);
                            break;
                        case RecurrenceInterval.Month:
                            lowerBound = DateTime.Now.Date.AddMonths(-1);
                            break;
                    }
                }
                else
                {
                    switch ((RecurrenceInterval)recurrenceInterval)
                    {
                        case RecurrenceInterval.Day:
                            if (DateTime.Now.Date.Subtract(lastKnown.OperationDate.Date).TotalDays < 1)
                                return null;
                            break;
                        case RecurrenceInterval.Month:
                            if (DateTime.Now.Date.Subtract(lastKnown.OperationDate.Date).TotalDays < 30)
                                return null;
                            break;
                    }

                    lowerBound = lastKnown.OperationDate;
                }

                var upperBound =
                    Transforms.ProcessDateUpperBound(lowerBound, (RecurrenceInterval)recurrenceInterval, out lowerBound);

                if (upperBound == null)
                    return null;

                return new ReportReadiness()
                {
                    LowerBound = lowerBound.Date,
                    UpperBound = upperBound.Value.Date
                };
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }

        private static void LogOperation(RecurrenceInterval recurrenceInterval, OperationType operationType)
        {
            try
            {
                using (var entity = new Entities())
                {
                    var lastOperation = entity.System_OperationLog.FirstOrDefault(x =>
                        x.IsCurrent && x.IntervalId == (int)recurrenceInterval && x.OperationId == (int)operationType);

                    if (lastOperation != null)
                    {
                        lastOperation.IsCurrent = false;
                        entity.Entry(lastOperation).State = EntityState.Modified;
                        entity.SaveChanges();
                    }

                    var systemOperationLog = new System_OperationLog()
                    {
                        IntervalId = (int)recurrenceInterval,
                        IsCurrent = true,
                        IsDeleted = false,
                        OperationDate = DateTime.Now,
                        OperationId = (int)operationType
                    };

                    entity.System_OperationLog.Add(systemOperationLog);
                    entity.SaveChanges();

                    LocalCache.RefreshCache("System_OperationLog");
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        #region Operations

        private static void ProcessOperation(int operationType, int recurrenceInterval, ReportReadiness reportReadiness = null)
        {
            try
            {
                switch ((OperationType)operationType)
                {
                    case OperationType.AppointmentReminder:
                        ProcessAppointments();
                        break;
                    case OperationType.BiometricComplianceReminder:
                        ProcessBiometricCompliance();
                        break;
                }

                LogOperation((RecurrenceInterval)recurrenceInterval, (OperationType)operationType);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void ProcessAppointments()
        {
            try
            {
                using (var entity = new Entities())
                {
                    var pendingManifestItems = entity.Integration_AppointmentDataItem
                        .Where(x => x.IsValid && !x.IsDeleted).ToList();

                    if (!pendingManifestItems.Any())
                    {
                        ActivityLogger.Log("INFO", $"No Pending Appointments at the moment");
                        return;
                    }

                    ActivityLogger.Log("INFO", $"Loaded {pendingManifestItems.Count} Pending Appointments");

                    SendAppointment(pendingManifestItems);
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static string GetMailingList(UserRole role, int siteId = 0)
        {
            try
            {
                var mailingList = "";
                var users = new List<string>();

                switch (role)
                {
                    case UserRole.CountryAdministrator:
                        using (var entity = new Entities())
                        {
                            users = entity.Administration_StaffInformation
                                .Where(x => !x.IsDeleted && x.RoleId == (int)role).Select(x => x.Email).ToList();

                            foreach (var user in users)
                                mailingList += $"{user};";
                        }

                        break;
                    case UserRole.StateAdministrator:
                        using (var entity = new Entities())
                        {
                            if (siteId == 0)
                                users = entity.Administration_StaffInformation
                                    .Where(x => !x.IsDeleted && x.RoleId == (int)role)
                                    .Select(x => x.Email).ToList();
                            else
                            {
                                var allSites = entity.Administration_SiteInformation.Where(x => x.StateId == siteId)
                                    .Select(x => x.Id).ToList();

                                users = entity.Administration_StaffInformation
                                    .Where(x => !x.IsDeleted && x.RoleId == (int)role && allSites.Contains(x.SiteId))
                                    .Select(x => x.Email).ToList();
                            }

                            foreach (var user in users)
                                mailingList += $"{user};";
                        }

                        break;
                    case UserRole.SiteAdministrator:
                        using (var entity = new Entities())
                        {
                            users = entity.Administration_StaffInformation
                                .Where(x => !x.IsDeleted && x.RoleId == (int)role && x.SiteId == siteId)
                                .Select(x => x.Email).ToList();
                        }

                        break;
                }

                foreach (var user in users)
                    mailingList += $"{user};";

                return mailingList;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return string.Empty;
            }
        }

        private static void ProcessBiometricCompliance()
        {
            try
            {
                if (!EngineDataIntegrity.EngineDuplicateBioData.BioDataIntegrityCases.Any())
                    return;

                var message = "Dear Administrator(s)<br />";
                message +=
                    $"There are {EngineDataIntegrity.EngineDuplicateBioData.BioDataIntegrityCases.Count} unresolved case around Biometric Data Integrity. Kindly Log In to Resolve them.<br/>";

                Messaging.SendMail(GetMailingList(UserRole.CountryAdministrator), null, null,
                    "Biometric Data Integrity", message, null);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void SendAppointment(List<Integration_AppointmentDataItem> appointmentDataItems)
        {
            try
            {
                using (var entity = new Entities())
                {
                    foreach (var appointmentDataItem in appointmentDataItems)
                    {
                        var patientInfo =
                            entity.Patient_PatientInformation.FirstOrDefault(x => x.PepId == appointmentDataItem.PepId);

                        if (patientInfo == null)
                            continue;

                        //dynamic content generator
                        //var message = RecurrentData.HealthMessages[new Random().Next(RecurrentData.HealthMessages.Length)];
                        var message = "Stay Healthy! See your health care provider regularly. ";

                        //var appointmentDataItemPayload =
                        //    Newtonsoft.Json.JsonConvert.DeserializeObject<AppointmentDataItem[]>(appointmentDataItem
                        //        .AppointmentData);

                        switch (appointmentDataItem.AppointmentOffice.Trim().ToUpper())
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

                        message += $"{appointmentDataItem.DateAppointment:MMMyy}";

                        //if (appointmentDataItemPayload != null)
                        //    message = appointmentDataItemPayload.Aggregate(message,
                        //        (current, appointmentDatumItemPayload) =>
                        //            current + (appointmentDatumItemPayload.ItemValue + " "));

                        message = message.Trim();
                        message += ". Get your FinVite today";

                        var appointment =
                            entity.Integration_AppointmentDataItem.FirstOrDefault(x => x.Id == appointmentDataItem.Id);

                        // just to check that there is an appointment
                        if (appointment == null)
                            continue;

                        object payload;

                        appointment.IsValid = !Messaging.SendSms(Transforms.FormatPhoneNumber(patientInfo.PhoneNumber),
                            message, out payload);
                        entity.Entry(appointment).State = EntityState.Modified;
                        entity.SaveChanges();

                        ActivityLogger.Log("INFO", Newtonsoft.Json.JsonConvert.SerializeObject(payload));
                    }
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        #endregion  
    }

    public class OperationConfiguration
    {
        public int OperationType { get; set; }
        public int RecurrenceInterval { get; set; }
        public bool IsRunning => Thread?.ThreadState == ThreadState.Running;
        public Thread Thread { get; set; }
    }
}