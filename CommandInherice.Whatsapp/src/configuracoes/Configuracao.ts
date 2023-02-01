import * as dotenv from 'dotenv';

import { IConfiguracao } from "./IConfiguracao";

export class Configuracao implements IConfiguracao {
    public bootstrapServer: string;
    public nomeChat: string;
    public clientId: string;
    public topico: string;
    public mongoCollectionName: string;
    public mongoDbName: string;
    public mongoUrl: string;

    constructor() {
        dotenv.config();

        this.topico = "";
        this.mongoCollectionName = process.env.MONGO_COLLECTION_NAME ?? "";;
        this.mongoDbName = process.env.MONGO_DB_NAME ?? "";
        this.mongoUrl = process.env.MONGO_URL ?? "";
        this.bootstrapServer = process.env.BOOTSTRAP_SERVER ?? "";
        this.clientId = process.env.CLIENT_ID ?? "";
        this.nomeChat = process.env.NOME_CHAT ?? "";
    }

    public ehValido(): boolean {
        if (this.bootstrapServer === "")
            return false;
        if (this.topico === "")
            return false;
        if (this.clientId === "")
            return false;
        if (this.mongoCollectionName === "")
            return false;
        if (this.mongoDbName === "")
            return false;
        if (this.mongoUrl === "")
            return false;

        return true;
    }
}
