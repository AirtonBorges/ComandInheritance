using System.Text.RegularExpressions;
using AutoMapper;
using ComandInheritance.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ComandInheritance.Services;

public partial class ComandoService : IComandoService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public ComandoService(IServiceProvider pServiceProvider, IMapper pMapper)
    {
        _serviceProvider = pServiceProvider;
        _mapper = pMapper;
    }

    public async Task<bool?> ExecutarComando(string pArgs)
    {
        try
        {
            var xPalavraChave = ObterPalavraChave(pArgs);
            var xInstrucao = new Instrucao
            {
                PalavraChave = xPalavraChave
                , Texto = pArgs
            };

            var xInstrucaoDeComando = _mapper.Map<IInstrucaoDeComando>(xInstrucao);

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
        var xMatches = MyRegex().Matches(pArgs);

        var xPalavras = from m in xMatches
            where !string.IsNullOrEmpty(m.Value)
            select m.Value;

        var xPalavraChave = PalavrasChave.Invalido;
        if (Enum.TryParse<PalavrasChave>(xPalavras.FirstOrDefault(), true, out var xPalavra))
            xPalavraChave = xPalavra;

        return xPalavraChave;
    }

    [GeneratedRegex("\\b[\\w']*\\b")]
    private static partial Regex MyRegex();
}

public interface IComando
{
    public IInstrucaoDeComando? Instrucao { get; set; }
    Task<bool> Executar();
}