using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
//����ocelot.json�����ļ�
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot();
var app = builder.Build();

//ʹ��ocelot
app.UseOcelot().Wait();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
