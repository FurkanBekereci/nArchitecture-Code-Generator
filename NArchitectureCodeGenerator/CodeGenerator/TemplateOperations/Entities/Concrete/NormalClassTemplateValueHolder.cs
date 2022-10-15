using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete
{
    public class NormalClassTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder
    {
        public TemplateType TemplateType => TemplateType.NormalClass;
        public string Name { get; set ; }
        public string Namespace { get; set; }
        public NormalClassTemplateValueHolder()
        {

        }

        public NormalClassTemplateValueHolder(string name)
        {
            Name = name;
        }
    }
}
