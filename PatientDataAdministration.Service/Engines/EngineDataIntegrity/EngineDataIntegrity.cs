using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Service.Engines.EngineDataIntegrity
{
    public class EngineDataIntegrity
    {
        public static List<TaskManager> Tasks;

        public EngineDataIntegrity()
        {
            if (EngineDuplicatePepId.DataIntegrityPepId == null)
                EngineDuplicatePepId.DataIntegrityPepId = new List<Sp_System_DataIntegrity_PepId_Result>();

            if (EngineDuplicateBioData.BioDataIntegrityCases == null)
                EngineDuplicateBioData.BioDataIntegrityCases = new List<Patient_PatientBiometricIntegrityCase>();

            if (EngineDuplicateBioDataSecondary.BioDataIntegrityCases == null)
                EngineDuplicateBioDataSecondary.BioDataIntegrityCases = new List<Patient_PatientBiometricSecondaryIntegrityCase>();

            try
            {
                if (Tasks == null)
                    Tasks = new List<TaskManager>();

                foreach (var task in EnumDictionary.GetList<DataIntegrityIssue>())
                {
                    var current = Tasks.FirstOrDefault(x => x.ThreadEngine.Name == task.ItemName);

                    if (current == null)
                    {
                        current = ResolveTask(task.ItemId);

                        if (current == null)
                            continue;

                        Tasks.Add(current);
                    }

                    if (current.ThreadEngine.ThreadState != ThreadState.Running)
                    {
                        current.DateGenerated = DateTime.Now;
                        current.ThreadEngine.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
            }
        }

        private TaskManager ResolveTask(int taskType)
        {
            switch (taskType)
            {
                case (int)DataIntegrityIssue.DupPepId:
                    return new TaskManager()
                    {
                        ThreadEngine = new Thread(EngineDuplicatePepId.ProcessDataIntegrityPepId)
                    };
                case (int)DataIntegrityIssue.DupBioData:
                    return new TaskManager()
                    {
                        ThreadEngine = new Thread(EngineDuplicateBioData.ProcessDataIntegrityBiometric)
                    };
                case (int)DataIntegrityIssue.DupBioDataSecondary:
                    return new TaskManager()
                    {
                        ThreadEngine = new Thread(EngineDuplicateBioDataSecondary.ProcessDataIntegrityBiometric)
                    };
            }

            return null;
        }

        public static bool IsDataIntegrityIssueExist()
        {
            try
            {
                if (EngineDuplicatePepId.DataIntegrityPepId.Any())
                    return true;
                else if (EngineDuplicateBioData.BioDataIntegrityCases.Any())
                    return true;

                return false;
            }
            catch(Exception e)
            {
                ActivityLogger.Log(e);
                return false;
            }
        }
    }
}