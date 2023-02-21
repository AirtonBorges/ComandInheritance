namespace ComandInheritance.Models;

public class Instrucao
{
    public Instrucao(string pTexto)
    {
        Texto = pTexto;
    }

    public PalavrasChave PalavraChave { get; private set; }

    public void AdicionarVerbo()
    {
        // TODO: Bem ruim isso aqui
        var xPalavraChave = Texto.Split(" ").ToList()
            .Select(p =>
                {
                    Enum.TryParse<PalavrasChave>(p, true, out var xVerbo);
                    return xVerbo;
                })
            .FirstOrDefault();

        PalavraChave = xPalavraChave;
    }

    public string Texto { get; init; }
}