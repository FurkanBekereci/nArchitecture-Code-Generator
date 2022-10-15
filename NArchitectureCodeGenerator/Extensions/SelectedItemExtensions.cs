using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Extensions
{
    public static class SelectedItemExtensions
    {
        public static bool IsEntity(this SelectedItem selectedItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return selectedItem?.ProjectItem?.Properties?.Item("FullPath").Value.ToString().Contains("Entities") ?? false;

        }

        public static string GetEntityName(this SelectedItem selectedItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return selectedItem.Name.Split(new[] { ".cs" }, StringSplitOptions.None)[0];

        }

        public static string GetSolutionFolderPath(this SelectedItem selectedItem)
        {
            var filePathStr = selectedItem.ProjectItem?.Properties?.Item("FullPath").Value.ToString();
            var fileInfo = new FileInfo(filePathStr);
            var solutionFolderPath = fileInfo.Directory.Parent.Parent.FullName;
            return solutionFolderPath;
        }
    }
}
