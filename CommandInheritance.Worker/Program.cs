﻿using System.Reflection;
using ComandInheritance.AutoMapper;
using ComandInheritance.Comandos;
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
    .ConfigureServices((p, pS) =>
    {
        pS.AdicionarConfiguracoes(p);
    })
    .Build();

await host.RunAsync();

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AdicionarConfiguracoes(this IServiceCollection pServiceCollection, HostBuilderContext pHostBuilderContext)
    {
        pServiceCollection.Configure<Configuracao>(pHostBuilderContext.Configuration.GetRequiredSection(nameof(Configuracao)))
            .AddSingleton<IConfiguracao, Configuracao>(p => p
                .GetRequiredService<IOptions<Configuracao>>().Value)
            .AddSingleton(pHostBuilderContext.Configuration)
        
            .AddSingleton<IComandoService, ComandoService>()

            .AddHostedService<MongoInstrucoesWorker>()
            .AddHostedService<ConsoleWorker>()

            .AddScoped<ComandoInvalido>()
            .AddScoped<ComandoMidia>()
            .AddScoped<ComandoPrograma>()
        
            .AddAutoMapper(typeof(MapperProfile));
    
        return pServiceCollection;
    }
}