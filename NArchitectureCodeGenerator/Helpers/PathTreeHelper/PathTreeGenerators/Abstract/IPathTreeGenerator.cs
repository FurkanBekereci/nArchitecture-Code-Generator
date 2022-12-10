using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract
{
    public interface IPathTreeGenerator
    {
        BaseTreeNode GetTree(EntityInfo entityInfo);
    }
}
