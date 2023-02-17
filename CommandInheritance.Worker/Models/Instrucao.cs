namespace ComandInheritance.Models;

public class Instrucao
{
    public Instrucao(string pTexto)
    {
        Texto = pTexto;
    }

    public PalavrasChave? Verbo { get; private set; }

    public void AdicionarVerbo()
    {
        foreach(var pPalavra in Texto.Split(" "))
        {
            Enum.TryParse(typeof(PalavrasChave), pPalavra, true, out var xVerbo);
            Verbo ??= (PalavrasChave?)xVerbo;
        }
    }

    public string Texto { get; init; }
}