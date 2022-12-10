using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Service
{
    public interface ITemplateService
    {
        string CreateConvertedTemplateString(ITemplateBaseValueHolder templateBaseValueHolder);

        EntityTemplateValueHolder GetEntityTemplateValueHolder(string name, string @namespace, string content);
    }
}
