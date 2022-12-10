using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract
{
    public abstract class BaseTreeNode
    {
        public string Name { get; set; }
        public bool IsLeaf { get; set; }
        public List<BaseTreeNode> Children { get; set; }

        public BaseTreeNode()
        {

        }

        public BaseTreeNode(string name, bool isLeaf)
        {
            Name = name;
            IsLeaf = isLeaf;
        }

        public BaseTreeNode(string name, bool isLeaf, List<BaseTreeNode> children)
        {
            Name = name;
            IsLeaf = isLeaf;
            Children = children;
        }
    }
}
