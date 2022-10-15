using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.Service
{
    public interface ICodeGeneratorService
    {
        void GenerateCodes(string entityName, string rootFolderPath);
    }
}
