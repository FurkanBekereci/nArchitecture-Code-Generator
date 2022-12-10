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
    public class EntityTypeConfigurationTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder, ITemplateEntityNameHolder, ITemplateDynamicContentHolder
    {
        public TemplateType TemplateType => TemplateType.EntityTypeConfiguration;
        public string Name { get; set ; }
        public string Namespace { get; set; }
        public string EntityName { get; set; }
        public string EntityNamePlural => EntityName.Pluralize();
        public string EntityNameCamelCase => EntityName.FirstCharToLowerCase();
        public string Content { get; set; }

        public EntityTypeConfigurationTemplateValueHolder()
        {

        }

        public EntityTypeConfigurationTemplateValueHolder(string name,string entityName, string content)
        {
            Name = name;
            EntityName = entityName;
            Content = content;
        }
    }
}
