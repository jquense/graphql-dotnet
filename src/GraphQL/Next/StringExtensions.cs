using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public static class StringExtensions
    {
        public static string NormalizeTypeName(this string s)
        {
            return s.Trim('!').TrimStart('[').TrimEnd(']');
        }

        public static string CamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            return $"{char.ToLowerInvariant(s[0])}{s.Substring(1)}";
        }

        public static string PascalCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            return $"{char.ToUpperInvariant(s[0])}{s.Substring(1)}";
        }
    }
}
