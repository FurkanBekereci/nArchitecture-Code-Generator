using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.RelationGenerator.Entities
{
    public class EntityRelationInfo
    {
        public EntityInfo EntityInfo { get; set; }
        public string EntityAppendLine { get; set; }
        public string EntityTypeConfigurationAppendLine { get; set; }
    }
}
