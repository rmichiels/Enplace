using System.Text.RegularExpressions;

namespace Enplace.Library
{
    public static partial class RegexDefinitions
    {
        [GeneratedRegex("([A-Z][a-z]+)")]
        public static partial Regex CapitalizedWords();
    }

}
