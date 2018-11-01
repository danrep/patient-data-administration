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
        [EnumDisplayName(DisplayName = "CISPro EndPoint")]
        EndPointCisPro = 1,
        [EnumDisplayName(DisplayName = "Appointment Data EndPoint")]
        EndPointAppointment
    }
}
