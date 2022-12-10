using Community.VisualStudio.Toolkit;
using Community.VisualStudio.Toolkit.DependencyInjection;
using Community.VisualStudio.Toolkit.DependencyInjection.Core;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NArchitectureCodeGenerator.CodeGenerator.Service;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using System;
using Task = System.Threading.Tasks.Task;

namespace NArchitectureCodeGenerator.Commands
{
    [Command(CommandString, CommandId)]
    internal sealed class CodeGeneratorCommand : BaseDICommand
    {

        public const int CommandId = 0x0100;
        public const string CommandString = "d7dda713-9385-4a96-94e6-4241e00ad46a";

        //public static readonly Guid CommandSet = new Guid("d7dda713-9385-4a96-94e6-4241e00ad46a");

        private static DIToolkitPackage _package;
        private static DTE2 _dte;
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IFileService _fileService;
        private readonly IEntityAnalyzerService _entityAnalyzerService;
        
        public CodeGeneratorCommand(DIToolkitPackage package, 
                                ICodeGeneratorService codeGeneratorService,
                                IFileService fileService,
                                IEntityAnalyzerService entityAnalyzerService) : base(package)
        {
            _codeGeneratorService = codeGeneratorService;
            _fileService = fileService;
            _entityAnalyzerService = entityAnalyzerService;
            SetProperties(package);
        }

        public async void SetProperties(DIToolkitPackage package)
        {
            _package = package;
            _dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
        }

        protected async override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            await ProgressDialogHelper.ShowDialogAsync(StartCodeGeneration);

        }

        private void StartCodeGeneration()
        {
            // Right click
            SelectedItem selectedItem = _dte.SelectedItems.Item(1);

            //Controls right-clicked item whether in folder named Domain.Entities.
            //So this class will be limited with being entity.
            //If not, then shows a message box
            if (!selectedItem.IsEntity())
            {
                VsShellUtilities.ShowMessageBox(
                   _package,
                   "Selected class is not an entity. Please select an entity from \"Domain\\Enities\" location.",
                   "Caution",
                   OLEMSGICON.OLEMSGICON_INFO,
                   OLEMSGBUTTON.OLEMSGBUTTON_OK,
                   OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST
                );
                return;
            }

            //Removing extension from selected item name, for example Brand.cs => Brand 
            var entityName = selectedItem.GetEntityName();
            var selectedItemFilePath = selectedItem.GetFilePath();

            //Get solution(or root folder) path of selected item's project
            ProjectHelper.RootPath = _fileService.GetSolutionFolderPathOfEntity(selectedItemFilePath);
            ProjectHelper.FolderPathsOfProject = _fileService.FindFoldersOfGivenPath(ProjectHelper.RootPath);
            ProjectHelper.SelectedEntityInfo = _entityAnalyzerService.GetAnalyzedEntityInfo(entityName);

            //Beklenmedik hata ile karşılaşılması durumunda kontrol amaçlı
            try
            {
                _codeGeneratorService.GenerateAllDependentCodesForEntity(ProjectHelper.SelectedEntityInfo, ProjectHelper.RootPath);

                VsShellUtilities.ShowMessageBox(
                  _package,
                  "Process finished!!",
                  "Done!!",
                  OLEMSGICON.OLEMSGICON_INFO,
                  OLEMSGBUTTON.OLEMSGBUTTON_OK,
                  OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST
               );
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(
                  _package,
                  ex.Message,
                  "Done!!",
                  OLEMSGICON.OLEMSGICON_WARNING,
                  OLEMSGBUTTON.OLEMSGBUTTON_OK,
                  OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST
               );
            }
        }
    }
}
