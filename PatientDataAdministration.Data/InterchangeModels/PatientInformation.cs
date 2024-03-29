﻿namespace PatientDataAdministration.Data.InterchangeModels
{
    public class PatientInformation
    {
        public Patient_PatientInformation Patient_PatientInformation { get; set; }
        public Patient_PatientBiometricData Patient_PatientBiometricData { get; set; }
        public Patient_PatientNearFieldCommunicationData Patient_PatientNearFieldCommunicationData { get; set; }
        public Administration_StaffInformation Administration_StaffInformation { get; set; }
    }
}
