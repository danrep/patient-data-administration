using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum PatientVisitReason : int
    {
        [EnumDisplayName(DisplayName = "Registration")]
        Registration = 0,
        [EnumDisplayName(DisplayName = "Drugs Dispensation")]
        Dispensation,
        [EnumDisplayName(DisplayName = "Laboratory")]
        Laboratory,
        [EnumDisplayName(DisplayName = "Report")]
        Report,
        [EnumDisplayName(DisplayName = "Counselling")]
        Counselling
    }
}
