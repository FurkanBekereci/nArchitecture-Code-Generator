using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Service
{
    public interface IServiceRegistrationService
    {
        void RegisterAllRequiredServices(string entityName);
    }
}
