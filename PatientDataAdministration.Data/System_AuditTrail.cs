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
    
    public partial class System_AuditTrail
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime AuditimeStamp { get; set; }
        public int AuditCategory { get; set; }
        public string AuditDetail { get; set; }
        public string AuditData { get; set; }
        public bool IsDeleted { get; set; }
    }
}
