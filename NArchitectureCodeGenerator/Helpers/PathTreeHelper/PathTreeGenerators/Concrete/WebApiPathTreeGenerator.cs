using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Concrete
{
    public class WebApiPathTreeGenerator : IPathTreeGenerator
    {
        private NonLeafTreeNode GetControllerTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode controllerFolder = new NonLeafTreeNode("Controllers");
            LeafTreeNode entityController = new LeafTreeNode($"{entityInfo.Name.Pluralize()}Controller");
            entityController.TemplateValueHolder = new ControllerTemplateValueHolder(entityController.Name, entityInfo.Name);

            controllerFolder.Children.Add(entityController);

            return controllerFolder;
        }

        public BaseTreeNode GetTree(EntityInfo entityInfo)
        {
            var webApiProjectName = ProjectHelper.WebApiProjectName;

            NonLeafTreeNode webApiTree = new NonLeafTreeNode(webApiProjectName);
            NonLeafTreeNode controllerTree = GetControllerTreeNode(entityInfo);

            webApiTree.Children.Add(controllerTree);

            return webApiTree;
        }
    }
}
