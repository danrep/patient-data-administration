using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum AuditCategory : int
    {
        [EnumDisplayName(DisplayName = "Tag Re-Initialization")]
        TagReInit = 1
    }
}
