using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
//加载ocelot.json配置文件
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot();
var app = builder.Build();

//使用ocelot
app.UseOcelot().Wait();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
