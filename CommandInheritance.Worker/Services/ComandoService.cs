using ComandInheritance.AutoMapper;
using ComandInheritance.Comandos;
using ComandInheritance.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ComandInheritance.Services;

public class ComandoService : IComandoService
{
    private readonly IServiceProvider _serviceProvider;

    public ComandoService(IServiceProvider pServiceProvider)
    {
        _serviceProvider = pServiceProvider;
    }

    public async Task<bool?> ExecutarComando(string pArgs)
    {
        var xPalavraChave = ObterPalavraChave(pArgs);
        var xInstrucao = new Instrucao
        {
            PalavraChave = xPalavraChave
            , Texto = pArgs
        };

        // TODO: Fazer isso virar um autommaper
        IInstrucaoDeComando xInstrucaoDeComando = xInstrucao.PalavraChave switch
        {
            PalavrasChave.Abrir => new InstrucaoDeComando<ComandoPrograma> { PalavraChave = PalavrasChave.Abrir, Texto = xInstrucao.Texto },
            PalavrasChave.Fechar => new InstrucaoDeComando<ComandoPrograma> { PalavraChave = PalavrasChave.Fechar, Texto = xInstrucao.Texto },
            PalavrasChave.Matar => new InstrucaoDeComando<ComandoPrograma> { PalavraChave = PalavrasChave.Matar, Texto = xInstrucao.Texto },
            PalavrasChave.Volume => new InstrucaoDeComando<ComandoMidia> { PalavraChave = PalavrasChave.Volume, Texto = xInstrucao.Texto },
            _ => new InstrucaoDeComando<ComandoInvalido> { PalavraChave = PalavrasChave.Invalido, Texto = xInstrucao.Texto }
        };

        try
        {
            using var xScope = _serviceProvider.CreateScope();
            var xComando = xInstrucaoDeComando.ObterComando(xScope.ServiceProvider);
            await xComando.Executar();
            return true;
        }
        catch (Exception pException)
        {
            Console.WriteLine(pException);
            return false;
        }
    }

    private PalavrasChave ObterPalavraChave(string pArgs)
    {
        // TODO: Bem ruim isso aqui
        var xPalavraChave = pArgs.Split(" ").ToList()
            .Select(p =>
            {
                Enum.TryParse<PalavrasChave>(p, true, out var xVerbo);
                return xVerbo;
            })
            .FirstOrDefault();

        return xPalavraChave;
    }
}

public interface IComando
{
    public IInstrucaoDeComando? Instrucao { get; set; }
    Task<bool> Executar();
}