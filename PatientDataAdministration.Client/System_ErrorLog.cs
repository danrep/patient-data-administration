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
    
    public partial class System_ErrorLog
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime ErrorDate { get; set; }
        public string ErrorString { get; set; }
        public string ErrorMessage { get; set; }
        public bool SyncStatus { get; set; }
        public bool IsDeleted { get; set; }
    }
}
