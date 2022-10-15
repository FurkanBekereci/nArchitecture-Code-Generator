﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract
{
    public interface ITemplateNameHolder
    {
        string Name { get; set; }
        //string NamePlural { get; set; }
        //string NameCamelCase { get; set; }
    }
}
