using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Winton.Extensions.Configuration.Consul;

namespace SchoolManageMicroService.Core.ConfigCenter
{
    public class ConsulConfigCenter : IConfigCenter
    {
        public void ConfigurationConfigCenter(HostBuilderContext cxt, IConfigurationBuilder config)
        {
            var figuration = config.AddJsonFile("consulcenter.json").Build();
            var address = figuration["ConfigCenterAddress"];
            IHostEnvironment hostEnvironment = cxt.HostingEnvironment;
            var environmentName = hostEnvironment.EnvironmentName;
            //var assmbly = Assembly.GetExecutingAssembly().GetName().Name;  //不能这么获取，这么只能获取当前运行的程序集名称这里获取的是 SchoolManageService.Core 
            var applicationName=cxt.HostingEnvironment.ApplicationName;
            config.AddConsul($"{applicationName}/appsettings.{environmentName}.json", configuration =>
            {
                configuration.ConsulConfigurationOptions = opt =>
                {
                    opt.Address = new Uri(address);
                };
                configuration.ReloadOnChange = true;//热加载配置文件
            });
        }
    }
}
