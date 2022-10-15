using Community.VisualStudio.Toolkit;
using Community.VisualStudio.Toolkit.DependencyInjection.Microsoft;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Operations;
using NArchitectureCodeGenerator.CodeGenerator.FileOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.Service;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Service;
using NArchitectureCodeGenerator.Commands;
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
            services.AddSingleton<IFileOperationService, FileOperationManager>();
            services.AddSingleton<IPathTreeOperationService, PathTreeOperationManager>();
            services.AddSingleton<ICodeGeneratorService, CodeGeneratorManager>();
            services.AddSingleton<ITemplateService, TemplateManager>();
            services.AddSingleton<IServiceRegistrationService, ServiceRegistrationManager>();

            services.AddTransient<IPathTreeGenerator, ApplicationPathTreeGenerator>();
            services.AddTransient<IPathTreeGenerator, PersistencePathTreeGenerator>();
            services.AddTransient<IPathTreeGenerator, WebApiPathTreeGenerator>();

            services.AddTransient<BaseRegistrator, ApplicationServiceRegistrator>();
            services.AddTransient<BaseRegistrator, PersistenceServiceRegistrator>();
            services.AddTransient<BaseRegistrator, DbContextRegistrator>();

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
