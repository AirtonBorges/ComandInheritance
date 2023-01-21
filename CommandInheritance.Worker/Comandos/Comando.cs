namespace ComandInheritance.Comandos;

public abstract class Comando
{
    public required string Texto { get; set; }
    public abstract Task<bool> Executar(IServiceProvider pServiceProvider);
}