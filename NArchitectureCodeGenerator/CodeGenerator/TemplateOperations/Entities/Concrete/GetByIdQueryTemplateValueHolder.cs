using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using NArchitectureCodeGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete
{
    public class GetByIdQueryTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder, ITemplateEntityNameHolder, ITemplateDtoNameHolder
    {
        public TemplateType TemplateType => TemplateType.GetByIdQuery;
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string EntityName { get; set; }
        public string EntityNamePlural => EntityName.Pluralize();
        public string EntityNameCamelCase => EntityName.FirstCharToLowerCase();
        public string DtoName { get; set; }
        public string DtoNamePlural => DtoName.Pluralize();
        public string DtoNameCamelCase => DtoName.FirstCharToLowerCase();

        public GetByIdQueryTemplateValueHolder()
        {

        }

        public GetByIdQueryTemplateValueHolder(string name, string entityName, string dtoName)
        {
            Name = name;
            EntityName = entityName;
            DtoName = dtoName;
        }
    }
}
