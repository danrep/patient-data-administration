using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum ActionTypeDataIntegrity : int
    {
        [EnumDisplayName(DisplayName = "Create New")]
        CreateNew = 1,
        [EnumDisplayName(DisplayName = "Preffered Record")]
        Preffered = 2,
        [EnumDisplayName(DisplayName = "Delete Record")]
        Delete = 3
    }
}
