using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete
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
