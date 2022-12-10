using Microsoft.VisualStudio.Text.Operations;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;
using NArchitectureCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Service
{
    public class TemplateManager : ITemplateService
    {

        private readonly IFileService _fileService;

        public TemplateManager(IFileService fileService)
        {
            _fileService = fileService;
        }

        public string CreateConvertedTemplateString(ITemplateBaseValueHolder templateValueHolder)
        {
            var templateFileName = GetTemplateFileName(templateValueHolder.TemplateType);
            var templateFileString = _fileService.ReadTemplateFile(templateFileName);
            var convertedString = GetConvertedStringFromTemplateValueHolder(templateValueHolder, templateFileString);
            return convertedString;
        }

        public EntityTemplateValueHolder GetEntityTemplateValueHolder(string name, string @namespace, string content) => new EntityTemplateValueHolder(name, @namespace, content);

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
