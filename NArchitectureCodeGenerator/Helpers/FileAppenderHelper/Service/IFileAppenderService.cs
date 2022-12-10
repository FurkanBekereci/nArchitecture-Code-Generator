using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.RelationGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Service
{
    public interface IFileAppenderService
    {
        void AppendLinesForCodeGenerator(EntityInfo entityInfo);
        void AppendLinesForRelationGenerator(EntityRelationInfo entityRelationInfo);
    }
}
