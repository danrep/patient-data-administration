//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientDataAdministration.Client
{
    using System;
    using System.Collections.Generic;
    
    public partial class System_AuditTrail
    {
        public int Id { get; set; }
        public System.DateTime AuditTimeStamp { get; set; }
        public bool IsRestrcitedOperation { get; set; }
        public string UserPerformed { get; set; }
        public string ActionPerformed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
