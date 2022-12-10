using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class ProjectHelper
    {

        public static List<string> FolderPathsOfProject{ get; set; }
        public static string RootPath { get; set; }
        public static EntityInfo SelectedEntityInfo { get; set; }
        public static string ApplicationProjectName => GetProjectName("application");
        public static string DomainProjectName => GetProjectName("domain");
        public static string PersistenceProjectName => GetProjectName("persistence");
        public static string InfrastructureProjectName => GetProjectName("infrastructure");
        public static string WebApiProjectName => GetProjectName("webapi");

        private static string GetProjectName(string projectType)
        {
            return FolderPathsOfProject.FirstOrDefault(l => l.ToLower().Contains(projectType))?.Split('\\').Last() ?? "";
        }
    }
}
