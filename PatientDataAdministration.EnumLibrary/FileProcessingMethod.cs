using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum FileProcessingMethod : int
    {
        [EnumDisplayName(DisplayName = "Extracted Patient Dump File")]
        ExPat = 1,
        [EnumDisplayName(DisplayName = "Phone Number Update")]
        PhoneNumberUpdate,
        [EnumDisplayName(DisplayName = "Date of Birth Update")]
        DateBirthUpdate
    }
}
