using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.TemplateHelper.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete
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
