
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
    public class MapperTemplateValueHolder : ITemplateClassHolder, ITemplateEntityNameHolder
    {
        public TemplateType TemplateType => TemplateType.Mapper;
        public string EntityName { get; set; }
        public string EntityNamePlural => EntityName.Pluralize();
        public string EntityNameCamelCase => EntityName.FirstCharToLowerCase();
        public string Namespace { get; set; }

        public MapperTemplateValueHolder()
        {

        }

        public MapperTemplateValueHolder(string entityName)
        {
            EntityName = entityName;
        }
    }
}
