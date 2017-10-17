using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum Status : int
    {
        [EnumDisplayName(DisplayName = "Active")]
        Active = 1,
        [EnumDisplayName(DisplayName = "Banned")]
        Banned
    }
}
