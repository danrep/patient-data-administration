using System.Collections.Generic;

namespace PatientDataAdministration.Web.Engines.EngineDataIntegrity
{
    public class EngineDuplicateBioData
    {
        public static List<BioDataIntegrityCase> BioDataIntegrityCase { get; set; }
        public static bool IsProcessing => true;

        public static void ProcessDataIntegrityBiometric()
        {
        }
    }

    public class BioDataIntegrityCase
    {

    }
}