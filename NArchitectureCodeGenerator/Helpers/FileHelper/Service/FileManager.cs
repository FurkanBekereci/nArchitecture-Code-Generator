using NArchitectureCodeGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NArchitectureCodeGenerator.Helpers.FileHelper.Service
{
    public class FileManager : IFileService
    {
        public string ReadTemplateFile(string fileName)
        {
            var templateFolderPath = GetTemplatesFolderPath();
            var templateString = ReadFileContent(CombinePaths(templateFolderPath, fileName));
            return templateString;
        }

        public void WriteToFile(string filePath, string content)
        {
            CreateDirectory(filePath);
            File.WriteAllText(filePath, content);
        }

        public void WriteToFileIfFileIsEmpty(string filePath, string content)
        {
            if (IsFileContentExist(filePath)) return;

            WriteToFile(filePath, content);
        }

        public void WriteToFile(string folderPath, string fileName, string content, string extension = "cs")
        {
            WriteToFile(CombinePaths(folderPath, $"{fileName}.{extension}"), content);
        }

        public void CreateFile(string path)
        {
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                fi.Create().Dispose();
            }
        }
        
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        
        public void CreateFileFromPathList(List<string> pathList)
        {
            foreach (var path in pathList)
            {
                CreateDirectory(path);
                CreateFile(path);
            }            
        }

        public List<string> FindFoldersOfGivenPath(string path)
        {            
            return Directory.GetDirectories(path, "*").ToList();
        }

        public List<string> FindFilesOfGivenPath(string path)
        {            
            return Directory.GetFiles(path, "*").ToList();
        }

        public string ExtractFileNameFromPath(string path)
        {
            var startIndex = path.LastIndexOf('\\') + 1;
            var length = path.Length - startIndex;
            return path.Substring(startIndex, length).RemoveExtensionFromString();
        }
        
        public string ExtractFilePathOfEntity(string entityName) => CombinePaths(ProjectHelper.RootPath, ProjectHelper.DomainProjectName, "Entities", $"{entityName}.cs");
        
        public string ExtractEntityTypeConfigurationFilePathOfEntity(string entityName) => CombinePaths(ProjectHelper.RootPath, ProjectHelper.PersistenceProjectName, "EntityTypeConfigurations", $"{entityName}Configuration.cs");
        
        public List<string> GetNamesOfGivePathList(List<string> pathList) => pathList.Select(ExtractFileNameFromPath).ToList();

        public string ReadFileContent(string filePath)
        {

            //Reads text, if error returns empty string
            try
            {
                return File.ReadAllText(filePath) ?? "";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public List<string> ReadLinesFromFile(string filePath)
        {
            try
            {
                return File.ReadLines(filePath)?.ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public void WriteLinesToFile(string filePath, List<string> lines)
        {
            File.WriteAllLines(filePath, lines);   //Add the lines including the new line.   
        }

        public bool IsFileContentExist(string filePath)
        {            
            return ReadFileContent(filePath)?.Length > 0;
        }

        public string GetTemplatesFolderPath()
        {
            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(assembly), "Templates");
            return path;
        }

        public string CombinePaths(params string[] pathArgs)
        {
            return Path.Combine(pathArgs);
        }

        public string GetParentFolderPathOfEntity(string entityFilePath)
        {
            var fileInfo = new FileInfo(entityFilePath);
            var parentFolderPath = fileInfo.Directory.Parent.FullName;
            return parentFolderPath;
        }

        public string GetDirectoryPathOfFile(string path)
        {
            var fileInfo = new FileInfo(path);
            var directoryPath = fileInfo.Directory.FullName;
            return directoryPath;
        }

        public string GetSolutionFolderPathOfEntity(string entityFilePath)
        {
            var fileInfo = new FileInfo(entityFilePath);
            var solutionFolderPath = fileInfo.Directory.Parent.Parent.FullName;
            return solutionFolderPath;
        }

        public List<string> GetNameListOfEntities(string path)
        {
            List<string> filePaths = FindFilesOfGivenPath(path);

            return filePaths.Select(ExtractFileNameFromPath).ToList();
            
        }
    }
}
