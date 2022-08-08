using SchoolManageMicroService.Core.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Cluster
{
    public class RandomLoadBalance : AbstractLoadBalance
    {
        private readonly Random Random=new Random();
        public override ServiceUrl DoLoadBalance(List<ServiceUrl> urls)
        {
            // 1、获取随机数
            var index = Random.Next(urls.Count);

            // 2、选择一个服务进行连接
            return urls[index];
        }
    }
}
