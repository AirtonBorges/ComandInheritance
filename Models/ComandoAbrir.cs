using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace ComandInheritance.Models
{
    public class ComandoAbrir : Comando
    {
        private  Dictionary<string, string> NOME_PROGRAMAS = new() { 
            {"visual studio code", "Microsoft VS Code"},
            {"vscode", "Microsoft VS Code"},
            {"code", "Microsoft VS Code"},
            {"opera", "Opera GX"},
            {"discord", "Discord"}
        };


        public override Task<bool> Executar(IServiceProvider pServiceProvider)
        {
            var xProgramasAbrir = ObterProgramas();
            xProgramasAbrir.ForEach(p => {
                var xLocal = Environment.GetEnvironmentVariable("path")?
                    .Split(";")
                    .FirstOrDefault(pS => pS.Contains(p));

                if (xLocal == null)
                 return;

                Process.Start(xLocal);  
            });
            Console.WriteLine("Abrindo Coisa que vc mandou abrir");
            return Task.FromResult(true);
        }

        private List<string> ObterProgramas()
        {
            var palavras = NOME_PROGRAMAS
                .Where(p => Texto.Contains(p.Key))
                .Select(p => p.Value)
                .Distinct()
                .ToList();
            return palavras;
        }
    }
}
