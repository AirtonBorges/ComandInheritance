using ComandInheritance.Services;
using ComandInheritance.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddSingleton<ICommandService, CommandService>()
        .AddHostedService<ConsoleWorker>())
    .Build();

await host.RunAsync();