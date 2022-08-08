using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register
{
    /// <summary>
    /// Consul注册服务
    /// </summary>
    public class ConsulServiceRegistry : IServiceRegistry
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="config"></param>
        public void DeRegistry(ServiceRegistryConfig config)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(config.RegistryAddress);
            });

            // 2、注销服务
            consulClient.Agent.ServiceDeregister(config.Id);

            // 3、关闭连接
            consulClient.Dispose();
        }
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="config"></param>
        public void Registry(ServiceRegistryConfig config)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(config.RegistryAddress);
            });

            // 2、获取服务内部地址

            // 3、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = config.Id,
                Name = config.Name,
                Address = config.Address,
                Port = config.Port,
                Tags = config.Tags,
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = config.HealthCheckAddress,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };
            // 4、注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();
            // 5、关闭连接
            consulClient.Dispose();
        }
    }
}
