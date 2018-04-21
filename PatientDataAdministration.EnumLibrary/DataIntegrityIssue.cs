using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum DataIntegrityIssue : int
    {
        [EnumDisplayName(DisplayName = "Duplicate Pep Id")]
        DupPepId = 0
    }
}
