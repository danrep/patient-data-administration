using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
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
                using (var entities = new Entities())
                {
                    States = entities.System_State.Where(x => !x.IsDeleted).ToList();
                    LocalGovermentAreas = entities.System_LocalGovermentArea.Where(x => !x.IsDeleted).ToList();
                    Sites = entities.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }

            try
            {
                //Load Health Text Messages
                var raw = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(
                    "~/Engines/EngineOperationManagement/DataHealthMessages.json"));
                HealthMessages = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(raw);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<System_State> States { get; private set; }

        public static List<System_LocalGovermentArea> LocalGovermentAreas { get; private set; }

        public static List<Administration_SiteInformation> Sites { get; private set; }

        public static string[] HealthMessages { get; private set; } 
    }
}