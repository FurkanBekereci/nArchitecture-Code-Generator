using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class PathHelper
    {
        public static string RootPath { get; set; }

        public static string GetTemplatesFolderPath()
        {
            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(assembly), "Templates");
            return path;
        }

        public static string CombinePaths(params string[] pathArgs)
        {
            return Path.Combine(pathArgs);
        }

    }
}
