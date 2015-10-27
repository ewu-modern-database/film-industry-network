using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.Utilities
{
    public static class StringExtension
    {
        public static string StripSpecialCharacters(this string str)
        {
            return new Regex(@"[^a-zA-Z0-9]+").Replace(str, "");
        }
    }
}
