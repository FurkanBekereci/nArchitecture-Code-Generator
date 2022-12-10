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
    public class EntityTypeConfigurationAppender : BaseAppenderForRelationGenerator
    {
        
        public EntityTypeConfigurationAppender(IFileService fileService) : base(fileService)
        {
        }

        protected override string GetFilePath()
        {
            return _fileService.ExtractEntityTypeConfigurationFilePathOfEntity(_entityRelationInfo.EntityInfo.Name);
        }

        protected override string GetLineContentToAdd()
        {
            return _entityRelationInfo.EntityTypeConfigurationAppendLine;
        }

        protected override List<LineNumberDecider> GetLineNumberDeciderList()
        {

            return new List<LineNumberDecider>
            {
                new LineNumberDecider(".*builder.*\\(.*p.*=>.*p.*\\).*", 1),
                new LineNumberDecider(".*public.*void.*Configure.*\\(.*EntityTypeBuilder<.*>.*builder.*\\).*", 0,true)
            };

        }


    }
}
