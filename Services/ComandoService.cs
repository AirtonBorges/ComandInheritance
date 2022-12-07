using AutoMapper;
using ComandInheritance.Models;

namespace ComandInheritance.Services
{
    public class ComandoService : ICommandService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public ComandoService(IMapper pMapper, IServiceProvider pServiceProvider)
        {
            _mapper = pMapper;
            _serviceProvider = pServiceProvider;
        }

        public TComando ObterComando<TComando>(string pArgs) where TComando : Comando
        {
            var xInstrucao = new Instrucao(pArgs);

            TentaAdicionarVerbo(xInstrucao);
            var xComando = _mapper.Map<Comando>(xInstrucao);
            return (TComando)xComando;
        }

        private static void TentaAdicionarVerbo(Instrucao xInstrucao)
        {
            try
            {
                xInstrucao.AdicionarVerbo();
            }
            catch (Exception xException)
            {
                Console.WriteLine(xException);
            }
        }

        public async Task<bool> ExecutarComando<TComando>(TComando pComando) where TComando : Comando
        {
            var xExecutou = false;
            try
            {
                xExecutou = await pComando.Executar(_serviceProvider);
            }
            catch (Exception xException)
            {
                Console.WriteLine($"Erro ao executar o comando {pComando.Texto}\n {xException}");
            }
            return xExecutou;
        }
    }
}
