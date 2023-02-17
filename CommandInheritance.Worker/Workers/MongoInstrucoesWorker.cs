using ComandInheritance.Comandos;
using ComandInheritance.Configurations;
using ComandInheritance.Entities;
using ComandInheritance.Services;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace ComandInheritance.Workers;

public class MongoInstrucoesWorker : IHostedService
{
    private readonly MongoClient _client;
    private readonly IMongoCollection<MongoInstrucao> _collection;
    private readonly IComandoService _comandoService;

    public MongoInstrucoesWorker(IConfiguracao pConfiguracao, IComandoService comandoService)
    {
        _client = new MongoClient(pConfiguracao.MongoDBConnectionString);
        var database = _client.GetDatabase(pConfiguracao.MongoDBDatabase);
        _collection = database.GetCollection<MongoInstrucao>(pConfiguracao.MongoDBCollection);

        _comandoService = comandoService;
    }

    public Task StartAsync(CancellationToken pCancellationToken)
    {
        try
        {
            Task.Run(async () => { await ObservarInstrucoesMongo(pCancellationToken); }, pCancellationToken);
        }
        catch (Exception xException)
        {
            Console.WriteLine(xException);
            throw;
        }
        finally
        {
            Console.WriteLine($"Finalizado Processo do {nameof(MongoInstrucoesWorker)}.");
        }

        return Task.CompletedTask;
    }

    private async Task ObservarInstrucoesMongo(CancellationToken pCancellationToken)
    {
        using var xCursor = await _collection.WatchAsync(cancellationToken: pCancellationToken);

        foreach (var xChange in xCursor.ToEnumerable(cancellationToken: pCancellationToken))
        {
            var xInstrucao = xChange.FullDocument;
            if (xInstrucao.mensagem == null)
            {
                continue;
            }

            var xComando = _comandoService.ObterComando<Comando>(xInstrucao.mensagem);
            var xExecutou = await _comandoService.ExecutarComando(xComando);
            Console.WriteLine($"Comando {xComando?.Texto}," + (xExecutou ? "Executou." : "Não Executou."));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
