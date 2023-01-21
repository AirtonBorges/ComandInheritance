using ComandInheritance.Configurations;
using Confluent.Kafka;

namespace CommandInheritanceApi.Infra;

public class KafkaProducer: ProducerBuilder<Null, string>, IKafkaProducer
{
    private readonly IConfiguracao _configuracao;

    public KafkaProducer(IConfiguracao configuracao) 
        : base(new ProducerConfig { BootstrapServers = configuracao.KafkaBoostrapServers })
    {
        _configuracao = configuracao;
    }

    public async Task<DeliveryResult<Null, string>> PublicarInstrucao(string pMensagem)
    {
        try
        {
            using var xProducer = base.Build();
            var xResult = await xProducer.ProduceAsync(_configuracao.Topico, 
                new Message<Null, string> { Value = pMensagem });

            return xResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}