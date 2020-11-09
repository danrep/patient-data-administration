using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Models
{
    public class PatientBioDataTrace
    {
        public long RowId { get; set; }
        public string PepId { get; set; }
        public string FingerPrintData { get; set; }
        public FingerPrintPosition FingerPosition { get; set; }
        public string FingerPositionText => FingerPosition.DisplayName();
        public FingerPrintStore FingerPrintStore { get; set; }
        public string FingerPrintStoreText => FingerPrintStore.DisplayName();
    }
}