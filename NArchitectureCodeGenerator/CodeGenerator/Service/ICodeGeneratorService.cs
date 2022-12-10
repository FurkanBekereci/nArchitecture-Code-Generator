using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.Service
{
    public interface ICodeGeneratorService
    {
        void GenerateAllDependentCodesForEntity(EntityInfo entityInfo, string rootFolderPath);
        void GenerateSingleCodeByLeafNode(LeafTreeNode leafItem, string @namespace, string path);
        void GenerateSingleCodeForEntity(EntityInfo entityInfo, ITemplateBaseValueHolder templateBaseValueHolder);
        void GenerateNewEntity(string name, string content);
    }
}
