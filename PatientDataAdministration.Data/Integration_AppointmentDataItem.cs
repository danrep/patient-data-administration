//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientDataAdministration.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Integration_AppointmentDataItem
    {
        public long Id { get; set; }
        public long AppointmentDataManifestId { get; set; }
        public string PepId { get; set; }
        public System.DateTime DateVisit { get; set; }
        public System.DateTime DateAppointment { get; set; }
        public string AppointmentOffice { get; set; }
        public string AppointmentData { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
    }
}
