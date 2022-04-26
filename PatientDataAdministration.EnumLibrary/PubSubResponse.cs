using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum PubSubResponse : int
    {
        [EnumDisplayName(DisplayName = "Success")]
        Success = 1,
        [EnumDisplayName(DisplayName = "Error")]
        Error
    }
}
