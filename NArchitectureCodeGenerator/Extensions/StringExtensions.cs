using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToLowerCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str.Substring(1);

            return str;
        }

        public static string Pluralize(this string value)
        {
            return PluralizationService
                .CreateService(new CultureInfo("en-US"))
                .Pluralize(value);
        }

        public static string RemoveExtensionFromString(this string value, string extension = null)
        {

            return value?.Split(new[] { $".{extension ?? ""}" }, StringSplitOptions.None)[0];
        }

    }
}
