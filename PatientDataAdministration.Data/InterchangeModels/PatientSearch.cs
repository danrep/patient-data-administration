namespace PatientDataAdministration.Data.InterchangeModels
{
    public class PatientSearch
    {
        public string Query { get; set; }
        public int StateId { get; set; }
        public int SiteId { get; set; }
        public bool HasBio { get; set; }
        public bool HasNfc { get; set; }
    }
}
