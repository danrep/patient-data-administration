using CsvHelper.Configuration.Attributes;

namespace QuickOperations.models
{
    public class sites_datim_states_lga
    {
        [Name("state")]
        public string State { get; set; }

        [Name("lga")]
        public string Lga { get; set; }

        [Name("facilityname")]
        public string FacilityName { get; set; }

        [Name("datimcode")]
        public string DatimCode { get; set; }
    }
}
