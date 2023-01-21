import { Kafka, Producer } from 'kafkajs';
import { Message, Whatsapp } from 'venom-bot';
import { IConfiguracao } from './configuracoes/IConfiguracao';

export class ProcessoProdutorInstrucoes {
    private kafka: Producer;
    constructor(private configuracao: IConfiguracao) {
        if (configuracao.bootstrapServer === undefined) {
            throw new ReferenceError("BOOTSTRAP_SERVER variable is not defined");
        }

        this.kafka = new Kafka({
            clientId: configuracao.clientId,
            brokers: [configuracao.bootstrapServer],
        }).producer();;
    }

    public async iniciar(client: Whatsapp): Promise<void> {
        await this.kafka.connect();
        await client.onAnyMessage(async (message) => await this.adicionar_instrucao(message));
    }

    public async adicionar_instrucao(message: Message): Promise<void> {
        if (message.chat.name !== this.configuracao.nomeChat) {
            return;
        }

        const responses = await this.kafka.send({
            topic: this.configuracao.topico,
            messages: [{
                value: message.body
            }]
        });

        console.log(responses);
    }
}
