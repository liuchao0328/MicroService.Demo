using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register
{
    /// <summary>
    /// 服务注册Dto
    /// </summary>
    public class ServiceRegistryConfig
    {
        // 服务ID
        public string Id { get; set; }

        // 服务名称
        public string Name { get; set; }

        // 服务标签(版本)
        public string[] Tags { set; get; }

        // 服务地址(可以选填 === 默认加载启动路径)
        public string Address { set; get; }

        // 服务端口号(可以选填 === 默认加载启动路径端口)
        public int Port { set; get; }

        // 服务注册地址
        public string RegistryAddress { get; set; }

        // 服务健康检查地址
        public string HealthCheckAddress { get; set; }
        public string LocalAddress { get; set; }
    }
}
