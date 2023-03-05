using ComandInheritance.AutoMapper;
using ComandInheritance.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ComandInheritance.Models;

public class InstrucaoDeComando<TComando> : Instrucao, IInstrucaoDeComando
    where TComando : IComando
{
    public IComando ObterComando(IServiceProvider pServiceProvider)
    {
        var xComando = pServiceProvider.GetRequiredService<TComando>();
        xComando.Instrucao = this;
        return xComando;
    }
}