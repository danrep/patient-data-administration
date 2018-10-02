using System.Collections.Generic;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class IntegrationAppointmentDataIngress
    {
        public string PepId { get; set; }
        public string VisitDate { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentOffice { get; set; }
        public object AppointmentData { get; set; }
    }

    public class IntegrationAppointmentDataIngressPayLoad
    {
        public List<IntegrationAppointmentDataIngress> IntegrationAppointmentDataIngresses { get; set; }
        public int SiteId { get; set; }
        public int UserId { get; set; }
        public string ClientId { get; set; }
    }
}
