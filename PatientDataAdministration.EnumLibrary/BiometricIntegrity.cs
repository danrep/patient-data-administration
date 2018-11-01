using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum CaseStatus : int
    {
        [EnumDisplayName(DisplayName = "Open")]
        Open = 1,
        [EnumDisplayName(DisplayName = "Closed")]
        Closed,
        [EnumDisplayName(DisplayName = "In Progress")]
        InProgress
    }

    public enum CaseMemberStatus : int
    {
        [EnumDisplayName(DisplayName = "Removed")]
        Removed = 1,
        [EnumDisplayName(DisplayName = "Confirmed")]
        Confirmed,
        [EnumDisplayName(DisplayName = "Undecided")]
        Undecided
    }
}
