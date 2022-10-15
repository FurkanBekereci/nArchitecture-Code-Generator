using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Abstract;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Entities.Concrete;
using NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.PathTreeGenerators.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.PathTreeOperations.Service
{
    public class PathTreeOperationManager : IPathTreeOperationService
    {

        private readonly IEnumerable<IPathTreeGenerator> _pathTreeGeneratorList;

        public PathTreeOperationManager(IEnumerable<IPathTreeGenerator> pathTreeGeneratorList)
        {
            _pathTreeGeneratorList = pathTreeGeneratorList;
        }

        public List<BaseTreeNode> GetTreeList(string entityName)
        {
            var children = _pathTreeGeneratorList.Select(tg => tg.GetTree(entityName)).ToList();
            return children;
            
        }
    }
}
