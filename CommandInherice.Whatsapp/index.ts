import { create } from 'venom-bot'
import { Configuracao } from './src/configuracoes/Configuracao';
import { ProcessoProdutorInstrucoes } from './src/ProcessoProdutorInstrucoes';

create({
    session: 'Teste',
    multidevice: true
})
    .then(async (client) => {
        const xConfiguracao = new Configuracao();
        if(xConfiguracao.ehValido())
            throw new ReferenceError("Configuracoes invalidos");

        const xProcesso = new ProcessoProdutorInstrucoes(xConfiguracao);
        await xProcesso.iniciar(client)
    })
    .catch((erro) => {
        console.log(erro);
    });
