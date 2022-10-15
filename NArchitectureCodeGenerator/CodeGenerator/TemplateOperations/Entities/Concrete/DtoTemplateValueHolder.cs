using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete
{
    public class DtoTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder
    {
        public TemplateType TemplateType => TemplateType.Dto;
        public string Name { get; set ; }
        public string Namespace { get; set; }

        public DtoTemplateValueHolder()
        {

        }
        public DtoTemplateValueHolder(string name)
        {
            Name = name;
        }
    }
}
