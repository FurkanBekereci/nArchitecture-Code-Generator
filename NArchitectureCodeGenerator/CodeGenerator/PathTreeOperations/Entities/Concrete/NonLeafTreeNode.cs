using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete
{
    public class NonLeafTreeNode : BaseTreeNode
    {
        public NonLeafTreeNode()
        {
            Children = new List<BaseTreeNode>();
        }

        public NonLeafTreeNode(string name) : base(name, false)
        {
            Children = new List<BaseTreeNode>();
        }
        public NonLeafTreeNode(string name, List<BaseTreeNode> children) : base(name, false)
        {
            Children = children;
        }
    }
}
