using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enplace.Library
{
    public static partial class RegexDefinitions
    {
        [GeneratedRegex("([A-Z][a-z]+)")]
        public static partial Regex CapitalizedWords();
    }

}
