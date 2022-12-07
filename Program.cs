using AutoMapper;
using ComandInheritance.AutoMapper;
using ComandInheritance.Services;
using ComandInheritance.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddAutoMapper(typeof(MapperProfile))
        .AddSingleton<ICommandService, ComandoService>()
        .AddHostedService<ConsoleWorker>())
    .Build();

await host.RunAsync();