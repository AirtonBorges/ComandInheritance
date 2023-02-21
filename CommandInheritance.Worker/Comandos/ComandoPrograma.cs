using System.Data;
using System.Diagnostics;
using ComandInheritance.Configurations;
using ComandInheritance.Models;
using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ComandInheritance.Comandos;

public sealed class ComandoPrograma : Comando
{
    // TODO: meio mais ou menos, voltar e pensar em algo melhor
    private Dictionary<PalavrasChave, Func<Programa, bool>> Processos => new()
    {
        { PalavrasChave.Abrir, AbrirPrograma }
        , { PalavrasChave.Fechar, FecharPrograma }
        , { PalavrasChave.Matar, MatarPrograma }
    };

    public override Task<bool> Executar(IServiceProvider pServiceProvider)
    {
        var xConfiguracao = pServiceProvider
            .GetRequiredService<IConfiguracao>();

        var xProgramas = ObterProgramas(xConfiguracao.CaminhoParaExcelProgramas);
        var xExecutouComSucesso = !xProgramas.Any(ProcessarInstrucao);

        return Task.FromResult(xExecutouComSucesso);

        bool ProcessarInstrucao(Programa pPrograma)
        {
            return Processos[Instrucao.PalavraChave](pPrograma);
        }
    }

    private static bool AbrirPrograma(Programa pPrograma)
    {
        if (pPrograma.Argumentos is null)
        {
            Process.Start(pPrograma.Caminho);
            Process.GetProcesses();
        }
        else
        {
            Process.Start(pPrograma.Caminho
                , pPrograma.Argumentos);
        }

        Console.WriteLine($"Abrindo {pPrograma.Nome}");

        return true;
    }

    private bool MatarPrograma(Programa pPrograma)
    {
        var xProcesso = ObterProcesso(pPrograma);
            
        xProcesso?.Kill();
        Console.WriteLine($"Matando processo: {pPrograma.Nome}");

        return true;
    }

    private bool FecharPrograma(Programa pPrograma)
    {
        var xProcesso = ObterProcesso(pPrograma);

        xProcesso?.CloseMainWindow();
        Console.WriteLine($"Fechando {pPrograma.Nome}");

        return true;
    }

    private Process? ObterProcesso(Programa pPrograma)
    {
        var xProcessos = Process.GetProcessesByName(pPrograma.Nome);

        if (xProcessos.IsNullOrEmpty())
        {
            Console.WriteLine($"Não encontrei o programa: {pPrograma.Nome}.");
            return null;
        }

        var xRetorno = xProcessos.FirstOrDefault(pS => pS.MainWindowTitle != "");
        return xRetorno;
    }

    private List<Programa> ObterProgramas(string pCaminho)
    {
        var xProgramas = new List<Programa>();

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using var stream = File.Open(pCaminho
            , FileMode.Open
            , FileAccess.Read); // TODO: Fazer pegar o path da configuracao
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var worksheet = reader.AsDataSet().Tables[0];

        xProgramas.AddRange(from DataRow row in worksheet.Rows
            let xNome = row[0].ToString()
            let xPalavrasChave = row[1].ToString()
            let xCaminho = row[2].ToString()

            where xNome != null
                  && xCaminho != null
                  && xPalavrasChave != null

            select new Programa
            {
                Nome = xNome
                , Caminho = xCaminho
                , PalavrasChave = xPalavrasChave.Split(',').ToList(),
                Argumentos = row[3].ToString()
            }
        );

        var xProgramasFiltrados = xProgramas
            .Where(p => p.PalavrasChave.Any(pS => Texto.Contains(pS)))
            .Where(p => p.PalavrasChave.Any(pS => pS != "") )
            .Distinct()
            .ToList();

        return xProgramasFiltrados;
    }
}