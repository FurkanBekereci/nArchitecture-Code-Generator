using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.FileOperations.Service
{
    public interface IFileOperationService
    {
        string ReadTemplateFile(string fileName);
        void WriteToFile(string filePath, string content);
        void WriteToFile(string folderName, string fileName, string content, string extension = "cs");
    }
}
