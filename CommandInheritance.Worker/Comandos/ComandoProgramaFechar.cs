using System.Diagnostics;
using ComandInheritance.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ComandInheritance.Comandos;

public class ComandoProgramaFechar: ComandoPrograma
{
    public override Task<bool> Executar(IServiceProvider pServiceProvider)
    {
        // TODO: Obter os programas todas as vezes é ruim

        var xConfiguracao = pServiceProvider
            .GetRequiredService<IConfiguracao>();
        ObterProgramas(xConfiguracao.CaminhoParaExcelProgramas).ForEach(p =>
        {
            var xProcessos = Process.GetProcessesByName(p.Nome);
            xProcessos.FirstOrDefault(pS => pS.MainWindowTitle != "")?.CloseMainWindow();

            Console.WriteLine($"Fechando {p.Nome}");
        });
        return Task.FromResult(true);
    }
}