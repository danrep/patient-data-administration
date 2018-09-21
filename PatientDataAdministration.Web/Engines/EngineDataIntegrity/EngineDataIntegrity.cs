using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PatientDataAdministration.Core;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.Web.Engines.EngineDataIntegrity
{
    public class EngineDataIntegrity
    {
        public static List<TaskManager> Tasks;

        public EngineDataIntegrity()
        {
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
            }

            return null;
        }

        public static bool IsDataIntegrityIssueExist()
        {
            if (EngineDuplicatePepId.DataIntegrityPepId.Any())
                return true;
            else if (EngineDuplicateBioData.BioDataIntegrityCase.Any())
                return true;

            return false;
        }
    }

    public class TaskManager
    {
        public Thread ThreadEngine { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}