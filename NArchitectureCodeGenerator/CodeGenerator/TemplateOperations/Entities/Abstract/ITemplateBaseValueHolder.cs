using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract
{
    public interface ITemplateBaseValueHolder
    {
       TemplateType TemplateType { get; }
    }
}
