using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Core.ConfigCenter;
using SchoolManageMicroService.Core.Register.Extensions;
using SchoolManageMicroService.Videos.Contexts;
using SchoolManageMicroService.Videos.Repositories;
using SchoolManageMicroService.Videos.Services;

var builder = WebApplication.CreateBuilder(args);
//加载配置中心配置文件
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
    //opt.UseInMemoryStorage();//使用缓存作本地消息表
    opt.UseMySql(options =>
    {
        options.ConnectionString= builder.Configuration.GetConnectionString("DefaultConnectionString");
    });
    //使用EntityFramework对数据库进行操作
    opt.UseEntityFramework<VideoContext>();
    // 6.4 人工处理仪表盘 // 内部aspnetcore
    opt.UseDashboard();
    opt.FailedRetryInterval = 1;
    opt.FailedRetryCount = 5;// 操过人工次数只能进行人工处理。
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
