using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities
{
    public class EntityPropertyInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsBuiltInType { get; set; }
        public bool IsArray { get; set; }

    }
}
