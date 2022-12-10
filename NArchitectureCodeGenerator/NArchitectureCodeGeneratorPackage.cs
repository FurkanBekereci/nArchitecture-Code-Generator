using Community.VisualStudio.Toolkit;
using Community.VisualStudio.Toolkit.DependencyInjection.Microsoft;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Shell;
using NArchitectureCodeGenerator.CodeGenerator.Service;
using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Abstract;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Concrete;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Service;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Concrete;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Service;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Service;
using NArchitectureCodeGenerator.RelationGenerator.Service;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace NArchitectureCodeGenerator
{

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class NArchitectureCodeGeneratorPackage : MicrosoftDIToolkitPackage<NArchitectureCodeGeneratorPackage>
    {
        public const string PackageGuidString = "d20044c4-8101-4adf-be40-b3a57c247c96";

        protected override void InitializeServices(IServiceCollection services)
        {

            base.InitializeServices(services);

            // Registering services here
            services.AddSingleton<IFileService, FileManager>();
            services.AddSingleton<IPathTreeOperationService, PathTreeOperationManager>();
            services.AddSingleton<ICodeGeneratorService, CodeGeneratorManager>();
            services.AddSingleton<ITemplateService, TemplateManager>();
            services.AddSingleton<IFileAppenderService, FileAppenderManager>();
            services.AddSingleton<IEntityAnalyzerService, EntityAnalyzerManager>();
            services.AddSingleton<IRelationGeneratorService, RelationGeneratorManager>();

            services.AddTransient<IPathTreeGenerator, ApplicationPathTreeGenerator>();
            services.AddTransient<IPathTreeGenerator, PersistencePathTreeGenerator>();
            services.AddTransient<IPathTreeGenerator, WebApiPathTreeGenerator>();

            services.AddTransient<BaseAppenderForCodeGenerator, ApplicationServiceRegistrationAppender>();
            services.AddTransient<BaseAppenderForCodeGenerator, PersistenceServiceRegistrationAppender>();
            services.AddTransient<BaseAppenderForCodeGenerator, DbContextAppender>();

            services.AddTransient<BaseAppenderForRelationGenerator, EntityAppender>();
            services.AddTransient<BaseAppenderForRelationGenerator, EntityTypeConfigurationAppender>();

            //// Registers command from assembly
            services.RegisterCommands(ServiceLifetime.Singleton);

            //Or we can register command separately
            //services.AddSingleton<CodeGeneratorCommand>();

        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);
        }
    }
}
