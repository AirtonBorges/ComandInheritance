using System.Diagnostics;
using ComandInheritance.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ComandInheritance.Comandos;

public class ComandoProgramaAbrir : ComandoPrograma
{
    public override Task<bool> Executar(IServiceProvider pServiceProvider)
    {
        // TODO: Obter os programas todas as vezes é ruim

        var xConfiguracao = pServiceProvider
            .GetRequiredService<IConfiguracao>();
        ObterProgramas(xConfiguracao.CaminhoParaExcelProgramas).ForEach(p =>
        {
            if (p.Argumentos is null)
            {
                Process.Start(p.Caminho);
                Process.GetProcesses();
            }
            else
            {
                Process.Start(p.Caminho
                    , p.Argumentos);
            }

            Console.WriteLine($"Abrindo {p.Nome}");
        });
        return Task.FromResult(true);
    }
}