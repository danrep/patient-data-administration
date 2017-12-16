using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class PatientMatching
    {
        public DateTime LastUpdate { get; set; }

        public string PepId { get; set; }
    }
}
