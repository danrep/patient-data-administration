using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using CsvHelper;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using PatientDataAdministration.Web.Engines.EngineModels;

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
                var lastKnown = LocalCache.Get<List<System_ReportingLog>>("System_ReportingLog").FirstOrDefault(x =>
                        x.ReportId == reportingType &&
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

        private static void ProcessReport(int reportingType, int recurrenceInterval, ReportReadiness reportReadiness = null)
        {
            try
            {
                switch ((ReportingType)reportingType)
                {
                    case ReportingType.DataSummaryCountry:
                        DataSummary(reportReadiness, ReportingType.DataSummaryCountry,
                            recurrenceInterval);
                        break;
                    case ReportingType.DataSummarySite:
                        break;
                        //Too Granular
                        //DataSummary(reportReadiness, ReportingType.DataSummarySite,
                        //    (RecurrenceInterval)recurrenceInterval);
                        //break;
                    case ReportingType.DataSummaryState:
                        DataSummary(reportReadiness, ReportingType.DataSummaryState,
                            recurrenceInterval);
                        break;
                    //Sync Compliance is a Daily Event Only. 
                    case ReportingType.SyncComplianceFail:
                        if (recurrenceInterval == (int) RecurrenceInterval.Day)
                        {
                            SyncCompliance();
                        }
                        break;
                    case ReportingType.PatientDataRegBio:
                        if (recurrenceInterval == (int)RecurrenceInterval.Day)
                        {
                            PatientDataRegBio();
                        }
                        break;
                    case ReportingType.PatientDataPopulation:
                        if (recurrenceInterval == (int)RecurrenceInterval.Day)
                        {
                            PatientDataReg();
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
                    var lastOperation = entity.System_ReportingLog.FirstOrDefault(x =>
                        x.IsCurrent && x.IntervalId == (int) recurrenceInterval && x.ReportId == (int) reportingType);

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

                    LocalCache.RefreshCache("System_ReportingLog");
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static void WriteToFile(IEnumerable<dynamic> list, string expectedFileName)
        {
            try
            {
                using (TextWriter writer =
                    new StreamWriter(expectedFileName, false, System.Text.Encoding.UTF8))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecords(list);
                    csv.Dispose();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        #region Reports
        
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

        private static void DataSummary(ReportReadiness reportReadiness, ReportingType reportingType,
            int recurrenceInterval, bool logOperation = true)
        {
            try
            {
                //if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 11)
                    using (var entity = new Entities())
                    {
                        var intervalDescription = Transforms.TransformInterval(recurrenceInterval);

                        string content;
                        var period = "";

                        switch ((RecurrenceInterval) recurrenceInterval)
                        {
                            case RecurrenceInterval.Day:
                                period = $"{reportReadiness.LowerBound.Date.ToLongDateString()}";
                                break;
                            case RecurrenceInterval.Month:
                                period = $" the month of {reportReadiness.LowerBound.Date:MMMM yyyy}";
                                break;
                        }

                    entity.Database.CommandTimeout = 0;

                    var spSystemIndicatorsPopulationDistroSexSiteStateResults = entity
                        .Sp_System_Indicators_PopulationDistro_SexSiteState(reportReadiness.LowerBound,
                            reportReadiness.UpperBound).ToList();

                        switch (reportingType)
                        {
                            case ReportingType.DataSummaryState:

                                foreach (var state in entity.System_State.Where(x => !x.IsDeleted).ToList())
                                {
                                    content = "Hello Administrator(s),<br />";
                                    content += $"Here is the Report for {period} for your State. {state.StateName}.<br />";

                                    var innerMessage = DataReportingState(
                                        spSystemIndicatorsPopulationDistroSexSiteStateResults
                                            .Where(x => x.StateCode == state.Id.ToString()).ToList());

                                    innerMessage = innerMessage.Replace("***", $"Report for {state.StateName} State for {period}");
                                    content += innerMessage;

                                    if (string.IsNullOrEmpty(innerMessage.Trim()))
                                        continue;

                                    Messaging.SendMail(GetMailingList(UserRole.StateAdministrator, state.Id),
                                        GetMailingList(UserRole.CountryAdministrator),
                                        null, $"{intervalDescription} Registration Summary in {state.StateName} for {period}", content, null);
                                }

                                break;
                            case ReportingType.DataSummaryCountry:

                                if (spSystemIndicatorsPopulationDistroSexSiteStateResults.Sum(x =>
                                        x.PatientPopulation) == 0)
                                {
                                    content = "Hello Administrator(s),<br />";
                                    content +=
                                        $"It would seem that No Data was synchronized (yet) from any Site for {period}.<br />Kindly confirm<br />";

                                    Messaging.SendMail(GetMailingList(UserRole.CountryAdministrator), null,
                                        null, $"{intervalDescription} Registration Summary Execption", content, null);
                                }
                                else
                                {
                                    content = "Hello Administrator(s),<br />";
                                    content += $"Here is the Country Report for {period}.<br /><br />";

                                    foreach (var state in entity.System_State.Where(x => !x.IsDeleted).ToList())
                                    {
                                        if (spSystemIndicatorsPopulationDistroSexSiteStateResults.All(x =>
                                            x.StateCode != state.Id.ToString()))
                                            continue;

                                        var report = DataReportingState(
                                            spSystemIndicatorsPopulationDistroSexSiteStateResults
                                                .Where(x => x.StateCode == state.Id.ToString()).ToList());

                                        if (string.IsNullOrEmpty(report.Trim()))
                                            continue;

                                        report = report.Replace("***",
                                            $"Report for {state.StateName} State for {period}");

                                        content += report;
                                        content += "<br />";
                                    }

                                    Messaging.SendMail(GetMailingList(UserRole.CountryAdministrator), null,
                                        null, $"{intervalDescription} Registration Summary", content, null);
                                }

                                break;
                        }

                        if (logOperation)
                            LogOperation((RecurrenceInterval)recurrenceInterval, reportingType);

                        ActivityLogger.Log("INFO", $"{intervalDescription} Operation Summary Successfully Executed");
                    }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static void PatientDataRegBio(ReportReadiness reportReadiness = null, string recepients = "")
        {
            try
            {
                using (var entity = new Entities())
                {
                    entity.Database.CommandTimeout = 0;

                    var bioDataSummary = new List<Sp_Administration_GetRegBioDataSummary_Result>();
                    var fileNameSuffix = "";

                    var msg = "Dear Administrator(s)<br />";

                    if (reportReadiness == null)
                    {
                        bioDataSummary = entity.Sp_Administration_GetRegBioDataSummary(null, null).ToList();
                        fileNameSuffix = "INI_" + DateTime.Now.Date.ToString("yyyyMMdd");

                        msg +=
                            $"Find Below the Biometric Registration summary as at {DateTime.Now.Date.ToLongDateString()}";
                    }
                    else
                    {
                        bioDataSummary = entity
                            .Sp_Administration_GetRegBioDataSummary(reportReadiness.LowerBound,
                                reportReadiness.UpperBound).ToList();

                        fileNameSuffix =
                            $"RNG_{reportReadiness.LowerBound:yyyyMMdd}_{reportReadiness.UpperBound:yyyyMMdd}";
                        msg +=
                            $"Find attached the Biometric Registration summary from {reportReadiness.LowerBound.ToLongDateString()} till {reportReadiness.UpperBound.AddDays(-1).ToLongDateString()}";
                    }

                    var localDirectory =
                        new DirectoryInfo($"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage");

                    if(!localDirectory.Exists)
                        localDirectory.Create();

                    var expectedFile = Path.Combine(localDirectory.FullName, $"{fileNameSuffix}.csv");

                    WriteToFile(bioDataSummary, expectedFile);

                    Messaging.SendMail(
                        string.IsNullOrEmpty(recepients) ? GetMailingList(UserRole.CountryAdministrator) : recepients,
                        null, null, "Registered Bio Data Summary: " + fileNameSuffix, msg, expectedFile);
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static void PatientDataReg(ReportReadiness reportReadiness = null, string recepients = "")
        {
            try
            {
                using (var entity = new Entities())
                {
                    entity.Database.CommandTimeout = 0;

                    var dataSummary = new List<Sp_Administration_GetRegDataSummary_Result>();
                    var fileNameSuffix = "";

                    var msg = "Dear Administrator(s)<br />";

                    if (reportReadiness == null)
                    {
                        dataSummary = entity.Sp_Administration_GetRegDataSummary(null, null).ToList();
                        fileNameSuffix = "INI_" + DateTime.Now.Date.ToString("yyyyMMdd");

                        msg +=
                            $"Find Below the Biometric Registration summary as at {DateTime.Now.Date.ToLongDateString()}";
                    }
                    else
                    {
                        dataSummary = entity
                            .Sp_Administration_GetRegDataSummary(reportReadiness.LowerBound,
                                reportReadiness.UpperBound).ToList();

                        fileNameSuffix =
                            $"RNG_{reportReadiness.LowerBound:yyyyMMdd}_{reportReadiness.UpperBound:yyyyMMdd}";
                        msg +=
                            $"Find attached the Biometric Registration summary from {reportReadiness.LowerBound.ToLongDateString()} till {reportReadiness.UpperBound.AddDays(-1).ToLongDateString()}";
                    }

                    var localDirectory =
                        new DirectoryInfo($"{HostingEnvironment.ApplicationPhysicalPath}LocalFileStorage");

                    if (!localDirectory.Exists)
                        localDirectory.Create();

                    var expectedFile = Path.Combine(localDirectory.FullName, $"{fileNameSuffix}.csv");

                    WriteToFile(dataSummary, expectedFile);

                    Messaging.SendMail(
                        string.IsNullOrEmpty(recepients) ? GetMailingList(UserRole.CountryAdministrator) : recepients,
                        null, null, "Registered Data Summary: " + fileNameSuffix, msg, expectedFile);
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        private static List<PopulationStat> ConvertPopulationStat(
            List<Sp_System_Indicators_PopulationDistro_SexSiteState_Result> stateData)
        {
            try
            {
                var returnList = new List<PopulationStat>();

                using (var entity = new Entities())
                {
                    foreach (var siteId in stateData.Select(x => x.SiteId).Distinct().ToList())
                    {
                        var siteData = stateData.Where(x => x.SiteId == siteId).ToList();
                        returnList.Add(new PopulationStat()
                        {
                            Females = (siteData.FirstOrDefault(x => x.Sex.ToLower() == "female")?.PatientPopulation ?? 0).ToString("#,##0"),
                            Males = (siteData.FirstOrDefault(x => x.Sex.ToLower() == "male")?.PatientPopulation ?? 0).ToString("#,##0"),
                            SiteName = entity.Administration_SiteInformation.FirstOrDefault(x => x.Id == siteId)?.SiteNameOfficial ?? "NA", 
                            Total = siteData.Sum(x => x.PatientPopulation).ToString()
                        });
                    }
                }

                return returnList.OrderBy(x => x.SiteName).ToList();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return new List<PopulationStat>();
            }
        }

        private static string DataReportingState(List<Sp_System_Indicators_PopulationDistro_SexSiteState_Result> stateData)
        {
            try
            {
                if (!stateData.Any())
                    return string.Empty;

                if (stateData.Sum(x => x.PatientPopulation) == 0)
                    return string.Empty;

                return Messaging.GetHtmlFromList(ConvertPopulationStat(stateData), null, x => x.SiteName,
                    x => x.Females, x => x.Males, x => x.Total);
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
                return string.Empty;
            }
        }

        #endregion
    }

    public class ReportConfiguration
    {
        public int ReportingType { get; set; }
        public int RecurrenceInterval { get; set; }
        public bool IsRunning => Thread?.ThreadState == ThreadState.Running;
        public Thread Thread { get; set; }
    }

    public class PopulationStat
    {
        public string SiteName { get; set; }
        public string Males { get; set; }
        public string Females { get; set; }
        public string Total { get; set; }
    }
}