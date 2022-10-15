using NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Registrators.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.CodeGenerator.ServiceRegistrationOperations.Service
{
    public class ServiceRegistrationManager : IServiceRegistrationService
    {
        private readonly IEnumerable<BaseRegistrator> _registratorList;

        public ServiceRegistrationManager(IEnumerable<BaseRegistrator> registratorList)
        {
            _registratorList = registratorList;
        }

        public void RegisterAllRequiredServices(string entityName)
        {
            foreach (var registrator in _registratorList)
            {
                registrator.Register(entityName);
            }
        }
    }
}
