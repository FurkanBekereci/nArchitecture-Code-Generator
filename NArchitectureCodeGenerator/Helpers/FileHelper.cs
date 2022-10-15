using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class FileHelper
    {
        public static void CreateFile(string path)
        {
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                fi.Create().Dispose();
            }
        }
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        public static void CreateFileFromPathList(List<string> pathList)
        {
            try
            {
                foreach (var path in pathList)
                {
                    CreateDirectory(path);
                    CreateFile(path);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<string> FindFoldersOfGivenPath(string path)
        {
            //List<string> folders = new List<string>();
            try
            {
                return Directory.GetDirectories(path, "*").ToList();
            }
            catch (UnauthorizedAccessException)
            {
                // TODO: Logla
                return new List<string>();
            }
        }

        public static string ReadFileContent(string filePath)
        {

            //Reads text, if error returns empty string
            try
            {
                return File.ReadAllText(filePath) ?? "";
            }
            catch (Exception)
            {
                // TODO: Logla
                return string.Empty;
            }
        }

        public static void WriteToFile(string filePath, string content)
        {

            try
            {
                CreateDirectory(filePath);
                File.WriteAllText(filePath, content);
            }
            catch (Exception e)
            {
                // TODO: Logla
            }
        }

        public static List<string> ReadLinesOfFile(string filePath)
        {
            try
            {
                return File.ReadLines(filePath).ToList();
            }
            catch (Exception)
            {

                // TODO: Logla
                return null;
            }
        }

        public static void AddLinesToFile(string filePath, List<string> lines)
        {

            try
            {
                File.WriteAllLines(filePath, lines);   //Add the lines including the new line.
            }
            catch (Exception)
            {
                // TODO: Loglansın
            }
        }

        public static bool IsFileContentExist(string filePath)
        {
            try
            {
                var lineCount = File.ReadAllLines(filePath).Length;
                return lineCount != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
