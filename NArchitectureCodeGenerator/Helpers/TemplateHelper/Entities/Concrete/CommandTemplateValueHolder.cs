using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;
using NArchitectureCodeGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete
{
    public class CommandTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder ,ITemplateEntityNameHolder, ITemplateDtoNameHolder
    {
        public TemplateType TemplateType => TemplateType.Command;
        public string Name { get; set ; }
        public string EntityName { get; set; }
        public string EntityNamePlural => EntityName.Pluralize();
        public string EntityNameCamelCase => EntityName.FirstCharToLowerCase();
        public string DtoName { get;  set; }
        public string DtoNamePlural => DtoName.Pluralize();
        public string DtoNameCamelCase => DtoName.FirstCharToLowerCase();
        public string Namespace { get; set; }
        public string OperationType { get; set; }

        public CommandTemplateValueHolder()
        {

        }

        public CommandTemplateValueHolder(string name, string entityName, string dtoName, string operationType)
        {
            Name = name;
            EntityName = entityName;
            DtoName = dtoName;
            OperationType = operationType;
        }
    }
}
