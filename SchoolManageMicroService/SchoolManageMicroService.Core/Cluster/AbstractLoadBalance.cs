using SchoolManageMicroService.Core.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Cluster
{
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        public ServiceUrl LoadBalance(List<ServiceUrl> urls)
        {
            if (urls.Count==1)
            {
                return urls[0];
            }
            return DoLoadBalance(urls);
        }
        public abstract ServiceUrl DoLoadBalance(List<ServiceUrl> urls);
    }
}
