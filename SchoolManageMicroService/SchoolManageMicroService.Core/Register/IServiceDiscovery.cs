using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register
{
    public interface IServiceDiscovery
    {
        public Task<List<ServiceUrl>> Discovery(string serviceName);
    }
}
