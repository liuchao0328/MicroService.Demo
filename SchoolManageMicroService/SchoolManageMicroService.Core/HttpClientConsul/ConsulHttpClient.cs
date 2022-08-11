using Newtonsoft.Json;
using SchoolManageMicroService.Core.Cluster;
using SchoolManageMicroService.Core.Mock;
using SchoolManageMicroService.Core.Register;
using System.Net;


namespace SchoolManageMicroService.Core.HttpClientConsul
{
    public class ConsulHttpClient
    {
        private readonly ILoadBalance loadBalance;
        private readonly IServiceDiscovery serviceDiscovery;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMock mock;
        public ConsulHttpClient(ILoadBalance loadBalance, IServiceDiscovery serviceDiscovery, IHttpClientFactory httpClientFactory, IMock mock)
        {
            this.loadBalance = loadBalance;
            this.serviceDiscovery = serviceDiscovery;
            this.httpClientFactory = httpClientFactory;
            this.mock = mock;
        }
        public async Task<T> GetAsync<T>(string Serviceshcme, string ServiceName, string serviceLink)
        {
            //进行服务降级
            mock.DoMock(ServiceName);
            // 故障转移
            string json = "";
            int RestyConut = 0;
            for (int i = 0; i <= 3; i++)
            {
                // 1、是否达到阀值
                if (RestyConut == 3)
                {
                    throw new Exception($"微服务重试操作阀值");
                }
                // 1、获取服务
                List<ServiceUrl> serviceUrls = await serviceDiscovery.Discovery(ServiceName);
                // 2、负载均衡服务
                ServiceUrl serviceUrl = loadBalance.LoadBalance(serviceUrls);
                try
                {
                    // 3、建立请求
                    HttpClient httpClient = httpClientFactory.CreateClient(ServiceName);
                    HttpResponseMessage response = await httpClient.GetAsync(serviceUrl.Url + serviceLink);
                    // 3.1、json转换成对象
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        json = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception($"{ServiceName}服务调用错误，异常{await response.Content.ReadAsStringAsync()}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"异常{e.Message}");
                    // 存储到集合
                    ++RestyConut;
                    Console.WriteLine($"调用微服务{ServiceName}出现故障，开始故障转移{RestyConut}");
                }
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
