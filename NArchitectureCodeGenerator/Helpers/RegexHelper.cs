using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class RegexHelper
    {
        public static bool IsMatch(string line, string pattern)
        {
            return Regex.IsMatch(line, pattern);
        }

        public static List<string> GetFilteredLinesMatchesByPattern(List<string> lines, string pattern)
        {
            return lines.FindAll(line => IsMatch(line, pattern));
        }
    }
}
