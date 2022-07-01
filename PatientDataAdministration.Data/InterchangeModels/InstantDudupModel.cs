using System.Collections.Generic;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class InstantDudupModel
    {
        public string OperationId { get; set; }

        public List<DuplicationSuspect> DuplicationSuspects { get; set; }

        public bool IsDuplicated { get; set; }

        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class DuplicationSuspect
    {
        public string PepId { get; set; }
        public string FingerPosition { get; set; }
        public float MatchScore { get; set; }
        public Administration_SiteInformation SiteInformation { get; set; }
    }
}
