using System.Diagnostics;
using ComandInheritance.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ComandInheritance.Comandos;

public class ComandoProgramaMatar : ComandoPrograma
{
    public override Task<bool> Executar(IServiceProvider pServiceProvider)
    {
        // TODO: Obter os programas todas as vezes é ruim

        var xConfiguracao = pServiceProvider
            .GetRequiredService<IConfiguracao>();
        ObterProgramas(xConfiguracao.CaminhoParaExcelProgramas).ForEach(p =>
        {
            var xProcessos = Process.GetProcessesByName(p.Nome);
            xProcessos.FirstOrDefault()?.Kill();

            Console.WriteLine($"Matando {p.Nome}");
        });
        return Task.FromResult(true);
    }
}