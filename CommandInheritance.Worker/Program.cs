using System.Reflection;
using ComandInheritance.AutoMapper;
using ComandInheritance.Configurations;
using ComandInheritance.Services;
using ComandInheritance.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(p =>
    {
        p.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    })
    .ConfigureServices((pHost, pServices) =>
        pServices
        .Configure<Configuracao>(pHost.Configuration.GetRequiredSection(nameof(Configuracao)))
        .AddSingleton<ICommandService, ComandoService>()
        .AddSingleton<IConfiguracao, Configuracao>(p => p
            .GetRequiredService<IOptions<Configuracao>>().Value)
        .AddSingleton(pHost.Configuration)
        .AddAutoMapper(typeof(MapperProfile))
        .AddHostedService<ConsoleWorker>())
    .Build();

await host.RunAsync();