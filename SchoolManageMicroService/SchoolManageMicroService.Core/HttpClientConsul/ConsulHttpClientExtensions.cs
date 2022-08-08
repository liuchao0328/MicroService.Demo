using Microsoft.Extensions.DependencyInjection;
using SchoolManageMicroService.Core.Cluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.HttpClientConsul
{
    public static class ConsulHttpClientExtensions
    {
        public static IServiceCollection AddHttpClientConsul<ConsulHttpClient>(this IServiceCollection services) where ConsulHttpClient : class
        {
            // 1、注册consul
            // 2、注册服务负载均衡
            services.AddSingleton<ILoadBalance, RandomLoadBalance>();
            // 3、注册httpclient
            services.AddSingleton<ConsulHttpClient>();

            return services;
        }
    }
}
