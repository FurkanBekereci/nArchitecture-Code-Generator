using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Enums
{
    public enum TemplateType
    {
        NormalClass,
        StaticClass,
        Command,
        GetByIdQuery,
        GetListQuery,
        GetListByDynamicQuery,
        Validator,
        PageableModel,
        IRepository,
        Repository,
        Mapper,
        Controller,
        EntityTypeConfiguration,
        Dto
    }
}
