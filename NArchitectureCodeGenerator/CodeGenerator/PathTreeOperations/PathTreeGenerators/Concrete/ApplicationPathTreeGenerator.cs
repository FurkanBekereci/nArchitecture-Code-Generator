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
    public class ApplicationPathTreeGenerator : IPathTreeGenerator
    {
        public NonLeafTreeNode GetFeaturesTreeNode(string entityName)
        {

            NonLeafTreeNode featuresFolder = new NonLeafTreeNode("Features");
            NonLeafTreeNode entityRoot = new NonLeafTreeNode(entityName.Pluralize());
            NonLeafTreeNode commandsFolder = GetCommandsTreeNode(entityName);
            NonLeafTreeNode constantsFolder = GetConstantsTreeNode(entityName);
            NonLeafTreeNode dtosFolder = GetDtosTreeNode(entityName);
            NonLeafTreeNode modelsFolder = GetModelsTreeNode(entityName);
            NonLeafTreeNode profilesFolder = GetProfilesTreeNode(entityName);
            NonLeafTreeNode queriesFolder = GetQueriesTreeNode(entityName);
            NonLeafTreeNode rulesFolder = GetRulesTreeNode(entityName);

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

        private NonLeafTreeNode GetCommandsTreeNode(string entityName)
        {
            NonLeafTreeNode commandsFolder = new NonLeafTreeNode("Commands");

            NonLeafTreeNode createFolder = new NonLeafTreeNode($"Create{entityName}");
            LeafTreeNode createCommand = new LeafTreeNode($"Create{entityName}Command");
            createCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Create{entityName}Command", entityName, $"Created{entityName}Dto", "Add");

            LeafTreeNode createCommandValidator = new LeafTreeNode($"Create{entityName}CommandValidator");
            createCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Create{entityName}CommandValidator", $"Create{entityName}Command");


            NonLeafTreeNode updateFolder = new NonLeafTreeNode($"Update{entityName}");
            LeafTreeNode updateCommand = new LeafTreeNode($"Update{entityName}Command");
            updateCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Update{entityName}Command", entityName, $"Updated{entityName}Dto", "Update");

            LeafTreeNode updateCommandValidator = new LeafTreeNode($"Update{entityName}CommandValidator");
            updateCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Update{entityName}CommandValidator", $"Update{entityName}Command");

            NonLeafTreeNode deleteFolder = new NonLeafTreeNode($"Delete{entityName}");
            LeafTreeNode deleteCommand = new LeafTreeNode($"Delete{entityName}Command");
            deleteCommand.TemplateValueHolder = new CommandTemplateValueHolder($"Delete{entityName}Command", entityName, $"Deleted{entityName}Dto", "Delete");

            LeafTreeNode deleteCommandValidator = new LeafTreeNode($"Delete{entityName}CommandValidator");
            deleteCommandValidator.TemplateValueHolder = new ValidatorTemplateValueHolder($"Delete{entityName}CommandValidator", $"Delete{entityName}Command");

            createFolder.Children.AddRange(new List<BaseTreeNode> { createCommand, createCommandValidator });
            updateFolder.Children.AddRange(new List<BaseTreeNode> { updateCommand, updateCommandValidator });
            deleteFolder.Children.AddRange(new List<BaseTreeNode> { deleteCommand, deleteCommandValidator });

            commandsFolder.Children.AddRange(new List<BaseTreeNode> { createFolder, updateFolder, deleteFolder });

            return commandsFolder;
        }

        private NonLeafTreeNode GetConstantsTreeNode(string entityName)
        {

            NonLeafTreeNode constantsFolder = new NonLeafTreeNode("Constants");
            LeafTreeNode constantsClass = new LeafTreeNode($"{entityName}Constants");
            constantsClass.TemplateValueHolder = new NormalClassTemplateValueHolder(constantsClass.Name);

            constantsFolder.Children.Add(constantsClass);

            return constantsFolder;
        }
        private NonLeafTreeNode GetDtosTreeNode(string entityName)
        {

            NonLeafTreeNode dtosFolder = new NonLeafTreeNode("Dtos");
            LeafTreeNode createdEntityDto = new LeafTreeNode($"Created{entityName}Dto");
            LeafTreeNode updatedEntityDto = new LeafTreeNode($"Updated{entityName}Dto");
            LeafTreeNode deletedEntityDto = new LeafTreeNode($"Deleted{entityName}Dto");
            LeafTreeNode entityGetByIdDto = new LeafTreeNode($"{entityName}GetByIdDto");
            LeafTreeNode entityListDto = new LeafTreeNode($"{entityName}ListDto");

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
                (c as LeafTreeNode).TemplateValueHolder = new DtoTemplateValueHolder(c.Name);
            });

            return dtosFolder;
        }
        private NonLeafTreeNode GetModelsTreeNode(string entityName)
        {
            NonLeafTreeNode modelsFolder = new NonLeafTreeNode("Models");
            LeafTreeNode entityListModel = new LeafTreeNode($"{entityName}ListModel");
            entityListModel.TemplateValueHolder = new PageableModelTemplateValueHolder(entityListModel.Name, entityName);

            modelsFolder.Children.Add(entityListModel);

            return modelsFolder;
        }
        private NonLeafTreeNode GetProfilesTreeNode(string entityName)
        {
            NonLeafTreeNode profilesFolder = new NonLeafTreeNode("Profiles");
            LeafTreeNode mappingProfiles = new LeafTreeNode($"MappingProfiles");
            mappingProfiles.TemplateValueHolder = new MapperTemplateValueHolder(entityName);

            profilesFolder.Children.Add(mappingProfiles);
            return profilesFolder;
        }
        private NonLeafTreeNode GetQueriesTreeNode(string entityName)
        {
            NonLeafTreeNode queriesFolder = new NonLeafTreeNode("Queries");

            NonLeafTreeNode getByIdEntityFolder = new NonLeafTreeNode($"GetById{entityName}");
            LeafTreeNode getByIdEntityQuery = new LeafTreeNode($"GetById{entityName}Query");
            getByIdEntityQuery.TemplateValueHolder = new GetByIdQueryTemplateValueHolder(getByIdEntityQuery.Name, entityName, $"{entityName}GetByIdDto");

            NonLeafTreeNode getListEntityFolder = new NonLeafTreeNode($"GetList{entityName}");
            LeafTreeNode getListEntityQuery = new LeafTreeNode($"GetList{entityName}Query");
            getListEntityQuery.TemplateValueHolder = new GetListQueryTemplateValueHolder(getListEntityQuery.Name, entityName, $"{entityName}ListModel");

            NonLeafTreeNode getListEntityByDynamicFolder = new NonLeafTreeNode($"GetList{entityName}ByDynamic");
            LeafTreeNode getListEntityByDynamicQuery = new LeafTreeNode($"GetList{entityName}ByDynamicQuery");
            getListEntityByDynamicQuery.TemplateValueHolder = new GetListByDynamicQueryTemplateValueHolder(getListEntityByDynamicQuery.Name, entityName, $"{entityName}ListModel");

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
        private NonLeafTreeNode GetRulesTreeNode(string entityName)
        {
            NonLeafTreeNode rulesFolder = new NonLeafTreeNode("Rules");
            LeafTreeNode entityBusinessRules = new LeafTreeNode($"{entityName}BusinessRules");
            entityBusinessRules.TemplateValueHolder = new NormalClassTemplateValueHolder(entityBusinessRules.Name);

            rulesFolder.Children.Add(entityBusinessRules);
            return rulesFolder;
        }


        public NonLeafTreeNode GetServicesTreeNode(string entityName)
        {

            NonLeafTreeNode servicesFolder = new NonLeafTreeNode("Services");
            NonLeafTreeNode repositoriesFolder = new NonLeafTreeNode("Repositories");

            LeafTreeNode iEntityRepository = new LeafTreeNode($"I{entityName}Repository");
            iEntityRepository.TemplateValueHolder = new IRepostioryTemplateValueHolder(iEntityRepository.Name, entityName);

            repositoriesFolder.Children.Add(iEntityRepository);
            servicesFolder.Children.Add(repositoriesFolder);

            return servicesFolder;
        }
        public BaseTreeNode GetTree(string entityName)
        {
            var applicationProjectName = ProjectHelper.ApplicationProjectName;

            NonLeafTreeNode applicationTree = new NonLeafTreeNode(applicationProjectName);
            NonLeafTreeNode featuresTree = GetFeaturesTreeNode(entityName);
            NonLeafTreeNode servicesTree = GetServicesTreeNode(entityName);

            applicationTree.Children.Add(featuresTree);
            applicationTree.Children.Add(servicesTree);

            return applicationTree;
        }
    }
}
