using ComandInheritance.Services;

namespace ComandInheritance.Comandos;

public class ComandoInvalido : Comando, IComando
{
    public override Task<bool> Executar()
    {
        throw new NotImplementedException();
    }
}