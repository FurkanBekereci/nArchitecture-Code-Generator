
using Community.VisualStudio.Toolkit;
using Community.VisualStudio.Toolkit.DependencyInjection;
using Community.VisualStudio.Toolkit.DependencyInjection.Core;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.RelationGenerator;
using NArchitectureCodeGenerator.RelationGenerator.Service;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using Process = System.Diagnostics.Process;
using Task = System.Threading.Tasks.Task;

namespace NArchitectureCodeGenerator.Commands
{
    [Command(CommandString, CommandId)]
    internal sealed class RelationshipGeneratorCommand : BaseDICommand
    {

        public const int CommandId = 0x0100;
        public const string CommandString = "99cc1f36-24ca-4b66-bfcb-af9209707b0f";

        //public static readonly Guid CommandSet = new Guid("d7dda713-9385-4a96-94e6-4241e00ad46a");

        private static DIToolkitPackage _package;
        private static DTE2 _dte;
        private readonly IFileService _fileService;
        private readonly IRelationGeneratorService _relationGeneratorService;
        public RelationshipGeneratorCommand(DIToolkitPackage package, 
                                        IRelationGeneratorService relationGeneratorService,
                                        IFileService fileService) : base(package)
        {
            _relationGeneratorService = relationGeneratorService;
            _fileService = fileService;
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

            var entityName = selectedItem.GetEntityName();
            var selectedItemFilePath = selectedItem.GetFilePath();

            //Get solution(or root folder) path of selected item's project
            ProjectHelper.RootPath = _fileService.GetSolutionFolderPathOfEntity(selectedItemFilePath);
            ProjectHelper.FolderPathsOfProject = _fileService.FindFoldersOfGivenPath(ProjectHelper.RootPath);

            try
            {
                await _relationGeneratorService.StartRelationGenerationAsync(entityName, selectedItemFilePath);
                
                VsShellUtilities.ShowMessageBox(
                   _package,
                   "Process finished.",
                   "Done",
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
                   "Error!!",
                   OLEMSGICON.OLEMSGICON_WARNING,
                   OLEMSGBUTTON.OLEMSGBUTTON_OK,
                   OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST
                );
            }

        }
    }
}
