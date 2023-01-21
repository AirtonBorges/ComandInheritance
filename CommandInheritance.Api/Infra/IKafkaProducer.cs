using Confluent.Kafka;

namespace CommandInheritanceApi.Infra;

public interface IKafkaProducer
{
    public Task<DeliveryResult<Null, string>> PublicarInstrucao(string pMensagem);
}