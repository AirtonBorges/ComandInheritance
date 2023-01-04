namespace ComandInheritance.Configurations;

public interface IConfiguracao
{
    public string CaminhoParaExcelProgramas { get; }
}
public class Configuracao : IConfiguracao
{
    public string CaminhoParaExcelProgramas { get; set; } = string.Empty;
}