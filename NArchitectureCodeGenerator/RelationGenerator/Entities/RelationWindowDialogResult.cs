using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.RelationGenerator.Entities
{
    public class RelationWindowDialogResult
    {
        public EntityRelationInfo SelectedEntityRelationInfo { get; set; }
        public EntityRelationInfo OtherEntityRelationInfo { get; set; }
        public bool IsManyToMany { get; set; }
        public string ManyToManyRelationEntityName { get; set; }
        public string ManyToManyRelationEntityContent { get; set; }
    }
}
