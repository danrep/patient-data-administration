using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum SyncMode : int
    {
        [EnumDisplayName(DisplayName = "Remote API")]
        RemoteApi = 1,
        [EnumDisplayName(DisplayName = "On Demand Synchronization")]
        OnDemandSync = 2
    }

    public enum LocalEndPoint : int
    {
        [EnumDisplayName(DisplayName = "CISPro EndPoint Pull API")]
        EndPointCisProPull = 1,
        [EnumDisplayName(DisplayName = "OpenMRS EndPoint Pull API")]
        EndPointOpenMrsPull,
        [EnumDisplayName(DisplayName = "CISPro EndPoint Push API")]
        EndPointCisProPush,
        [EnumDisplayName(DisplayName = "OpenMRS EndPoint Push API")]
        EndPointOpenMrsPush
    }
}
