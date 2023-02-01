using MongoDB.Driver;

namespace ComandInheritance.Configurations;

public interface IConfiguracao
{
    public string CaminhoParaExcelProgramas { get; }
    public string Topico { get; }
    public string KafkaBoostrapServers { get; }
    MongoClientSettings MongoDBConnectionString { get; }
    string MongoDBDatabase { get; }
    string MongoDBCollection { get;  }
}
public class Configuracao : IConfiguracao
{
    public string CaminhoParaExcelProgramas { get; set; } = string.Empty;
    public string Topico { get; set; } = string.Empty;
    public string KafkaBoostrapServers { get; set; } = string.Empty;
    public required MongoClientSettings MongoDBConnectionString { get; set; } = new ();
    public required string MongoDBDatabase { get; set; } = string.Empty;
    public required string MongoDBCollection { get; set; } = string.Empty;
}