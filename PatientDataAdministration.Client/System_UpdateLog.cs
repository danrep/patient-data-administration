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
    
    public partial class System_UpdateLog
    {
        public int Id { get; set; }
        public System.DateTime DateProvided { get; set; }
        public System.DateTime DateDownloaded { get; set; }
        public bool IsDownloaded { get; set; }
        public bool IsApplied { get; set; }
        public bool IsVerified { get; set; }
        public bool VersionNumber { get; set; }
        public string ServerLocation { get; set; }
        public string ServerUsername { get; set; }
        public string ServerPassword { get; set; }
        public string FolderLocation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
