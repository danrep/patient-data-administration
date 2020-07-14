using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum FingerPrintStore
    {
        [EnumDisplayName(DisplayName = "Primary Bio Data")]
        Primary = 1,
        [EnumDisplayName(DisplayName = "Secondary Bio Data")]
        Secondary
    }
}
