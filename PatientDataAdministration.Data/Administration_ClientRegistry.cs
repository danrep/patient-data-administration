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
    
    public partial class Administration_ClientRegistry
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientMAC { get; set; }
        public int ClientState { get; set; }
        public System.DateTime DateConfigured { get; set; }
        public bool IsDeleted { get; set; }
    }
}
