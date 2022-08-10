using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Classes.Context;
using SchoolManageMicroService.Classes.Repositories;
using SchoolManageMicroService.Classes.Services;
using SchoolManageMicroService.Core.Register.Extensions;
using Winton.Extensions.Configuration.Consul;

var builder = WebApplication.CreateBuilder(args);

//�����������������ļ�
builder.Host.ConfigureAppConfiguration((cxt, config) =>
{
    var figuration = config.AddJsonFile("consulcenter.json").Build();
    var address = figuration["ConfigCenterAddress"];
    config.AddConsul("appsettings.json", configuration =>
    {
        configuration.ConsulConfigurationOptions = opt =>
        {
            opt.Address = new Uri(address);
        };
        configuration.ReloadOnChange = true;//�ȼ��������ļ�
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClassContext>(options =>
{
    var str = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseMySql(str, new MySqlServerVersion(new Version(8, 0, 27)));
    options.LogTo(Console.WriteLine, minimumLevel: LogLevel.Warning);
});
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddServiceConfig(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseConsulRegistry();
app.UseAuthorization();

app.MapControllers();

app.Run();
