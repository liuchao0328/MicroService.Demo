using Newtonsoft.Json;
using SchoolManageMicroService.Aggregates.Models;
using SchoolManageMicroService.Core.HttpClientConsul;

namespace SchoolManageMicroService.Aggregates.Services
{
    public class ClassesHttpClientService : IClassesClientService
    {
        private readonly ConsulHttpClient consulHttpClient;
        private readonly string ServiceSchme = "https";
        private readonly string ServiceName = "ClassesService"; // 服务名称
        private readonly string ServiceLink = "/api/Classes";// 团队链接
        public ClassesHttpClientService(ConsulHttpClient consulHttpClient)
        {
            this.consulHttpClient = consulHttpClient;
        }
        public async Task<List<Class>> GetClasses()
        {

            List<Class> classes = await
             consulHttpClient.GetAsync<List<Class>>(ServiceSchme, ServiceName, ServiceLink);
            return classes;
        }
    }
}
