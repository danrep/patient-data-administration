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
    
    public partial class Patient_PatientBiometricIntegrityCaseMember
    {
        public int Id { get; set; }
        public int PatientBiometricIntegrityCaseId { get; set; }
        public string PivotPepId { get; set; }
        public string SuspectPepId { get; set; }
        public string MemberTreatmentNote { get; set; }
        public int MemberTreatmentTypeId { get; set; }
        public System.DateTime DateTreated { get; set; }
        public bool IsTreated { get; set; }
        public bool IsDeleted { get; set; }
        public int MatchingScore { get; set; }
    }
}