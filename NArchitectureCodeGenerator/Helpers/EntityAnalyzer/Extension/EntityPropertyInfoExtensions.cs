using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Extension
{
    public static class EntityPropertyInfoExtensions
    {
        public static string ToEntityTypeConfigurationLine(this EntityPropertyInfo entityPropertyInfo)
        {
            if (entityPropertyInfo.IsBuiltInType) return $"builder.Property(p => p.{entityPropertyInfo.Name}).HasColumnName(\"{entityPropertyInfo.Name}\");";

            if (entityPropertyInfo.IsArray) return $"builder.HasMany(p => p.{entityPropertyInfo.Name});";

            return $"builder.HasOne(p => p.{entityPropertyInfo.Name});";
        }
    }
}
