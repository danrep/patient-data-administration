using System;

namespace PatientDataAdministration.Service.Engines.EngineModels
{
    public class ReportReadiness
    {
        public DateTime LowerBound { get; set; }
        public DateTime UpperBound { get; set; }
    }
}