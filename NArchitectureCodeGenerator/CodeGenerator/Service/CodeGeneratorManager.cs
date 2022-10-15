using Microsoft.VisualStudio.Text.Operations;
using NArchitectureCodeGenerator.CodeGenerator.FileOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.Service
{
    public class CodeGeneratorManager : ICodeGeneratorService
    {

        private readonly IFileOperationService _fileOperationService;
        private readonly IPathTreeOperationService _pathTreeOperationService;
        private readonly IServiceRegistrationService _serviceRegistrationService;
        private readonly ITemplateService _templateService;

        public CodeGeneratorManager(IFileOperationService fileOperationService, 
                                    IPathTreeOperationService pathTreeOperationService, 
                                    IServiceRegistrationService serviceRegistrationService, 
                                    ITemplateService templateService)
        {
            _fileOperationService = fileOperationService;
            _pathTreeOperationService = pathTreeOperationService;
            _serviceRegistrationService = serviceRegistrationService;
            _templateService = templateService;
        }

        public void GenerateCodes(string entityName, string rootFolderPath)
        {
            //Her projeye ait treeler bir liste içinde toplandı
            List<BaseTreeNode> treeList = _pathTreeOperationService.GetTreeList(entityName);

            treeList.ForEach(tree =>
            {
                TraverseAllPath(tree, $"{rootFolderPath}\\{tree.Name}", tree.Name, entityName);
            });

            //Add registrations
            _serviceRegistrationService.RegisterAllRequiredServices(entityName);
        }

        private void TraverseAllPath(BaseTreeNode root, string parentPath, string @namespace, string entityName)
        {

            foreach (var item in root.Children)
            {
                if (item.IsLeaf)
                {
                    //Here is deepest node that is structured leaf node.
                    var leafItem = item as LeafTreeNode;

                    //Caution, this is special for class templates to add namespace
                    //As i think, this if block will not be longer with else ifs
                    if (leafItem.TemplateValueHolder is ITemplateClassHolder)
                    {
                        leafItem.TemplateValueHolder.Namespace = @namespace;
                    }

                    var convertedString = _templateService.CreateConvertedTemplateString(leafItem.TemplateValueHolder);

                    _fileOperationService.WriteToFile(parentPath, leafItem.Name, convertedString);

                    continue;
                }

                TraverseAllPath(item, $"{parentPath}\\{item.Name}", $"{@namespace}.{item.Name}", entityName);

            }
        }
    }
}
