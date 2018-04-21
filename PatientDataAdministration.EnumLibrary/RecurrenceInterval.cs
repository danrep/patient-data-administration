using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum RecurrenceInterval : int
    {
        [EnumDisplayName(DisplayName = "Day")]
        Day = 1,
        [EnumDisplayName(DisplayName = "Week")]
        Week,
        [EnumDisplayName(DisplayName = "Month")]
        Month,
        [EnumDisplayName(DisplayName = "Querterly")]
        Querterly,
        [EnumDisplayName(DisplayName = "Bi-Annual")]
        BiAnnual,
        [EnumDisplayName(DisplayName = "Year")]
        Year
    }
}
