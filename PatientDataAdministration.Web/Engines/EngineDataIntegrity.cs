using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Engines
{
    public class EngineDataIntegrity
    {
        private static Entities _pdaEntities;

        public static Thread ThreadEngine { get; private set; }
        public static DateTime DateGenerated {get; set; }

        public EngineDataIntegrity()
        {
            ThreadEngine = new Thread(ProcessDataIntegrityPepId);
            _pdaEntities = new Entities();

            if (ThreadEngine.ThreadState == ThreadState.Running)
                return;

            #region Data Integrity Engine
            try
            {
                if (DataIntegrityPepId == null)
                    DataIntegrityPepId = new List<Sp_System_DataIntegrity_PepId_Result>();

                if (!DataIntegrityPepId.Any())
                {
                    ThreadEngine.Start();
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
            #endregion
        }

        private static void ProcessDataIntegrityPepId()
        {
            try
            {
                DataIntegrityPepId = _pdaEntities.Sp_System_DataIntegrity_PepId().ToList();
                DateGenerated = DateTime.Now;
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static List<Sp_System_DataIntegrity_PepId_Result> DataIntegrityPepId { get; set; }
    }
}