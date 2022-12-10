using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Entities;
using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Abstract;
using NArchitectureCodeGenerator.Extensions;
using NArchitectureCodeGenerator.Helpers;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitectureCodeGenerator.RelationGenerator.Entities;

namespace NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Concrete
{
    public class EntityAppender : BaseAppenderForRelationGenerator
    {
        
        public EntityAppender(IFileService fileService) : base(fileService)
        {
        }

        protected override void ControlUsingStatements(ref List<string> lines)
        {
            
        }

        protected override string GetFilePath()
        {
            return _fileService.ExtractFilePathOfEntity(_entityRelationInfo.EntityInfo.Name);
        }

        protected override string GetLineContentToAdd()
        {
            return _entityRelationInfo.EntityAppendLine;
        }

        protected override List<LineNumberDecider> GetLineNumberDeciderList()
        {

            return new List<LineNumberDecider>
            {
                new LineNumberDecider(".*{.*get.*set.*}.*", 1),
                new LineNumberDecider($".*{_entityRelationInfo.EntityInfo.Name}.*:.*Entity.*", 0, true)
            };

        }
    }
}
