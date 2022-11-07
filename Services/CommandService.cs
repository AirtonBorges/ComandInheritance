using ComandInheritance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.Services
{
    public class CommandService : ICommandService
    {
        public TComando ObterComando<TComando>(string pArgs) where TComando : Comando
        {
            var xComando = new Comando
            {
                Mensagem = pArgs
            };

            return (TComando)xComando;
        }

        public Task<bool> ExecutarComando<TComando>(TComando pComando) where TComando : Comando
        {
            Console.WriteLine(pComando.Mensagem);
            return Task.FromResult(true);
        }
    }
}
