using System;

namespace PatientDataAdministration.Web.Areas.Integration.Models.BaseIntegration
{
    public class NextAppointment
    {
        public string PepId { get; set; }
        public string PhoneNumber { get; set; }
        public string AppointmentOffice { get; set; }
        public string AppointmentData { get; set; }
        public string AppointmentDate { get; set; }
    }
}