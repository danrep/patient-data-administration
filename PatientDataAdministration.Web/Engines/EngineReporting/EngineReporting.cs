using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Threading;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Web.Engines.EngineReporting
{
    public class EngineReporting
    {
        public static List<ReportConfiguration> ReportConfigurations;

        public EngineReporting()
        {
            try
            {
                if (ReportConfigurations == null)
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
                ReportConfigurations = new List<ReportConfiguration>();

                foreach (var reportingType in EnumDictionary.GetList<ReportingType>())
                {
                    foreach (var recurrenceInterval in EnumDictionary.GetList<RecurrenceInterval>())
                    {
                        ReportConfigurations.Add(new ReportConfiguration()
                        {
                            Thread = new Thread(() =>
                            {
                                ActivityLogger.Log("WARN", "This Thread has not been Initialized.");
                            }),
                            RecurrenceInterval = recurrenceInterval.ItemId,
                            ReportingType = reportingType.ItemId
                        });
                    }
                }
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
                foreach (var sleepingReportConfiguration in ReportConfigurations.Where(x => !x.IsRunning))
                {
                    var readiness = CheckIfReady(sleepingReportConfiguration.ReportingType,
                        sleepingReportConfiguration.RecurrenceInterval);

                    if (readiness == null)
                        continue;

                    sleepingReportConfiguration.Thread = new Thread(() =>
                    {
                        ProcessReport(sleepingReportConfiguration.ReportingType,
                            sleepingReportConfiguration.RecurrenceInterval, readiness);
                    });
                    sleepingReportConfiguration.Thread.Start();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static ReportReadiness CheckIfReady(int reportingType, int recurrenceInterval)
        {
            try
            {
                using (var entities = new Entities())
                {
                    var lastKnown = entities.System_ReportingLog.FirstOrDefault(x =>
                        x.IsCurrent && !x.IsDeleted && x.ReportId == reportingType &&
                        x.IntervalId == recurrenceInterval);

                    var lowerBound = DateTime.Now;
                    if (lastKnown == null)
                    {
                        switch ((RecurrenceInterval) recurrenceInterval)
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
                                if (DateTime.Now.Date.Subtract(lastKnown.ReportDate.Date).TotalDays < 1)
                                    return null;
                                break;
                            case RecurrenceInterval.Month:
                                if (DateTime.Now.Date.Subtract(lastKnown.ReportDate.Date).TotalDays < 30)
                                    return null;
                                break;
                        }

                        lowerBound = lastKnown.ReportDate;
                    }

                    var upperBound =
                        Transforms.ProcessDateUpperBound(lowerBound, (RecurrenceInterval) recurrenceInterval, out lowerBound);

                    if (upperBound == null)
                        return null;

                    return new ReportReadiness()
                    {
                        LowerBound = lowerBound.Date, 
                        UpperBound = upperBound.Value.Date
                    };
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return null;
            }
        }

        private static void ProcessReport(int reportingType, int recurrenceInterval, ReportReadiness readiness = null)
        {
            try
            {
                var intervalDescription = Transforms.TransformInterval(recurrenceInterval);

                switch ((ReportingType)reportingType)
                {
                    case ReportingType.DataSummaryCountry:
                        break;
                    case ReportingType.DataSummarySite:
                        break;
                    case ReportingType.DataSummaryState:
                        break;
                    //Sync Compliance is a Daily Event Only. 
                    case ReportingType.SyncComplianceFail:
                        if (recurrenceInterval == (int) RecurrenceInterval.Day)
                        {
                            SyncCompliance();
                        }
                        break;
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
                                .Where(x => !x.IsDeleted && x.RoleId == (int) role).Select(x => x.Email).ToList();

                            foreach (var user in users)
                                mailingList += $"{user};";
                        }

                        break;
                    case UserRole.StateAdministrator:
                        using (var entity = new Entities())
                        {
                            if (siteId == 0)
                                users = entity.Administration_StaffInformation
                                    .Where(x => !x.IsDeleted && x.RoleId == (int) role)
                                    .Select(x => x.Email).ToList();
                            else
                            {
                                var allSites = entity.Administration_SiteInformation.Where(x => x.StateId == siteId)
                                    .Select(x => x.Id).ToList();

                                users = entity.Administration_StaffInformation
                                    .Where(x => !x.IsDeleted && x.RoleId == (int) role && allSites.Contains(x.SiteId))
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
                                .Where(x => !x.IsDeleted && x.RoleId == (int) role && x.SiteId == siteId)
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

        public static void SyncCompliance(bool logOperation = true)
        {
            try
            {
                using (var entity = new Entities())
                {
                    var stateIds = entity.System_State.Where(x => !x.IsDeleted).Select(x => x.Id)
                        .ToList();

                    foreach (var stateId in stateIds)
                    {
                        SyncComplianceState(stateId);
                    }
                }

                if (logOperation)
                    LogOperation(RecurrenceInterval.Day, ReportingType.SyncComplianceFail);

                ActivityLogger.Log("INFO", "Sync Compliance Notification Successfully Executed");
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void SyncComplianceState(int stateId)
        {
            try
            {
                using (var entity = new Entities())
                {
                    entity.Database.CommandTimeout = 0;

                    var sitesInState = entity.Administration_SiteInformation.Where(x => x.StateId == stateId).ToList();
                    if (!sitesInState.Any())
                        return;

                    var state = entity.System_State.FirstOrDefault(x => x.Id == stateId);
                    if (state == null)
                        return;

                    var msg = "Dear State Administrator(s)<br />";
                    msg +=
                        $"Its been a while since any data was received from some sites in <b>{state.StateName} State</b>.<br/><br/>";

                    var innerMsg = "";
                    foreach (var site in sitesInState)
                    {
                        innerMsg += GetSiteInformation(site);
                    }
                    
                    msg += innerMsg + $"Kindly assist by looking them up.";

                    if (!string.IsNullOrEmpty(innerMsg))
                        Core.Messaging.SendMail(GetMailingList(UserRole.StateAdministrator, stateId),
                            GetMailingList(UserRole.CountryAdministrator),
                            null, "Operation Compliance Alert", msg, null);
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static string GetSiteInformation(Administration_SiteInformation site)
        {
            try
            {
                using (var entity = new Entities())
                {
                    var dateLimit = DateTime.Now.Date.AddDays(-3);

                    if (entity.Patient_PatientInformation.Any(x =>
                        !x.IsDeleted && x.SiteId == site.Id && DbFunctions.TruncateTime(x.LastUpdated) >= dateLimit))
                        return string.Empty;

                    var siteAdmins = entity.Administration_StaffInformation.Where(x =>
                        x.SiteId == site.Id && x.RoleId == (int) UserRole.SiteAdministrator && !x.IsDeleted).ToList();
                    if (!siteAdmins.Any())
                        return string.Empty;

                    var content = "";
                    content +=
                        $"<b>{site.SiteNameOfficial}:</b> Last Known Communication: {entity.Patient_PatientInformation.Where(x => !x.IsDeleted && x.SiteId == site.Id).OrderByDescending(x => x.LastUpdated).Take(1).FirstOrDefault()?.LastUpdated:MMMM dd, yyyy hh:mm tt}<br/>";
                    content += "Contact Information of Site Adminsitrator(s)<br />";

                    foreach (var siteAdmin in siteAdmins)
                    {
                        content += $"{siteAdmin.FirstName} {siteAdmin.Surname} ({siteAdmin.PhoneNumber})<br />";
                    }

                    content += "<br/>";

                    return content;
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return string.Empty;
            }
        }

        private static void LogOperation(RecurrenceInterval recurrenceInterval, ReportingType reportingType)
        {
            try
            {
                using (var entity = new Entities())
                {
                    var lastOperation = entity.System_ReportingLog.FirstOrDefault(x => x.IsCurrent);
                    if (lastOperation != null)
                    {
                        lastOperation.IsCurrent = false;
                        entity.Entry(lastOperation).State = EntityState.Modified;
                        entity.SaveChanges();
                    }

                    var systemReportingLog = new System_ReportingLog()
                    {
                        IntervalId = (int)recurrenceInterval,
                        IsCurrent = true,
                        IsDeleted = false,
                        ReportDate = DateTime.Now,
                        ReportId = (int)reportingType
                    };

                    entity.System_ReportingLog.Add(systemReportingLog);
                    entity.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }

    public class ReportConfiguration
    {
        public int ReportingType { get; set; }
        public int RecurrenceInterval { get; set; }
        public bool IsRunning => Thread?.ThreadState == ThreadState.Running;
        public Thread Thread { get; set; }
    }

    public class ReportReadiness
    {
        public DateTime LowerBound { get; set; }
        public DateTime UpperBound { get; set; }
    }
}