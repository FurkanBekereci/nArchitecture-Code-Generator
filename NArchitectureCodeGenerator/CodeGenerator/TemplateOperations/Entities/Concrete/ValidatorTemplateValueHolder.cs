
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Concrete
{
    public class ValidatorTemplateValueHolder : ITemplateClassHolder, ITemplateNameHolder , ITemplateRequestNameHolder
    {
        public TemplateType TemplateType => TemplateType.Validator;
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string RequestName { get; set; }

        public ValidatorTemplateValueHolder()
        {

        }

        public ValidatorTemplateValueHolder(string name, string requestName)
        {
            Name = name;
            RequestName = requestName;
        }
    }
}
