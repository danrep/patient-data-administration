using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class PatientData
    {
        public long RowId { get; set; }
        public string PepId { get; set; }
        public string FingerPrintData { get; set; }
        public FingerPrintPosition FingerPosition { get; set; }
        public FingerPrintStore FingerPrintStore { get; set; }
        public int BioDataSource { get; set; }
    }
}
