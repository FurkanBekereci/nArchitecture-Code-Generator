using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Service
{
    public interface ITemplateService
    {
        string CreateConvertedTemplateString(ITemplateBaseValueHolder templateBaseValueHolder);
    }
}
