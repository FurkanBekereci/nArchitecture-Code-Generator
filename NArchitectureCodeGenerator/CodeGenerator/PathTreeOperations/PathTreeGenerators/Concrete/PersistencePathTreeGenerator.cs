using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Concrete
{
    public class PersistencePathTreeGenerator : IPathTreeGenerator
    {

        private NonLeafTreeNode GetRepositoriesTreeNode(string entityName)
        {

            NonLeafTreeNode repositoriesFolder = new NonLeafTreeNode("Repositories");
            LeafTreeNode entityRepository = new LeafTreeNode($"{entityName}Repository");
            entityRepository.TemplateValueHolder = new RepositoryTemplateValueHolder(entityRepository.Name, entityName);

            repositoriesFolder.Children.Add(entityRepository);

            return repositoriesFolder;
        }

        private NonLeafTreeNode GetEntityTypeConfigurationsTreeNode(string entityName)
        {
            NonLeafTreeNode entityTypeConfigurationsFolder = new NonLeafTreeNode("EntityTypeConfigurations");
            LeafTreeNode entityTypeConfiguration = new LeafTreeNode($"{entityName}Configuration");
            entityTypeConfiguration.TemplateValueHolder = new EntityTypeConfigurationTemplateValueHolder(entityTypeConfiguration.Name, entityName);

            entityTypeConfigurationsFolder.Children.Add(entityTypeConfiguration);

            return entityTypeConfigurationsFolder;
        }

        public BaseTreeNode GetTree(string entityName)
        {
            var persistenceProjectName = ProjectHelper.PersistenceProjectName;

            NonLeafTreeNode persistenceTree = new NonLeafTreeNode(persistenceProjectName);
            NonLeafTreeNode repositoriesTree = GetRepositoriesTreeNode(entityName);
            NonLeafTreeNode entityTypeConfigurationsTree = GetEntityTypeConfigurationsTreeNode(entityName);

            persistenceTree.Children.Add(repositoriesTree);
            persistenceTree.Children.Add(entityTypeConfigurationsTree);

            return persistenceTree;
        }


    }
}
