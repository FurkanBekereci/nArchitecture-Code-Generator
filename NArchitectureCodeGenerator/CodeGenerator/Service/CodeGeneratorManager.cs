using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Service;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Service;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Service;
using System.Collections.Generic;

namespace NArchitectureCodeGenerator.CodeGenerator.Service
{
    public class CodeGeneratorManager : ICodeGeneratorService
    {

        private readonly IFileService _fileService;
        private readonly IPathTreeOperationService _pathTreeOperationService;
        private readonly IFileAppenderService _serviceRegistrationService;
        private readonly ITemplateService _templateService;

        public CodeGeneratorManager(IFileService fileService, 
                                    IPathTreeOperationService pathTreeOperationService, 
                                    IFileAppenderService serviceRegistrationService, 
                                    ITemplateService templateService)
        {
            _fileService = fileService;
            _pathTreeOperationService = pathTreeOperationService;
            _serviceRegistrationService = serviceRegistrationService;
            _templateService = templateService;
        }

        public void GenerateAllDependentCodesForEntity(EntityInfo entityInfo, string rootFolderPath)
        {
            //Her projeye ait treeler bir liste içinde toplandı
            List<BaseTreeNode> treeList = _pathTreeOperationService.GetTreeList(entityInfo);

            treeList.ForEach(tree =>
            {
                TraverseAllPath(tree, $"{rootFolderPath}\\{tree.Name}", tree.Name, entityInfo.Name);
            });

            //Add registrations
            _serviceRegistrationService.AppendLinesForCodeGenerator(entityInfo);
        }

        private void TraverseAllPath(BaseTreeNode root, string parentPath, string @namespace, string entityName)
        {

            foreach (var item in root.Children)
            {
                if (item.IsLeaf)
                {
                    //Here is deepest node that is structured leaf node.
                    GenerateSingleCodeByLeafNode(item as LeafTreeNode, @namespace, parentPath);
                    continue;
                }

                TraverseAllPath(item, $"{parentPath}\\{item.Name}", $"{@namespace}.{item.Name}", entityName);

            }
        }

        public void GenerateSingleCodeForEntity(EntityInfo entityInfo , ITemplateBaseValueHolder templateBaseValueHolder)
        {
            var convertedString = _templateService.CreateConvertedTemplateString(templateBaseValueHolder);
            _fileService.WriteToFile(entityInfo.FilePath, entityInfo.Name, convertedString);
        }

        public void GenerateSingleCodeByLeafNode(LeafTreeNode leafItem, string @namespace, string path)
        {

            //Caution, this is special for class templates to add namespace
            //As i think, this if block will not be longer with else ifs
            if (leafItem.TemplateValueHolder is ITemplateClassHolder)
            {
                leafItem.TemplateValueHolder.Namespace = @namespace;
            }

            var convertedString = _templateService.CreateConvertedTemplateString(leafItem.TemplateValueHolder);

            _fileService.WriteToFile(path, leafItem.Name, convertedString);
        }

        public void GenerateNewEntity(string name, string entityContent)
        {
            var path = _fileService.ExtractFilePathOfEntity(name);
            var @namespace = $"{ProjectHelper.DomainProjectName}.Entities";
            var entityTemplateValueHolder = _templateService.GetEntityTemplateValueHolder(name, @namespace, entityContent);
            var convertedEntityTemplateString = _templateService.CreateConvertedTemplateString(entityTemplateValueHolder);

            _fileService.WriteToFile(path, convertedEntityTemplateString);
        }
    }
}
