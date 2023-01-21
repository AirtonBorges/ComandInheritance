using ComandInheritance.Configurations;
using CommandInheritanceApi.Infra;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Configuracao>(builder.Configuration.GetRequiredSection(nameof(Configuracao)));
builder.Services.AddSingleton<IConfiguracao, Configuracao>(p => p
        .GetRequiredService<IOptions<Configuracao>>().Value);
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
                     
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
