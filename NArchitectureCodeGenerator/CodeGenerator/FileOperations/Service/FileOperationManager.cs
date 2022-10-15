using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.FileOperations.Service
{
    public class FileOperationManager : IFileOperationService
    {
        public string ReadTemplateFile(string fileName)
        {
            var templateFolderPath = PathHelper.GetTemplatesFolderPath();
            var templateString = FileHelper.ReadFileContent(PathHelper.CombinePaths(templateFolderPath, fileName));
            return templateString;
        }

        public void WriteToFile(string filePath, string content)
        {
            //Aynı entity üzerine right-click yapıldığında içeriği tekrar yazmaması için dosya içeriği var mı kontrol ediliyor
            if(!FileHelper.IsFileContentExist(filePath))
                FileHelper.WriteToFile(filePath, content);
        }

        public void WriteToFile(string folderPath, string fileName, string content, string extension = "cs")
        {
            WriteToFile(PathHelper.CombinePaths(folderPath, $"{fileName}.{extension}"), content);
        }
    }
}
