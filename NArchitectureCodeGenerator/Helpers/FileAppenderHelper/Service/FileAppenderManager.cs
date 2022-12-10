using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Abstract;
using NArchitectureCodeGenerator.RelationGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Service
{
    public class FileAppenderManager : IFileAppenderService
    {
        private readonly IEnumerable<BaseAppenderForCodeGenerator> _appenderListForCodeGenerator;
        private readonly IEnumerable<BaseAppenderForRelationGenerator> _appenderListForRelationGenerator;

        public FileAppenderManager(
            IEnumerable<BaseAppenderForCodeGenerator> appenderListForCodeGenerator, 
            IEnumerable<BaseAppenderForRelationGenerator> appenderListForRelationGenerator
            )
        {
            _appenderListForCodeGenerator = appenderListForCodeGenerator;
            _appenderListForRelationGenerator = appenderListForRelationGenerator;
        }

        public void AppendLinesForCodeGenerator(EntityInfo entityInfo)
        {
            foreach (var appender in _appenderListForCodeGenerator)
            {
                appender.AppendLinesForCodeGenerator(entityInfo);
            }
        }

        public void AppendLinesForRelationGenerator(EntityRelationInfo entityRelationInfo)
        {
            foreach (var appender in _appenderListForRelationGenerator)
            {
                appender.AppendLinesForRelationGenerator(entityRelationInfo);
            }
        }
    }
}
