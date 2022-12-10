using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract
{
    public interface ITemplateEntityNameHolder
    {
        string EntityName { get; set; }
        string EntityNamePlural { get;  }
        string EntityNameCamelCase { get; }
    }
}
