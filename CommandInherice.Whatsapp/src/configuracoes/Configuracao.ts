import * as dotenv from 'dotenv';
import { IConfiguracao } from "./IConfiguracao";

export class Configuracao implements IConfiguracao {
    public bootstrapServer: string;
    public nomeChat: string;
    public clientId: string;
    public topico: string;

    constructor() {
        dotenv.config();

        this.bootstrapServer = process.env.BOOTSTRAP_SERVER ?? "";
        this.topico = process.env.TOPICO ?? "";
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
        if (this.nomeChat === "")
            return false;

        return true;
    }
}
