using PatientDataAdministration.EnumLibrary.Dictionary;

namespace PatientDataAdministration.EnumLibrary
{
    public enum FingerPrintPosition
    {
        [EnumDisplayName(DisplayName = "Left Thumb")]
        LeftThumb = 1,
        [EnumDisplayName(DisplayName = "Right Thumb")]
        RightThumb,
        [EnumDisplayName(DisplayName = "Left Index")]
        LeftIndex,
        [EnumDisplayName(DisplayName = "Right Index")]
        RightIndex,
        [EnumDisplayName(DisplayName = "Left Middle")]
        LeftMiddle,
        [EnumDisplayName(DisplayName = "Right Middle")]
        RightMiddle,
        [EnumDisplayName(DisplayName = "Left Ring")]
        LeftRing,
        [EnumDisplayName(DisplayName = "Right Ring")]
        RightRing,
        [EnumDisplayName(DisplayName = "Left Small")]
        LeftSmall,
        [EnumDisplayName(DisplayName = "Right Small")]
        RightSmall
    }
}
