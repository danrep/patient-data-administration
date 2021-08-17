using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.SecondaryBioDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOperations
{
    class Program
    {
        static List<Administration_SiteInformation> siteInfo;

        static void Main(string[] args)
        {
            siteInfo = new List<Administration_SiteInformation>();

            ExtractPatientData();
        }

        public static void ExtractPatientData()
        {
            Console.WriteLine($"Running Patient Data Update for Secondary Biometrics.");

            while (true)
            {
                UpdateSites();

                using (Entities entity = new Entities())
                {
                    Console.WriteLine($"Loading Data ...");

                    var unassigned = entity.Patient_PatientBiometricDataSecondary
                        .Where(x => !x.IsDeleted && (x.StateId == 0 || x.FacilityId == 0))
                        .OrderBy(x => Guid.NewGuid())
                        .Take(1000)
                        .ToList();

                    if (!unassigned.Any())
                    {
                        Console.WriteLine($"No Unassigned Patients.");
                        break;
                    }

                    Console.WriteLine($"Loaded {unassigned.Count()} records for treatment.");

                    var chunksUnassigned = Transforms.ListChunk(unassigned, 100);

                    Parallel.ForEach(chunksUnassigned, (chunk) =>
                    {
                        foreach (var unassignedMember in chunk)
                        {
                            try
                            {
                                using (var innerEnt = new Entities())
                                {
                                    var xmlDoc = JsonConvert.DeserializeXmlNode(unassignedMember.DataSet);
                                    var patientDemographics = xmlDoc.GetElementsByTagName("PatientDemographics")[0];

                                    var patientDemographicsJson = JsonConvert.SerializeXmlNode(patientDemographics,
                                       Formatting.None, true);
                                    var patientDemographicsParsed =
                                        JsonConvert.DeserializeObject<NmrsXmlPatientDemographics>(patientDemographicsJson);

                                    var facilityData = innerEnt.Administration_SiteInformation
                                        .FirstOrDefault(x => (x.SiteNameOfficial == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim()
                                        || x.SiteNameInformal == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim())
                                        && x.StateId != 0);

                                    if (facilityData == null)
                                    {
                                        if (innerEnt.Administration_SiteInformation
                                        .Any(x => x.SiteNameOfficial == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim()))
                                            continue;

                                        if (siteInfo
                                        .Any(x => x.SiteNameOfficial == patientDemographicsParsed.TreatmentFacility.FacilityName.Trim()))
                                            continue;

                                        siteInfo.Add(new Administration_SiteInformation()
                                        {
                                            IsDeleted = false,
                                            StateId = 0,
                                            LastUpdate = DateTime.Now,
                                            SiteCode = "NA",
                                            SiteCodeExposedInfants = "",
                                            SiteCodePediatric = "",
                                            SiteCodePMTCT = "",
                                            SiteCodeVCT = "",
                                            SiteNameInformal = patientDemographicsParsed.TreatmentFacility.FacilityName.Trim(),
                                            SiteNameOfficial = patientDemographicsParsed.TreatmentFacility.FacilityName.Trim()
                                        });

                                        Console.WriteLine($"Did not find Facility {patientDemographicsParsed.TreatmentFacility.FacilityName.Trim()} for {unassignedMember.PepId}");
                                        continue;
                                    }

                                    var member = innerEnt.Patient_PatientBiometricDataSecondary.FirstOrDefault(x => x.Id == unassignedMember.Id);

                                    member.FacilityId = facilityData.Id;
                                    member.StateId = facilityData.StateId;

                                    innerEnt.Entry(member).State = System.Data.Entity.EntityState.Modified;
                                    innerEnt.SaveChanges();

                                    Console.WriteLine($"Successfully Updated {unassignedMember.PepId} with State ID {facilityData.StateId} and Facility Id {facilityData.Id}");

                                    xmlDoc = null;
                                    patientDemographics = null;
                                    patientDemographicsJson = null;
                                    patientDemographicsParsed = null;
                                    facilityData = null;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message} | {e.InnerException?.Message ?? "No Inner Exception"}");
                            }
                        }

                        chunk = null;
                    });

                    Console.WriteLine("Running Next Batch ... \n\n");

                    chunksUnassigned = null;
                    unassigned = null;
                }
            }            
        }

        public static void UpdateSites()
        {
            if (!siteInfo.Any())
                return;

            using (var entities = new Entities())
            {
                entities.Administration_SiteInformation.AddRange(siteInfo);
                entities.SaveChanges();
                siteInfo = new List<Administration_SiteInformation>();
            }
        }
    }
}
