using AutoMapper;
using ComandInheritance.Comandos;
using ComandInheritance.Models;

namespace ComandInheritance.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        ComandoMapping();
    } 

    private void ComandoMapping()
    {
        CreateMap<Instrucao, Comando>()
            .ConstructUsing((p, pContext) => p.PalavraChave switch
            {
                PalavrasChave.Abrir => pContext.Mapper.Map<Instrucao, ComandoPrograma>(p),
                PalavrasChave.Fechar => pContext.Mapper.Map<Instrucao, ComandoPrograma>(p),
                PalavrasChave.Matar => pContext.Mapper.Map<Instrucao, ComandoPrograma>(p),
                PalavrasChave.Volume => pContext.Mapper.Map<Instrucao, ComandoMidia>(p),
                _ => pContext.Mapper.Map<Instrucao, ComandoInvalido>(p),
            })
            .ForMember(p => p.Texto, p 
                => p.MapFrom(pS => pS.Texto))
            .ForMember(p => p.Instrucao, p
                => p.MapFrom(pS => pS))
        ;

        CreateMap<Instrucao, ComandoMidia>();
        CreateMap<Instrucao, ComandoPrograma>();
        CreateMap<Instrucao, ComandoInvalido>();
    }
}