namespace ComandInheritance.Configurations;

public interface IConfiguracao
{
    string KafkaBoostrapServers { get; }
    string Topico { get; }
}
public class Configuracao : IConfiguracao
{
    public string KafkaBoostrapServers { get; set; } = string.Empty;
    public string Topico { get; set; } = string.Empty;
}