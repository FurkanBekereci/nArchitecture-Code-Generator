using NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Entities;
using NArchitectureCodeGenerator.Helpers.FileHelper.Service;
using NArchitectureCodeGenerator.RelationGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NArchitectureCodeGenerator.Helpers.FileAppenderHelper.Appenders.Abstract
{
    public abstract class BaseAppenderForRelationGenerator : BaseAppender
    {
        protected EntityRelationInfo _entityRelationInfo;
        protected BaseAppenderForRelationGenerator(IFileService fileService) : base(fileService)
        {
        }

        private void SetEntityRelationInfo(EntityRelationInfo entityRelationInfo) => _entityRelationInfo = entityRelationInfo;

        public void AppendLinesForRelationGenerator(EntityRelationInfo entityRelationInfo)
        {
            SetEntityRelationInfo(entityRelationInfo);

            AppendLines();
        }
    }
}
