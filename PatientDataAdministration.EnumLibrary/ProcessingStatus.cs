using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum ProcessingStatus : int
    {
        [EnumDisplayName(DisplayName = "Submitted")]
        Submitted = 1,
        [EnumDisplayName(DisplayName = "Processing")]
        Processing,
        [EnumDisplayName(DisplayName = "Completed")]
        Completed
    }
}
