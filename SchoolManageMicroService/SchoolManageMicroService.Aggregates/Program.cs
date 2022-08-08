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
    Content = new StringContent("��������æ�����Ժ�����"),
    StatusCode = System.Net.HttpStatusCode.BadRequest
};
builder.Services.AddHttpClient("ClassesService") // �������Ӹ���
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<ExecutionRejectedException>() // �������е�Polly�쳣
                .FallbackAsync(fallbackResponse))  //����ָ������
                .AddPolicyHandler(
                Policy<HttpResponseMessage>
                .Handle<Exception>().
                CircuitBreakerAsync(5, TimeSpan.FromSeconds(10))) // ��·����������ζ�·����·����ʱ��ʮ���� 
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(60)) // ��ʱ
                .AddPolicyHandler(Policy<HttpResponseMessage>
              .Handle<Exception>()
              .RetryAsync(2))  //���Ի���  ��������
                .AddPolicyHandler(Policy.BulkheadAsync<HttpResponseMessage>(10, 100));//������
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
