using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class ProjectHelper
    {
        public static string CurrentEntityName { get; set; }
        public static string ApplicationProjectName { get; private set; }
        public static string DomainProjectName { get; private set; }
        public static string PersistenceProjectName { get; private set; }
        public static string InfrastructureProjectName { get; private set; }
        public static string WebApiProjectName { get; private set; }

        public static void SetProjectNames(List<string> projectList)
        {
            ApplicationProjectName = projectList.Where(l => l.ToLower().Contains("application")).FirstOrDefault()?.Split('\\').Last() ?? "";
            DomainProjectName = projectList.Where(l => l.ToLower().Contains("domain")).FirstOrDefault()?.Split('\\').Last() ?? "";
            InfrastructureProjectName = projectList.Where(l => l.ToLower().Contains("infrastructure")).FirstOrDefault()?.Split('\\').Last() ?? "";
            PersistenceProjectName = projectList.Where(l => l.ToLower().Contains("persistence")).FirstOrDefault()?.Split('\\').Last() ?? "";
            WebApiProjectName = projectList.Where(l => l.ToLower().Contains("webapi")).FirstOrDefault()?.Split('\\').Last() ?? "";
        }
    }
}
