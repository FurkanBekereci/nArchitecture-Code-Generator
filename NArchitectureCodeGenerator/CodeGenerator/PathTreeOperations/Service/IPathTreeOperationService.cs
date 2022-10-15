using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Service
{
    public interface IPathTreeOperationService
    {
        List<BaseTreeNode> GetTreeList(string entityName);
    }
}
