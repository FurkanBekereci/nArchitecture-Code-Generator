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
    public class ApplicationPathTreeGenerator : IPathTreeGenerator
    {
        
        public NonLeafTreeNode GetFeaturesTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode featuresFolder = new NonLeafTreeNode("Features");
            NonLeafTreeNode entityRoot = new NonLeafTreeNode(entityInfo.Name.Pluralize());
            NonLeafTreeNode commandsFolder = GetCommandsTreeNode(entityInfo);
            NonLeafTreeNode constantsFolder = GetConstantsTreeNode(entityInfo);
            NonLeafTreeNode dtosFolder = GetDtosTreeNode(entityInfo);
            NonLeafTreeNode modelsFolder = GetModelsTreeNode(entityInfo);
            NonLeafTreeNode profilesFolder = GetProfilesTreeNode(entityInfo);
            NonLeafTreeNode queriesFolder = GetQueriesTreeNode(entityInfo);
            NonLeafTreeNode rulesFolder = GetRulesTreeNode(entityInfo);

            entityRoot.Children.AddRange(new List<NonLeafTreeNode>
            {
                commandsFolder,
                constantsFolder,
                dtosFolder,
                modelsFolder,
                profilesFolder,
                queriesFolder,
                rulesFolder
            });

            featuresFolder.Children.Add(entityRoot);


            return featuresFolder;
        }

        private NonLeafTreeNode GetCommandsTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode commandsFolder = new NonLeafTreeNode("Commands");

            NonLeafTreeNode createFolder = new NonLeafTreeNode($"Create{entityInfo.Name}");
            LeafTreeNode createCommand = new LeafTreeNode($"Create{entityInfo.Name}Command");
            createCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Create{entityInfo.Name}Command", entityInfo.Name, $"Created{entityInfo.Name}Dto", "Add");

            LeafTreeNode createCommandValidator = new LeafTreeNode($"Create{entityInfo.Name}CommandValidator");
            createCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Create{entityInfo.Name}CommandValidator", $"Create{entityInfo.Name}Command");


            NonLeafTreeNode updateFolder = new NonLeafTreeNode($"Update{entityInfo.Name}");
            LeafTreeNode updateCommand = new LeafTreeNode($"Update{entityInfo.Name}Command");
            updateCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Update{entityInfo.Name}Command", entityInfo.Name, $"Updated{entityInfo.Name}Dto", "Update");

            LeafTreeNode updateCommandValidator = new LeafTreeNode($"Update{entityInfo.Name}CommandValidator");
            updateCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Update{entityInfo.Name}CommandValidator", $"Update{entityInfo.Name}Command");

            NonLeafTreeNode deleteFolder = new NonLeafTreeNode($"Delete{entityInfo.Name}");
            LeafTreeNode deleteCommand = new LeafTreeNode($"Delete{entityInfo.Name}Command");
            deleteCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Delete{entityInfo.Name}Command", entityInfo.Name, $"Deleted{entityInfo.Name}Dto", "Delete");

            LeafTreeNode deleteCommandValidator = new LeafTreeNode($"Delete{entityInfo.Name}CommandValidator");
            deleteCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Delete{entityInfo.Name}CommandValidator", $"Delete{entityInfo.Name}Command");

            createFolder.Children.AddRange(new List<BaseTreeNode> { createCommand, createCommandValidator });
            updateFolder.Children.AddRange(new List<BaseTreeNode> { updateCommand, updateCommandValidator });
            deleteFolder.Children.AddRange(new List<BaseTreeNode> { deleteCommand, deleteCommandValidator });

            commandsFolder.Children.AddRange(new List<BaseTreeNode> { createFolder, updateFolder, deleteFolder });

            return commandsFolder;
        }

        private NonLeafTreeNode GetConstantsTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode constantsFolder = new NonLeafTreeNode("Constants");
            LeafTreeNode constantsClass = new LeafTreeNode($"{entityInfo.Name}Constants");
            constantsClass.TemplateValueHolder = new NormalClassTemplateValueHolder(constantsClass.Name);

            constantsFolder.Children.Add(constantsClass);

            return constantsFolder;
        }
        private NonLeafTreeNode GetDtosTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode dtosFolder = new NonLeafTreeNode("Dtos");
            LeafTreeNode createdEntityDto = new LeafTreeNode($"Created{entityInfo.Name}Dto");
            LeafTreeNode updatedEntityDto = new LeafTreeNode($"Updated{entityInfo.Name}Dto");
            LeafTreeNode deletedEntityDto = new LeafTreeNode($"Deleted{entityInfo.Name}Dto");
            LeafTreeNode entityGetByIdDto = new LeafTreeNode($"{entityInfo.Name}GetByIdDto");
            LeafTreeNode entityListDto = new LeafTreeNode($"{entityInfo.Name}ListDto");

            dtosFolder.Children.AddRange(new List<BaseTreeNode>
            {
                createdEntityDto,
                updatedEntityDto,
                deletedEntityDto,
                entityGetByIdDto,
                entityListDto
            });

            dtosFolder.Children.ForEach(c =>
            {
                (c as LeafTreeNode).TemplateValueHolder = new DtoTemplateValueHolder(c.Name, entityInfo.DtoContent);
            });

            return dtosFolder;
        }
        private NonLeafTreeNode GetModelsTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode modelsFolder = new NonLeafTreeNode("Models");
            LeafTreeNode entityListModel = new LeafTreeNode($"{entityInfo.Name}ListModel");
            entityListModel.TemplateValueHolder = new PageableModelTemplateValueHolder(entityListModel.Name, entityInfo.Name);

            modelsFolder.Children.Add(entityListModel);

            return modelsFolder;
        }
        private NonLeafTreeNode GetProfilesTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode profilesFolder = new NonLeafTreeNode("Profiles");
            LeafTreeNode mappingProfiles = new LeafTreeNode($"MappingProfiles");
            mappingProfiles.TemplateValueHolder = new MapperTemplateValueHolder(entityInfo.Name);

            profilesFolder.Children.Add(mappingProfiles);
            return profilesFolder;
        }
        private NonLeafTreeNode GetQueriesTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode queriesFolder = new NonLeafTreeNode("Queries");

            NonLeafTreeNode getByIdEntityFolder = new NonLeafTreeNode($"GetById{entityInfo.Name}");
            LeafTreeNode getByIdEntityQuery = new LeafTreeNode($"GetById{entityInfo.Name}Query");
            getByIdEntityQuery.TemplateValueHolder = new GetByIdQueryTemplateValueHolder(getByIdEntityQuery.Name, entityInfo.Name, $"{entityInfo.Name}GetByIdDto");

            NonLeafTreeNode getListEntityFolder = new NonLeafTreeNode($"GetList{entityInfo.Name}");
            LeafTreeNode getListEntityQuery = new LeafTreeNode($"GetList{entityInfo.Name}Query");
            getListEntityQuery.TemplateValueHolder = new GetListQueryTemplateValueHolder(getListEntityQuery.Name, entityInfo.Name, $"{entityInfo.Name}ListModel");

            NonLeafTreeNode getListEntityByDynamicFolder = new NonLeafTreeNode($"GetList{entityInfo.Name}ByDynamic");
            LeafTreeNode getListEntityByDynamicQuery = new LeafTreeNode($"GetList{entityInfo.Name}ByDynamicQuery");
            getListEntityByDynamicQuery.TemplateValueHolder = new GetListByDynamicQueryTemplateValueHolder(getListEntityByDynamicQuery.Name, entityInfo.Name, $"{entityInfo.Name}ListModel");

            getByIdEntityFolder.Children.Add(getByIdEntityQuery);
            getListEntityFolder.Children.Add(getListEntityQuery);
            getListEntityByDynamicFolder.Children.Add(getListEntityByDynamicQuery);

            queriesFolder.Children.AddRange(new List<BaseTreeNode>
            {
                getByIdEntityFolder,
                getListEntityFolder,
                getListEntityByDynamicFolder
            });

            return queriesFolder;
        }
        private NonLeafTreeNode GetRulesTreeNode(EntityInfo entityInfo)
        {
            NonLeafTreeNode rulesFolder = new NonLeafTreeNode("Rules");
            LeafTreeNode entityBusinessRules = new LeafTreeNode($"{entityInfo.Name}BusinessRules");
            entityBusinessRules.TemplateValueHolder = new NormalClassTemplateValueHolder(entityBusinessRules.Name);

            rulesFolder.Children.Add(entityBusinessRules);
            return rulesFolder;
        }


        public NonLeafTreeNode GetServicesTreeNode(EntityInfo entityInfo)
        {

            NonLeafTreeNode servicesFolder = new NonLeafTreeNode("Services");
            NonLeafTreeNode repositoriesFolder = new NonLeafTreeNode("Repositories");

            LeafTreeNode iEntityRepository = new LeafTreeNode($"I{entityInfo.Name}Repository");
            iEntityRepository.TemplateValueHolder = new IRepostioryTemplateValueHolder(iEntityRepository.Name, entityInfo.Name);

            repositoriesFolder.Children.Add(iEntityRepository);
            servicesFolder.Children.Add(repositoriesFolder);

            return servicesFolder;
        }
        public BaseTreeNode GetTree(EntityInfo entityInfo)
        {
            var applicationProjectName = ProjectHelper.ApplicationProjectName;

            NonLeafTreeNode applicationTree = new NonLeafTreeNode(applicationProjectName);
            NonLeafTreeNode featuresTree = GetFeaturesTreeNode(entityInfo);
            NonLeafTreeNode servicesTree = GetServicesTreeNode(entityInfo);

            applicationTree.Children.Add(featuresTree);
            applicationTree.Children.Add(servicesTree);

            return applicationTree;
        }
    }
}
