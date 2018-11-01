using System;
using System.Threading;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class TaskManager
    {
        public Thread ThreadEngine { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
