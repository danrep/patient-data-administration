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
    
    public partial class Patient_PatientTransferHistory
    {
        public int Id { get; set; }
        public string PepId { get; set; }
        public int PreviousSiteId { get; set; }
        public int NewSiteId { get; set; }
        public System.DateTime DateMoved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
