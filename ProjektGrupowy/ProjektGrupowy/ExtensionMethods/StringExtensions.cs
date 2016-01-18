using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektGrupowy.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}