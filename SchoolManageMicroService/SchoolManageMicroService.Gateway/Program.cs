using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);
//����ocelot.json�����ļ�
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//ocelot���consul��polly����ʹ��
builder.Services.AddOcelot().AddConsul().AddPolly();
var app = builder.Build();

//ʹ��ocelot
app.UseOcelot().Wait();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
