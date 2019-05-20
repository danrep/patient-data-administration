using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum MessageResponse
    {
        [EnumDisplayName(DisplayName = "Rejected")]
        Rejected = 1,
        [EnumDisplayName(DisplayName = "Delivered")]
        Delivered,
        [EnumDisplayName(DisplayName = "Do Not Disturb")]
        DoNotDisturb,
        [EnumDisplayName(DisplayName = "Pending")]
        Pending,
        [EnumDisplayName(DisplayName = "Processing")]
        Processing,
        [EnumDisplayName(DisplayName = "Failed")]
        Failed,
        [EnumDisplayName(DisplayName = "Error")]
        Error,
        [EnumDisplayName(DisplayName = "Invalid")]
        Invalid,
        [EnumDisplayName(DisplayName = "Expired")]
        Expired,
        [EnumDisplayName(DisplayName = "Undelivered")]
        Undelivered
    }
}
