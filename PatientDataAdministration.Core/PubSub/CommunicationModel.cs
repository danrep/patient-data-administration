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
        public object Data { get; set; }
    }

    public class SecondaryFileData
    {
        public SecondaryBioDataSources SecondaryBioDataSources { get; set; }
        public List<string> Files { get; set; }
    }
}
