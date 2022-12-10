using NArchitectureCodeGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities
{
    public class EntityInfo
    {
        public string Name { get; set; }
        public List<EntityPropertyInfo> PropertyInfoList { get; set; }
        public List<string> Lines { get; set; }
        public string Content { get; set; }
        public string DtoContent { get; set; }
        public string EntityTypeConfigurationContent { get; set; }
        public string FilePath { get; set; }
        public string Namespace { get; set; }
        public List<string> EntityTypeModelBuilderLines { get; set; }
        public List<string> DtoLines { get; set; }
    }
}
