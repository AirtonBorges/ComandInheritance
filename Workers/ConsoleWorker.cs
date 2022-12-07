using ComandInheritance.Models;
using ComandInheritance.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.Workers
{
    public class ConsoleWorker : IHostedService
    {
        private readonly ICommandService _comandoService;

        public ConsoleWorker(ICommandService pComandoService)
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

                    var xComando = _comandoService.ObterComando<Comando>(xArgs);
                    var xExecutou = _comandoService.ExecutarComando(xComando);
                }
            }
            catch ( Exception xException) 
            {
                Console.WriteLine("{Message}", xException.Message);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
