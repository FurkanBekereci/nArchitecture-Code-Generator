using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete
{
    public class DtoTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder, ITemplateDynamicContentHolder
    {
        public TemplateType TemplateType => TemplateType.Dto;
        public string Name { get; set ; }
        public string Namespace { get; set; }
        public string Content { get; set; }

        public DtoTemplateValueHolder()
        {

        }
        public DtoTemplateValueHolder(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
