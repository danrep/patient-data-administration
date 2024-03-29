﻿using System.Collections.Generic;
using System.Linq;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Service.Engines.EngineDataIntegrity
{
    public class EngineDuplicatePepId
    {
        public static List<Sp_System_DataIntegrity_PepId_Result> DataIntegrityPepId { get; set; }

        public static void ProcessDataIntegrityPepId()
        {
            using (var entites = new Entities())
            {
                entites.Database.CommandTimeout = 0;
                DataIntegrityPepId = entites.Sp_System_DataIntegrity_PepId().ToList();
            }
        }
    }
}