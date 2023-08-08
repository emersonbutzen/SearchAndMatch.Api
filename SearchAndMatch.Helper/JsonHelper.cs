using System.Text.RegularExpressions;

namespace SearchAndMatch.Helper
{
    public static class JsonHelper
    {
        public static string Compact(string json)
        {
            return Regex.Replace(json, @"(\t|\n|\r|\s)+", string.Empty);
        }

    }
}
