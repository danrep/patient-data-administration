using System.Collections.Generic;
using System.Linq;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Engines.EngineDataIntegrity
{
    public class EngineDuplicatePepId
    {
        public static List<Sp_System_DataIntegrity_PepId_Result> DataIntegrityPepId { get; set; }

        public static void ProcessDataIntegrityPepId()
        {
            DataIntegrityPepId = new List<Sp_System_DataIntegrity_PepId_Result>();

            using (var entites = new Entities())
            {
                DataIntegrityPepId = entites.Sp_System_DataIntegrity_PepId().ToList();
            }
        }
    }
}