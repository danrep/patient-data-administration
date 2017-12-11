using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataAdministration.Data.InterchangeModels
{
    public class UserCredential
    {
        public Administration_StaffInformation AdministrationStaffInformation { get; set; }
        public Administration_SiteInformation AdministrationSiteInformation { get; set; }
    }
}
