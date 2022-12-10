using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.FileHelper.Service
{
    public interface IFileService
    {
        string ReadTemplateFile(string fileName);
        void WriteToFile(string filePath, string content);
        void WriteToFile(string folderName, string fileName, string content, string extension = "cs");
        void WriteToFileIfFileIsEmpty(string filePath, string content);
        void CreateFile(string path);
        void CreateDirectory(string path);
        void CreateFileFromPathList(List<string> pathList);
        List<string> FindFoldersOfGivenPath(string path);
        List<string> FindFilesOfGivenPath(string path);
        string ExtractFileNameFromPath(string path);
        string ExtractFilePathOfEntity(string entityName);
        string ExtractEntityTypeConfigurationFilePathOfEntity(string entityName);
        List<string> GetNamesOfGivePathList(List<string> pathList);
        string ReadFileContent(string filePath);
        List<string> ReadLinesFromFile(string filePath);
        void WriteLinesToFile(string filePath, List<string> lines);
        bool IsFileContentExist(string filePath);

        //Path ile ilgili 
        string GetTemplatesFolderPath();
        string CombinePaths(params string[] pathArgs);
        string GetParentFolderPathOfEntity(string entityFilePath);
        string GetSolutionFolderPathOfEntity(string entityFilePath);
        string GetDirectoryPathOfFile(string path);
        List<string> GetNameListOfEntities(string path);
    }
}
