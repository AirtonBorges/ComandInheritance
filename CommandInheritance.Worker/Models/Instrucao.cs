namespace ComandInheritance.Models;

public class Instrucao
{
    public Instrucao(string pTexto)
    {
        Texto = pTexto;
    }

    public Verbo? Verbo { get; private set; }

    public void AdicionarVerbo()
    {
        foreach(var pPalavra in Texto.Split(" "))
        {
            Enum.TryParse(typeof(Verbo), pPalavra, true, out var xVerbo);
            Verbo ??= (Verbo?)xVerbo;
        }
    }

    public string Texto { get; init; }
}