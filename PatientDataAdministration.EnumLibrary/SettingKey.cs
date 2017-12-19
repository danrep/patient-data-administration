using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum SettingKey : int
    {
        [EnumDisplayName(DisplayName = "Remote API")]
        RemoteApi = 1,
        [EnumDisplayName(DisplayName = "On Demand Synchronization")]
        OnDemandSync = 2
    }
}
