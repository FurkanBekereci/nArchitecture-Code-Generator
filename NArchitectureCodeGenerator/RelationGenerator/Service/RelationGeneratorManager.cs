using NArchitectureCodeGenerator.CodeGenerator.Service;
using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Service;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete;
using NArchitectureCodeGenerator.RelationGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NArchitectureCodeGenerator.RelationGenerator.Service
{
    public class RelationGeneratorManager : IRelationGeneratorService
    {
        private readonly IFileService _fileService;
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IEntityAnalyzerService _entityAnalyzerService;
        private readonly IFileAppenderService _fileAppenderService;
        
        public RelationGeneratorManager(IFileService fileService, ICodeGeneratorService codeGeneratorService, IEntityAnalyzerService entityAnalyzerService, IFileAppenderService fileAppenderService)
        {
            _fileService = fileService;
            _codeGeneratorService = codeGeneratorService;
            _entityAnalyzerService = entityAnalyzerService;
            _fileAppenderService = fileAppenderService;
        }

        public async Task StartRelationGenerationAsync(string selectedEntityName, string selectedEntityPath)
        {
            var selectedEntityInfo = _entityAnalyzerService.GetAnalyzedEntityInfo(selectedEntityName);

            var directoryOfSelectedItemPath = _fileService.GetDirectoryPathOfFile(selectedEntityPath);
            var nameListOfEntities = _fileService.GetNameListOfEntities(directoryOfSelectedItemPath);

            var allEntityInfos = _entityAnalyzerService.GetAnalyzedEntityInfoList(nameListOfEntities);

            //We need to send list of unselected entities. So need to remove selectedEntity from allEntityInfos;
            var otherEntityInfos = allEntityInfos.FindAll(ei => ei.Name != selectedEntityName);

            if (otherEntityInfos.Count == 0) throw new Exception("There is no different entity to create a relation!");

            RelationWindow relationWindow = new RelationWindow(selectedEntityInfo, otherEntityInfos)
            {
                Owner = Application.Current.MainWindow
            };

            RelationWindowDialogResult dialogResult = null;
            relationWindow.Closing += delegate
            {
                dialogResult = relationWindow.GetDialogResult();
            };

            await relationWindow.ShowDialogAsync(WindowStartupLocation.CenterScreen);

            if (dialogResult == null) return;

            if (dialogResult.IsManyToMany) 
            {
                _codeGeneratorService.GenerateNewEntity(dialogResult.ManyToManyRelationEntityName, dialogResult.ManyToManyRelationEntityContent);
                var realEntityInfo = _entityAnalyzerService.GetAnalyzedEntityInfo(dialogResult.ManyToManyRelationEntityName);
                _codeGeneratorService.GenerateAllDependentCodesForEntity(realEntityInfo, ProjectHelper.RootPath);
            }

            _fileAppenderService.AppendLinesForRelationGenerator(dialogResult.SelectedEntityRelationInfo);
            _fileAppenderService.AppendLinesForRelationGenerator(dialogResult.OtherEntityRelationInfo);
        }
    }
}
