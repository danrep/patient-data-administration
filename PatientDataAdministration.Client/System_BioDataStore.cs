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
    
    public partial class System_BioDataStore
    {
        public int Id { get; set; }
        public string PepId { get; set; }
        public int SiteId { get; set; }
        public string FullName { get; set; }
        public string PrimaryFinger { get; set; }
        public string PrimaryFingerPosition { get; set; }
        public string SecondaryFinger { get; set; }
        public string SecondaryFingerPosition { get; set; }
        public string NfcUid { get; set; }
        public string PatientData { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public System.DateTime LastSync { get; set; }
        public bool IsSync { get; set; }
        public bool IsLocalPush { get; set; }
        public bool IsDeleted { get; set; }
    }
}
