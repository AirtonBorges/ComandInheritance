using ComandInheritance.Comandos;

namespace ComandInheritance.Services;

public interface IComandoService
{
    TComando? ObterComando<TComando>(string pArgs) where TComando : Comando;
    Task<bool> ExecutarComando<TComando>(TComando pComando) where TComando : Comando?;
}