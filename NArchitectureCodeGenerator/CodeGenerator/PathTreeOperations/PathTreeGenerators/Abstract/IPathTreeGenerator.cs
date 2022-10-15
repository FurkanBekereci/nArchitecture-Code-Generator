using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract
{
    public interface IPathTreeGenerator
    {
        BaseTreeNode GetTree(string entityName);
    }
}
