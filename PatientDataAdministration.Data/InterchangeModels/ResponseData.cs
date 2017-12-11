using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class ResponseData
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public object Data { get; set; }
    }
}
