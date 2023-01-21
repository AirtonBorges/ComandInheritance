using System.Data;
using ComandInheritance.Models;
using ExcelDataReader;

namespace ComandInheritance.Comandos;

// TODO: Fazer todos os ComandoPrograma virarem um só
public abstract class ComandoPrograma : Comando
{
    public List<Programa> ObterProgramas(string pCaminho)
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