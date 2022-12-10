using NArchitectureCodeGenerator.Helpers.TemplateHelper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract
{
    public interface ITemplateBaseValueHolder
    {
       TemplateType TemplateType { get; }
    }
}
