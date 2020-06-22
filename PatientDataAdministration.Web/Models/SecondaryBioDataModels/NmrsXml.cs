using System.Collections.Generic;

namespace PatientDataAdministration.Web.Models.SecondaryBioDataModels
{
    public class NmrsXmlIndividualReport
    {
        public PatientDemographics PatientDemographics { get; set; }
        public dynamic Condition { get; set; }
        public dynamic HIVTestingReport { get; set; }
    }
    
    public class TreatmentFacility
    {
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string FacilityTypeCode { get; set; }
    }
    
    public class Identifier
    {
        public string IDNumber { get; set; }
        public string IDTypeCode { get; set; }
    }
    
    public class OtherPatientIdentifiers
    {
        public List<Identifier> Identifier { get; set; }
    }
    
    public class RightHand
    {
        public string RightThumb { get; set; }
        public string RightIndex { get; set; }
        public string RightMiddle { get; set; }
        public string RightWedding { get; set; }
        public string RightSmall { get; set; }
    }
    
    public class LeftHand
    {
        public string LeftThumb { get; set; }
        public string LeftIndex { get; set; }
        public string LeftMiddle { get; set; }
        public string LeftWedding { get; set; }
        public string LeftSmall { get; set; }
    }
    
    public class FingerPrints
    {
        public string DateCaptured { get; set; }
        public RightHand RightHand { get; set; }
        public LeftHand LeftHand { get; set; }
        public string Source { get; set; }
        public string Present { get; set; }
    }

    public class PatientDemographics
    {
        public string PatientIdentifier { get; set; }
        public TreatmentFacility TreatmentFacility { get; set; }
        public OtherPatientIdentifiers OtherPatientIdentifiers { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string PatientSexCode { get; set; }
        public string EnrolleeCode { get; set; }
        public FingerPrints FingerPrints { get; set; }
    }
}
