using Microsoft.VisualStudio.Text.Operations;
using NArchitectureCodeGenerator.CodeGenerator.FileOperations.Service;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Service
{
    public class TemplateManager : ITemplateService
    {

        private readonly IFileOperationService _fileOperationService;

        public TemplateManager(IFileOperationService fileOperationService)
        {
            _fileOperationService = fileOperationService;
        }

        public string CreateConvertedTemplateString(ITemplateBaseValueHolder templateValueHolder)
        {
            var templateFileName = GetTemplateFileName(templateValueHolder.TemplateType);
            var templateFileString = _fileOperationService.ReadTemplateFile(templateFileName);
            var convertedString = GetConvertedStringFromTemplateValueHolder(templateValueHolder, templateFileString);
            return convertedString;
        }

        private string GetConvertedStringFromTemplateValueHolder(ITemplateBaseValueHolder templateValueHolder, string templateString)
        {

            //Get converted string by reflection
            return ReflectionHelper.GetConvertedTemplateStringByProperties(templateValueHolder, templateString)
                                    .Replace("{applicationProjectName}", ProjectHelper.ApplicationProjectName)
                                    .Replace("{domainProjectName}", ProjectHelper.DomainProjectName)
                                    .Replace("{infrastructureProjectName}", ProjectHelper.InfrastructureProjectName)
                                    .Replace("{persistenceProjectName}", ProjectHelper.PersistenceProjectName)
                                    .Replace("{webApiProjectName}", ProjectHelper.WebApiProjectName);


        }

        private string GetTemplateFileName(TemplateType templateType)
        {
            return $"{templateType}.txt";
        }
    }
}
