using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum PubSubAction : int
    {
        [EnumDisplayName(DisplayName = "Delete Uploaded File")]
        DeleteUploadedFile = 1,
        [EnumDisplayName(DisplayName = "Process Uploaded File")]
        ProcessSecondaryDataUploadedFile,
        [EnumDisplayName(DisplayName = "Instant Deduplication: Client Submission")]
        InstaDedupClientSub
    }
}
