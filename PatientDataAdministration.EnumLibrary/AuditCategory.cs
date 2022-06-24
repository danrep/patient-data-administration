using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum AuditCategory : int
    {
        [EnumDisplayName(DisplayName = "Tag Re-Initialization")]
        TagReInit = 1,
        [EnumDisplayName(DisplayName = "Client Authentication")]
        ClientAuth,
        [EnumDisplayName(DisplayName = "Reset BioData Storage")]
        ResetBioDataStore
    }
}
