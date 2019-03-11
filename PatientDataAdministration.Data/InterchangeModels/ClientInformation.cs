namespace PatientDataAdministration.Data.InterchangeModels
{
    public class ClientInformation
    {
        public string ClientGuid { get; set; }
        public string ClientName { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
        public int CurrentUser { get; set; }
    }
}
