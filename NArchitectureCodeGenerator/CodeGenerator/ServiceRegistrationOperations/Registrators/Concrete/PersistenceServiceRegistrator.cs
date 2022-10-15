﻿using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Entities;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Abstract;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Concrete
{
    public class PersistenceServiceRegistrator : BaseRegistrator
    {
        protected override void ControlUsingStatements(ref List<string> lines, string entityName)
        {

            var usingStatements = new List<string>
            {
                $"using {ProjectHelper.ApplicationProjectName}.Services.Repositories;",
                $"using {ProjectHelper.PersistenceProjectName}.Repositories;",

            };

            foreach (var usingStatement in usingStatements)
            {
                if (!lines.Any(line => line.Contains(usingStatement)))
                    lines.Insert(0, usingStatement);
            }

        }

        protected override string GetFilePath()
        {
            return PathHelper.CombinePaths(PathHelper.RootPath, ProjectHelper.PersistenceProjectName, "PersistenceServiceRegistration.cs");
        }

        protected override string GetLineContentToAdd(string entityName)
        {
            return $"\t\t\tservices.AddScoped<I{entityName}Repository, {entityName}Repository>();";
        }

        protected override List<LineNumberDecider> GetLineNumberDeciderList()
        {
            return new List<LineNumberDecider>
            {
                new LineNumberDecider(".*\\/\\/\\*\\*\\*Repository here - autogenerated\\*\\*\\*.*$", 1),
                new LineNumberDecider(".*AddScoped<.*>.*", 1),
                new LineNumberDecider(".*public static IServiceCollection AddPersistenceServices.*", 0, true),
                new LineNumberDecider(".*return.*", -1)
            };

        }
    }
}