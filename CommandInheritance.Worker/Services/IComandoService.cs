namespace ComandInheritance.Services;

public interface IComandoService
{
    Task<bool?> ExecutarComando(string pArgs);
}