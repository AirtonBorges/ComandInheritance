using ComandInheritance.AutoMapper;
using ComandInheritance.Services;

namespace ComandInheritance.Comandos;

public abstract class Comando: IComando
{
    public IInstrucaoDeComando Instrucao { get; set; } // TODO: resolver nulabilidade
    public abstract Task<bool> Executar();
}