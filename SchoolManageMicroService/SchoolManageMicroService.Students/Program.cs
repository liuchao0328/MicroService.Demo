using Microsoft.EntityFrameworkCore;
using SchoolManageMicroService.Core.Register.Extensions;
using SchoolManageMicroService.Students.Context;
using SchoolManageMicroService.Students.Repositories;
using SchoolManageMicroService.Students.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentContext>(options =>
{
    var str = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseMySql(str,new MySqlServerVersion(new Version(8, 0, 27)));
    options.LogTo(Console.WriteLine, minimumLevel: LogLevel.Warning);
});
//（加载注册服务需要的配置文件）
builder.Services.AddServiceConfig(builder.Configuration);
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddHttpContextAccessor();
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
