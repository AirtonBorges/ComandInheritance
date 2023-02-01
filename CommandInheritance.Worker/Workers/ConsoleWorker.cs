using ComandInheritance.Comandos;
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

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write("Insira comando: ");
                var xArgs = Console.ReadLine();

                if (xArgs == null)
                    continue;

                var xComando = _comandoService.ObterComando<Comando>(xArgs);
                if (xComando is null)
                {
                    Console.WriteLine("Não consegui criar um comando.");
                    continue;
                }

                var xExecutou = _comandoService.ExecutarComando(xComando);
            }
        }
        catch ( Exception xException) 
        {
            Console.WriteLine(xException);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}