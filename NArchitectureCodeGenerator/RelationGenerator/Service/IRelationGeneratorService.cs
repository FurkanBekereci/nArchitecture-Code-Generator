using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.RelationGenerator.Service
{
    public interface IRelationGeneratorService
    {
        Task StartRelationGenerationAsync(string selectedEntityName, string selectedEntityPath);
    }
}
