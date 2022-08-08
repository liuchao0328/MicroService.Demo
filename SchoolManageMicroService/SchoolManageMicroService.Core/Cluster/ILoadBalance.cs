using SchoolManageMicroService.Core.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Cluster
{
    public interface ILoadBalance
    {
        ServiceUrl LoadBalance(List<ServiceUrl> urls);
    }
}
