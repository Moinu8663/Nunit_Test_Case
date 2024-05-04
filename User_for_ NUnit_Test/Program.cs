using Microsoft.EntityFrameworkCore;
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
builder.Services.AddDbContext<UserContext>(options => options.UseInMemoryDatabase(databaseName: "InMemoryDb"));
// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()
              .WithOrigins("http://localhost:4200"));

});

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
