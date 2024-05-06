using Consul;
using Microsoft.EntityFrameworkCore;
using User_for__NUnit_Test.Config;
using User_for__NUnit_Test.Model;
using User_for__NUnit_Test.Repository;
using User_for__NUnit_Test.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Irepo, Repo>();
builder.Services.AddScoped<Iservice, service>();
builder.Services.AddDbContext<UserContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));
// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()
              .WithOrigins("http://localhost:4200"));

});

// Register multiple instances based on configuration
var consulClient = new ConsulClient();
var consulConfiguration = builder.Configuration.GetSection("Consul:Instances").Get<List<Service_Config>>();

foreach (var instanceConfig in consulConfiguration)
{
    var registration = new AgentServiceRegistration()
    {
        ID = instanceConfig.Id,
        Name = instanceConfig.Name,
        Address = instanceConfig.Address,
        Port = instanceConfig.Port,
        Check = new AgentServiceCheck()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
            Interval = TimeSpan.FromSeconds(15),
            HTTP = $"https://{instanceConfig.Address}:{instanceConfig.Port}/{instanceConfig.Name}",
            Timeout = TimeSpan.FromSeconds(15),
        }
    };
    await consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(false);
    await consulClient.Agent.ServiceRegister(registration).ConfigureAwait(false);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
