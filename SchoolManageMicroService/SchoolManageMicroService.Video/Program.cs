using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Core.ConfigCenter;
using SchoolManageMicroService.Core.Register.Extensions;
using SchoolManageMicroService.Videos.Contexts;
using SchoolManageMicroService.Videos.Repositories;
using SchoolManageMicroService.Videos.Services;

var builder = WebApplication.CreateBuilder(args);
//�����������������ļ�
builder.Host.ConfigureAppConfiguration((cxt, config) =>
{
    IConfigCenter configCenter = new ConsulConfigCenter();
    configCenter.ConfigurationConfigCenter(cxt, config);
});
builder.Services.AddServiceConfig(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VideoContext>(opt =>
{
    var constr = builder.Configuration.GetConnectionString("DefaultConnectionString");
    opt.UseMySql(constr, new MySqlServerVersion(new Version(8, 0, 27)));
});
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>(); 
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
    //opt.UseInMemoryStorage();//ʹ�û�����������Ϣ��
    opt.UseMySql(options =>
    {
        options.ConnectionString= builder.Configuration.GetConnectionString("DefaultConnectionString");
    });
    //ʹ��EntityFramework�����ݿ���в���
    opt.UseEntityFramework<VideoContext>();
    // 6.4 �˹������Ǳ��� // �ڲ�aspnetcore
    opt.UseDashboard();
    opt.FailedRetryInterval = 1;
    opt.FailedRetryCount = 5;// �ٹ��˹�����ֻ�ܽ����˹�����
});
var app = builder.Build();

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
