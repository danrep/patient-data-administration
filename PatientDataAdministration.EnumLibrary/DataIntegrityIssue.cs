using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum DataIntegrityIssue : int
    {
        [EnumDisplayName(DisplayName = "Duplicate Pep Id")]
        DupPepId = 0,

        [EnumDisplayName(DisplayName = "Duplicate Biometric Data")]
        DupBioData,

        [EnumDisplayName(DisplayName = "Duplicate Biometric Data Secondary")]
        DupBioDataSecondary
    }
}
