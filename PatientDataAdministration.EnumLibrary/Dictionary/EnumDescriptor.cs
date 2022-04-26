using System;

namespace PatientDataAdministration.EnumLibrary.Dictionary
{
    public class EnumDisplayNameAttribute: Attribute
    {
        public string DisplayName { get; set; }

        public string NormalizeDisplayName { get; set; }
    }

    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;

            return attribute == null ? value.ToString() : attribute.DisplayName;
        }

        public static string NormalizeDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;

            return (attribute == null ? value.ToString() : attribute.DisplayName).Replace(' ', '_').ToLower();
        }
    }
}
