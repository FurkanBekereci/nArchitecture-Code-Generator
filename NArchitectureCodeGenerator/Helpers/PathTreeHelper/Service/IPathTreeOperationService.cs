using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.Entities.Abstract;
using NArchitectureCodeGenerator.Helpers.PathTreeHelper.PathTreeGenerators.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.PathTreeHelper.Service
{
    public interface IPathTreeOperationService
    {
        List<BaseTreeNode> GetTreeList(EntityInfo entityInfo);
    }
}
