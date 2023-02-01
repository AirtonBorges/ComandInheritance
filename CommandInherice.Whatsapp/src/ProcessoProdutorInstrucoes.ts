import { Db, MongoClient } from 'mongodb';
import { Message, Whatsapp } from 'venom-bot';

import { IConfiguracao } from './configuracoes/IConfiguracao';

export class ProcessoProdutorInstrucoes {
    private _db?: Db;

    constructor(private configuracao: IConfiguracao) {
    }

    public async iniciar(client: Whatsapp): Promise<void> {
        const xClient = await MongoClient.connect(this.configuracao.mongoUrl);
        this._db = xClient.db(this.configuracao.mongoDbName);

        await client.onAnyMessage(async (message) => await this.adicionar_instrucao(message));
    }

    public async adicionar_instrucao(message: Message): Promise<void> {
        if (message.chat.name !== this.configuracao.nomeChat) {
            return;
        }

        try {
            await this._db?.collection(this.configuracao.mongoCollectionName).insertOne({
                mensagem: message.body
            });
            
            console.log('Instrucao adicionada com sucesso');
        } catch (err) {
            console.log(err);
        }
    }
}