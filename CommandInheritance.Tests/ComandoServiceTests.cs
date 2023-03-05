using AutoMapper;
using ComandInheritance.AutoMapper;
using ComandInheritance.Comandos;
using ComandInheritance.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CommandInheritance.Tests;

public class ComandoServiceTests
{
    [Fact(DisplayName = "DADO uma instrução DEVE retornar o comando correto.")]
    public async Task DeveRetornarComandoCorreto()
    {
        // TODO: Arrumar teste
        // Arrange
        var xServiceProvider = Substitute.For<IServiceProvider>();
        xServiceProvider
            .GetService(Arg.Is(typeof(IServiceScopeFactory)))
            .Returns(Substitute.For<IServiceScopeFactory>());
        xServiceProvider.GetRequiredService<ComandoPrograma>().Returns(Substitute.For<ComandoPrograma>());

        // var _ = new MapperConfiguration(p => p.AddProfile(typeof(MapperProfile)));

        var xSut = new ComandoService(xServiceProvider);

        // Act
        var xComando = await xSut.ExecutarComando("Abrir code");

        // Assert
        xComando.Should().BeTrue();
    }
}