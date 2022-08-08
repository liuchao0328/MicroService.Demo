using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register
{
    /// <summary>
    /// 服务发现Dto
    /// </summary>
    public class ServiceDiscoveryConfig
    {
        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string RegistryAddress { get; set; }
    }
}
