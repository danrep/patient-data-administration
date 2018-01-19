using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum UserRole : int
    {
        [EnumDisplayName(DisplayName = "System Administrator")]
        SystemAdministrator = 0,
        [EnumDisplayName(DisplayName = "Country Administrator")]
        CountryAdministrator,
        [EnumDisplayName(DisplayName = "State Administrator")]
        StateAdministrator,
        [EnumDisplayName(DisplayName = "Site Administrator")]
        SiteAdministrator
    }
}
