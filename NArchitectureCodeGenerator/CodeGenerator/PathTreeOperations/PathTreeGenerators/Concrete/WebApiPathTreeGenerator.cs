using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Concrete
{
    public class WebApiPathTreeGenerator : IPathTreeGenerator
    {
        private NonLeafTreeNode GetControllerTreeNode(string entityName)
        {

            NonLeafTreeNode controllerFolder = new NonLeafTreeNode("Controllers");
            LeafTreeNode entityController = new LeafTreeNode($"{entityName.Pluralize()}Controller");
            entityController.TemplateValueHolder = new ControllerTemplateValueHolder(entityController.Name, entityName);

            controllerFolder.Children.Add(entityController);

            return controllerFolder;
        }

        public BaseTreeNode GetTree(string entityName)
        {
            var webApiProjectName = ProjectHelper.WebApiProjectName;

            NonLeafTreeNode webApiTree = new NonLeafTreeNode(webApiProjectName);
            NonLeafTreeNode controllerTree = GetControllerTreeNode(entityName);

            webApiTree.Children.Add(controllerTree);

            return webApiTree;
        }
    }
}
