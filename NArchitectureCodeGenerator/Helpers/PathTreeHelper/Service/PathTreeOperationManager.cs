using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Concrete;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.Service
{
    public class PathTreeOperationManager : IPathTreeOperationService
    {

        private readonly IEnumerable<IPathTreeGenerator> _pathTreeGeneratorList;

        public PathTreeOperationManager(IEnumerable<IPathTreeGenerator> pathTreeGeneratorList)
        {
            _pathTreeGeneratorList = pathTreeGeneratorList;
        }

        public List<BaseTreeNode> GetTreeList(EntityInfo entityInfo)
        {
            var children = _pathTreeGeneratorList.Select(tg => tg.GetTree(entityInfo)).ToList();
            return children;
            
        }
    }
}
