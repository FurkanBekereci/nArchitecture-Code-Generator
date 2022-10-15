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
    public class EntityTypeConfigurationTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder, ITemplateEntityNameHolder
    {
        public TemplateType TemplateType => TemplateType.EntityTypeConfiguration;
        public string Name { get; set ; }
        public string Namespace { get; set; }
        public string EntityName { get; set; }
        public string EntityNamePlural => EntityName.Pluralize();
        public string EntityNameCamelCase => EntityName.FirstCharToLowerCase();

        public EntityTypeConfigurationTemplateValueHolder()
        {

        }

        public EntityTypeConfigurationTemplateValueHolder(string name,string entityName)
        {
            Name = name;
            EntityName = entityName;
        }
    }
}
