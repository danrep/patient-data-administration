using System;
using System.Collections.Generic;
using System.Linq;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;

namespace PatientDataAdministration.Web.Models
{
    public class RecurrentData
    {
        public RecurrentData()
        {
            try
            {
                var entities = new Entities();

                States = entities.System_State.Where(x => !x.IsDeleted).ToList();
                LocalGovermentAreas = entities.System_LocalGovermentArea.Where(x => !x.IsDeleted).ToList();
                Sites = entities.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }

        public static List<System_State> States { get; private set; }

        public static List<System_LocalGovermentArea> LocalGovermentAreas { get; private set; }

        public static List<Administration_SiteInformation> Sites { get; private set; }
    }
}