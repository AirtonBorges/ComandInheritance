using AutoMapper;
using ComandInheritance.Comandos;
using ComandInheritance.Models;

namespace ComandInheritance.AutoMapper;

public class MapperProfile : Profile
{
    public class InstrucaoDeComandoProfile : Profile
    {
        public InstrucaoDeComandoProfile()
        {
            CreateMap<Instrucao, IInstrucaoDeComando>()
                .ConstructUsing((p, pS) => p.PalavraChave switch
                {
                    PalavrasChave.Abrir => pS.Mapper.Map<InstrucaoDeComando<ComandoPrograma>>(p),
                    PalavrasChave.Fechar => pS.Mapper.Map<InstrucaoDeComando<ComandoPrograma>>(p),
                    PalavrasChave.Matar => pS.Mapper.Map<InstrucaoDeComando<ComandoPrograma>>(p),
                    PalavrasChave.Volume => pS.Mapper.Map<InstrucaoDeComando<ComandoMidia>>(p),
                    _ => pS.Mapper.Map<InstrucaoDeComando<ComandoInvalido>>(p)
                });

            CreateMap<Instrucao, InstrucaoDeComando<ComandoPrograma>>();
            CreateMap<Instrucao, InstrucaoDeComando<ComandoMidia>>();
            CreateMap<Instrucao, InstrucaoDeComando<ComandoInvalido>>();
        }
    }
}