using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.TemplateOperations.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete
{
    public class LeafTreeNode : BaseTreeNode
    {
        public ITemplateClassHolder TemplateValueHolder { get; set; }

        public LeafTreeNode() : base()
        {

        }

        public LeafTreeNode(string name) : base(name, true)
        {

        }
    }
}
