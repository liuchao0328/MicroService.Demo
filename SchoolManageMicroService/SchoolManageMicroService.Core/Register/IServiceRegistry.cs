using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register
{
    public interface IServiceRegistry
    {
        public void Registry(ServiceRegistryConfig config);
        public void DeRegistry(ServiceRegistryConfig config);
    }
}
