using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class ClientInformation
    {
        public string ClientGuid { get; set; }
        public string ClientName { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
        public int CurrentUser { get; set; }
    }
}
