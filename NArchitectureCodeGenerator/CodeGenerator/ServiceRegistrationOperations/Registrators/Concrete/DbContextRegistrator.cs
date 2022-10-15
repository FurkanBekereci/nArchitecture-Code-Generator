﻿using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Entities;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Abstract;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Concrete
{
    public class DbContextRegistrator : BaseRegistrator
    {
        protected override void ControlUsingStatements(ref List<string> lines, string entityName)
        {
            var usingStatement = $"using {ProjectHelper.DomainProjectName}.Entities;";

            if (!lines.Any(line => line.Contains(usingStatement)))
                lines.Insert(0, usingStatement);
        }

        protected override string GetFilePath()
        {
            return PathHelper.CombinePaths(PathHelper.RootPath, ProjectHelper.PersistenceProjectName, "Contexts", "BaseDbContext.cs");
        }

        protected override string GetLineContentToAdd(string entityName)
        {
            return $"\t\tpublic DbSet<{entityName}> {entityName.Pluralize()} {{ get; set; }}";
        }

        protected override List<LineNumberDecider> GetLineNumberDeciderList()
        {
            return new List<LineNumberDecider>
            {
                new LineNumberDecider(".*\\/\\/\\*\\*\\*DbSet here - autogenerated\\*\\*\\*.*$", 1),
                new LineNumberDecider(".*public DbSet<.*>.*{.*get;.*set;.*}.*", 1),
                new LineNumberDecider(".*public class BaseDbContext : DbContext.*", 0, true)
            };

        }
    }
}
