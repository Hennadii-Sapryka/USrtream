using System.Text.RegularExpressions;


namespace Stream.Extensions
{
    public static class DataExtensions
    {
        public static string ToCamelCase(this string value)
        {
            return $"{char.ToLowerInvariant(value[0])}{value[1..]}";
        }

        public static string ToTitleCase(this string value, bool separateWithSpaces = true)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", match => $"{match.Groups[1].Value}{(separateWithSpaces ? " " : "")}{match.Groups[2].Value}");
        }

    }
}
