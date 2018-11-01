using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum RecurrenceInterval : int
    {
        [EnumDisplayName(DisplayName = "Day")]
        Day = 1,
        [EnumDisplayName(DisplayName = "Month")]
        Month,
        [EnumDisplayName(DisplayName = "Year")]
        Year
    }
}
