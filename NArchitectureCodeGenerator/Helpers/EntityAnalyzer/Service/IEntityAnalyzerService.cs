using NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers.EntityAnalyzer.Service
{
    public interface IEntityAnalyzerService
    {
        EntityInfo GetAnalyzedEntityInfo(string entityName);
        List<EntityInfo> GetAnalyzedEntityInfoList(List<string> entityNames);
    }
}
