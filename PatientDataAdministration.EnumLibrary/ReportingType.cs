using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum ReportingType
    {
        [EnumDisplayName(DisplayName = "System Synchronization Compliance Failure")]
        SyncComplianceFail = 1,
        [EnumDisplayName(DisplayName = "System Data Summary: Country")]
        DataSummaryCountry,
        [EnumDisplayName(DisplayName = "System Data Summary: State")]
        DataSummaryState,
        [EnumDisplayName(DisplayName = "System Data Summary: Site")]
        DataSummarySite,
        [EnumDisplayName(DisplayName = "Patient Data: Registered Biometrics")]
        PatientDataRegBio,
        [EnumDisplayName(DisplayName = "Patient Data: Default")]
        PatientDataPopulation,
        [EnumDisplayName(DisplayName = "Patient Data: Secondary BioData Duplication Report")]
        PatientSecondaryBioDataDeDupRep
    }
}
