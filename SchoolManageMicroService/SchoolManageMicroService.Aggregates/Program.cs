using NLog.Web;
using Polly;
using SchoolManageMicroService.Aggregates.Services;
using SchoolManageMicroService.Core.ConfigCenter;
using SchoolManageMicroService.Core.HttpClientConsul;
using SchoolManageMicroService.Core.Mock;
using SchoolManageMicroService.Core.Register.Extensions;

var builder = WebApplication.CreateBuilder(args);
//加载配置中心文件
builder.Host.ConfigureAppConfiguration((ctx, config) =>
{
    IConfigCenter configCenter = new ConsulConfigCenter();
    configCenter.ConfigurationConfigCenter(ctx, config);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//（加载注册服务需要的配置文件）
builder.Services.AddServiceConfig(builder.Configuration);
var fallbackResponse = new HttpResponseMessage()
{
    Content = new StringContent("服务器繁忙，请稍后再试"),
    StatusCode = System.Net.HttpStatusCode.BadRequest
};
builder.Services.AddHttpClient("ClassesService") // 请求连接复用
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<ExecutionRejectedException>() // 捕获所有的Polly异常
                .FallbackAsync(fallbackResponse))  //返回指定内容
                .AddPolicyHandler(
                Policy<HttpResponseMessage>
                .Handle<Exception>().
                CircuitBreakerAsync(5, TimeSpan.FromSeconds(10))) // 断路器，错误五次断路，断路过期时间十秒钟 
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(60)) // 超时
                .AddPolicyHandler(Policy<HttpResponseMessage>
              .Handle<Exception>()
              .RetryAsync(2))  //重试机制  重试两次
                .AddPolicyHandler(Policy.BulkheadAsync<HttpResponseMessage>(10, 100));//服务发现
builder.Services.AddDisConvery();
builder.Services.AddHttpClientConsul<ConsulHttpClient>();
builder.Services.AddSingleton<IClassesClientService, ClassesHttpClientService>();
builder.Services.AddSingleton<IMock, ExceptionMock>();//注册服务降级服务
builder.Services.AddCap(opt =>
{
    opt.UseRabbitMQ(options =>
    {
        options.HostName = "182.92.120.29";//
        options.UserName = "guest";
        options.Password = "guest";
        options.Port = 5672;
        options.VirtualHost = "/";
    });
    opt.UseInMemoryStorage();//使用缓存作本地消息表
});
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{

}).ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseConsulRegistry();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
