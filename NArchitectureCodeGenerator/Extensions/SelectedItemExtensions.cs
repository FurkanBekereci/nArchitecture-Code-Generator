using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace NArchitectureCodeGenerator.Extensions
{
    public static class SelectedItemExtensions
    {

        public static bool IsEntity(this SelectedItem selectedItem)
        {
            return GetFilePath(selectedItem)?.Contains("Domain\\Entities") ?? false;

        }

        public static string GetEntityName(this SelectedItem selectedItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return selectedItem.Name.RemoveExtensionFromString();
        }

        public static string GetFilePath(this SelectedItem selectedItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var filePathStr = selectedItem.ProjectItem?.Properties?.Item("FullPath").Value.ToString();
            return filePathStr;
        }

        //public static string GetParentFolderPath(this SelectedItem selectedItem)
        //{
        //    var filePathStr = GetFilePath(selectedItem);
        //    return PathHelper.GetParentFolderPathOfSelectedEntity(filePathStr);
        //}

        //public static string GetSolutionFolderPath(this SelectedItem selectedItem)
        //{
        //    var filePathStr = GetFilePath(selectedItem);
        //    return PathHelper.GetSolutionFolderPathOfSelectedEntity(filePathStr);
        //}

        //public static List<string> GetContent(this SelectedItem selectedItem)
        //{
        //    return FileHelpers.ReadLinesFromFile(GetFilePath(selectedItem)).ToList();
        //}
    }
}
