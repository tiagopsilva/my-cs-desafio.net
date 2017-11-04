using System;
using System.Text.RegularExpressions;

namespace AuthAPI.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string value)
        {
            return Regex.Replace(value ?? "", "[^0-9.]", "");
        }

        public static bool EqualsIgnoreCase(this string value, string stringComparation)
        {
            return value?.Equals(stringComparation, StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}