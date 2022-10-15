using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class ReflectionHelper
    {
        public static string GetConvertedTemplateStringByProperties(ITemplateBaseValueHolder templateValueHolder, string templateString)
        {
            var propertyList = templateValueHolder.GetType().GetProperties().Where(p => p.PropertyType == typeof(string)).ToList();

            propertyList.ForEach(p =>
            {
                templateString = templateString.Replace($"{{{p.Name.FirstCharToLowerCase()}}}", p.GetValue(templateValueHolder).ToString());
            });

            return templateString;
        }
    }
}
