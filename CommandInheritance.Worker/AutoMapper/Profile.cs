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
            .ConstructUsing((p, pContext) => p.Verbo switch
            {
                PalavrasChave.Abrir => pContext.Mapper.Map<Instrucao, ComandoProgramaAbrir>(p),
                PalavrasChave.Fechar => pContext.Mapper.Map<Instrucao, ComandoProgramaFechar>(p),
                PalavrasChave.Matar => pContext.Mapper.Map<Instrucao, ComandoProgramaMatar>(p),
                PalavrasChave.Volume => pContext.Mapper.Map<Instrucao, ComandoMidia>(p),
                _ => pContext.Mapper.Map<Instrucao, ComandoInvalido>(p),
            })
            .ForMember(p => p.Texto, p 
                => p.MapFrom(pS => pS.Texto))
            .ForAllMembers(p => p.Ignore())
            ;

        CreateMap<Instrucao, ComandoProgramaAbrir>();
        CreateMap<Instrucao, ComandoProgramaFechar>();
        CreateMap<Instrucao, ComandoProgramaMatar>();
        CreateMap<Instrucao, ComandoMidia>();
        CreateMap<Instrucao, ComandoInvalido>();
        CreateMap<Instrucao, ComandoPrograma>();
        CreateMap<Instrucao, ComandoInvalido>();
    }
}