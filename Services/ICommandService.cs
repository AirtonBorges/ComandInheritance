using ComandInheritance.Models;

namespace ComandInheritance.Services
{
    public interface ICommandService
    {
        TComando ObterComando<TComando>(string pArgs) where TComando : Comando;
        Task<bool> ExecutarComando<TComando>(TComando Comando) where TComando : Comando;
    }
}