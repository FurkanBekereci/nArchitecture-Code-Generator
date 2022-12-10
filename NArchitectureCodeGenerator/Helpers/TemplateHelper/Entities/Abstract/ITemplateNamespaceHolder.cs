using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract
{
    public interface ITemplateNamespaceHolder
    {
        string Namespace { get; set; }
    }
}
