using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Register.Extensions
{
    public static class ServiceConfigConfigurationExtensions
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="Services"></param>
        /// <param name="configuration"></param>
        public static void AddServiceConfig(this IServiceCollection Services,IConfiguration configuration)
        {

            Services.Configure<ServiceRegistryConfig>(configuration.GetSection("ConsulRegistry"));
            Services.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();
        }
        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="services"></param>
        public static void AddDisConvery(this IServiceCollection services)
        {
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
        }

    }
}
