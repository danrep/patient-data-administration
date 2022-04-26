using CsvHelper.Configuration.Attributes;

namespace PatientDataAdministration.Data.SecondaryBioDataModels
{
    public class Ndrcsv
    {
        [Name("date_of_birth")]
        public string DateOfBirth { get; set; }

        [Name("sex")]
        public string Sex { get; set; }

        [Name("pid")]
        public string Pid { get; set; }

        [Name("patient_identifier")]
        public string PatientIdentifier { get; set; }

        [Name("right_thumb")]
        public string RightThumb { get; set; }

        [Name("right_index")]
        public string RightIndex { get; set; }

        [Name("right_middle")]
        public string RightMiddle { get; set; }

        [Name("right_wedding")]
        public string RightWedding { get; set; }

        [Name("right_small")]
        public string RightSmall { get; set; }

        [Name("left_thumb")]
        public string LeftThumb { get; set; }

        [Name("left_index")]
        public string LeftIndex { get; set; }

        [Name("left_middle")]
        public string LeftMiddle { get; set; }

        [Name("left_wedding")]
        public string LeftWedding { get; set; }

        [Name("left_small")]
        public string LeftSmall { get; set; }
    }
}
