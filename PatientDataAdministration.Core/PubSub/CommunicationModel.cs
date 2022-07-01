using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using System;
using System.Collections.Generic;

namespace PatientDataAdministration.Core.PubSub
{
    public class CommunicationModel
    {
        readonly DateTime timeStamp;

        public CommunicationModel()
        {
            timeStamp = DateTime.Now;
        }

        public DateTime TimeStamp => timeStamp;
        public PubSubAction PubSubAction { get; set; }
        public dynamic Data { get; set; }
    }

    public class SecondaryFileData
    {
        public SecondaryBioDataSources SecondaryBioDataSources { get; set; }
        public List<string> Files { get; set; }
        public bool ForceReplace { get; set; }
        public string NotifyDestination { get; set; }
    }

    public class DeleteFile
    {
        public string File { get; set; }
    }

    public class DedupSubmission
    {
        public string OperationId { get; set; }
        public List<PatientData> PatientDataSubmitted { get; set; }
    }
}
