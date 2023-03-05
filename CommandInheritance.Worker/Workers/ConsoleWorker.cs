using ComandInheritance.Services;
using Microsoft.Extensions.Hosting;

namespace ComandInheritance.Workers;

public class ConsoleWorker : IHostedService
{
    private readonly IComandoService _comandoService;

    public ConsoleWorker(IComandoService pComandoService)
    {
        _comandoService = pComandoService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write("Insira comando: ");
                var xArgs = Console.ReadLine();

                if (xArgs == null)
                    continue;

                var xComando = await _comandoService.ExecutarComando(xArgs);
                if (xComando == false)
                {
                    Console.WriteLine("Não consegui criar um comando.");
                }
            }
        }
        catch ( Exception xException) 
        {
            Console.WriteLine(xException);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}