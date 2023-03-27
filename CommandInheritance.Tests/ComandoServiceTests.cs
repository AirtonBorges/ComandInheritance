using System.Reflection;
using ComandInheritance.Configurations;
using ComandInheritance.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;

namespace CommandInheritance.Tests;

public class ComandoServiceTests
{
    [Fact(DisplayName = "DADO uma frase de comando válida DEVE retornar verdadeiro.")]
    public async Task DeveRetornarVerdadeiro()
    {
        var xHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(p =>
            {
                p.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);
            })
            .ConfigureServices((p,pS) =>
            {
                pS.AdicionarConfiguracoes(p);
            })
            .Build();

        var xSut = xHost.Services.GetRequiredService<IComandoService>();

        // Act
        var xComando = await xSut.ExecutarComando("Abrir code");

        // Assert
        xComando.Should().BeTrue();
    }
}