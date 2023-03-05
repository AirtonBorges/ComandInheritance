using AutoMapper;
using ComandInheritance.Comandos;
using ComandInheritance.Models;
using ComandInheritance.Services;

namespace ComandInheritance.AutoMapper;

public class MapperProfile : Profile
{
    public class InstrucaoDeComandoProfile : Profile
    {
        public InstrucaoDeComandoProfile()
        {
        }
    }
}

public interface IInstrucaoDeComando
{
    public PalavrasChave PalavraChave { get; init; }
    public string Texto { get; init; }

    public IComando ObterComando(IServiceProvider pServiceProvider);
}
