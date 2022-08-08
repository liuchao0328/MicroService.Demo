using Polly;
using SchoolManageMicroService.Aggregates.Services;
using SchoolManageMicroService.Core.HttpClientConsul;
using SchoolManageMicroService.Core.Register.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
