using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum ReportingType : int
    {
        [EnumDisplayName(DisplayName = "Synchronization Compliance Failure")]
        SyncComplianceFail = 1,
        [EnumDisplayName(DisplayName = "Data Summary: Country")]
        DataSummaryCountry,
        [EnumDisplayName(DisplayName = "Data Summary: State")]
        DataSummaryState,
        [EnumDisplayName(DisplayName = "Data Summary: Site")]
        DataSummarySite
    }
}
