using System;

namespace PatientDataAdministration.Web.Engines.EngineModels
{
    public class ReportReadiness
    {
        public DateTime LowerBound { get; set; }
        public DateTime UpperBound { get; set; }
    }
}