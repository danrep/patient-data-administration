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
    
    public partial class Sp_Administration_GetPatients_Result
    {
        public int Id { get; set; }
        public string PepId { get; set; }
        public string PreviousId { get; set; }
        public int SiteId { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string Othername { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public int StateOfOrigin { get; set; }
        public string HouseAddress { get; set; }
        public int HouseAddressState { get; set; }
        public int HouseAddresLga { get; set; }
        public string HospitalNumber { get; set; }
        public byte[] PassportData { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public bool HasBio { get; set; }
        public bool HasNfc { get; set; }
    }
}
