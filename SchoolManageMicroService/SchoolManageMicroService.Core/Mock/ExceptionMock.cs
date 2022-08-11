using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManageMicroService.Core.Mock
{
    /// <summary>
    /// 异常服务降级类
    /// </summary>
    public class ExceptionMock : IMock
    {
        private readonly IConfiguration configuration;

        public ExceptionMock(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void DoMock(string SeriveName)
        {
            var flag = Convert.ToBoolean(configuration[SeriveName]);
            if (flag)
            {
                throw new Exception($"对{SeriveName}进行降级");
            }
        }
    }
}
