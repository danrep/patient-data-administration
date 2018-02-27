using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum FileProcessingMethod : int
    {
        [EnumDisplayName(DisplayName = "Extracted Patient Dump File")]
        ExPat = 1
    }
}
