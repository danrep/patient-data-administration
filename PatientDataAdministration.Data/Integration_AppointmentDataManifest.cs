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
    
    public partial class Integration_AppointmentDataManifest
    {
        public long Id { get; set; }
        public System.DateTime DateLog { get; set; }
        public int SiteId { get; set; }
        public string ClientId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}