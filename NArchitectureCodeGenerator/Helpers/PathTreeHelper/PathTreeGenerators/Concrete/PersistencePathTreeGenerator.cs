using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Concrete
{
    public class PersistencePathTreeGenerator : IPathTreeGenerator
    {

        private NonLeafTreeNode GetRepositoriesTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode repositoriesFolder = new NonLeafTreeNode("Repositories");
            LeafTreeNode entityRepository = new LeafTreeNode($"{entityInfo.Name}Repository");
            entityRepository.TemplateValueHolder = new RepositoryTemplateValueHolder(entityRepository.Name, entityInfo.Name);

            repositoriesFolder.Children.Add(entityRepository);

            return repositoriesFolder;
        }

        private NonLeafTreeNode GetEntityTypeConfigurationsTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode entityTypeConfigurationsFolder = new NonLeafTreeNode("EntityTypeConfigurations");
            LeafTreeNode entityTypeConfiguration = new LeafTreeNode($"{entityInfo.Name}Configuration");
            entityTypeConfiguration.TemplateValueHolder = new EntityTypeConfigurationTemplateValueHolder(entityTypeConfiguration.Name, entityInfo.Name, entityInfo.EntityTypeConfigurationContent);

            entityTypeConfigurationsFolder.Children.Add(entityTypeConfiguration);

            return entityTypeConfigurationsFolder;
        }

        public BaseTreeNode GetTree(EntityInfo entityInfo)
        {
            var persistenceProjectName = ProjectHelper.PersistenceProjectName;

            NonLeafTreeNode persistenceTree = new NonLeafTreeNode(persistenceProjectName);
            NonLeafTreeNode repositoriesTree = GetRepositoriesTreeNode(entityInfo);
            NonLeafTreeNode entityTypeConfigurationsTree = GetEntityTypeConfigurationsTreeNode(entityInfo);

            persistenceTree.Children.Add(repositoriesTree);
            persistenceTree.Children.Add(entityTypeConfigurationsTree);

            return persistenceTree;
        }
    }
}
