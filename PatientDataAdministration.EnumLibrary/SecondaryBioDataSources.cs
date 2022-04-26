using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum SecondaryBioDataSources : int
    {
        [EnumDisplayName(DisplayName = "NMRS Bio Data XML")]
        NmrsBioDataXml = 1,
        [EnumDisplayName(DisplayName = "NDR Bio Data CSV")]
        NdrBioDataCsv =2
    }
}
