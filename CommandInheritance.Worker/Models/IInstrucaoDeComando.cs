using ComandInheritance.Services;

namespace ComandInheritance.Models;

public interface IInstrucaoDeComando
{
    public PalavrasChave PalavraChave { get; init; }
    public string Texto { get; init; }

    public IComando ObterComando(IServiceProvider pServiceProvider);
}