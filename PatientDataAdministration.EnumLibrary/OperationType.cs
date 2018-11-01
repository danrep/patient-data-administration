using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum OperationType : int
    {
        [EnumDisplayName(DisplayName = "Appointment Reminder")]
        AppointmentReminder = 1,
        [EnumDisplayName(DisplayName = "Biometric Compliance Reminder")]
        BiometricComplianceReminder 
    }
}
