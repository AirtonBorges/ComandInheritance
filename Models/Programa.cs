namespace ComandInheritance.Models;

public class Programa
{
    public required string Nome { get; set; }
    public required List<string> PalavrasChave { get; set; }
    public required string Caminho { get; set; }
    public string? Argumentos { get; set; }
}