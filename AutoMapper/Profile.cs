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
                Verbo.Abrir => pContext.Mapper.Map<Instrucao, ComandoProgramaAbrir>(p),
                Verbo.Fechar => pContext.Mapper.Map<Instrucao, ComandoProgramaFechar>(p),
                Verbo.Matar => pContext.Mapper.Map<Instrucao, ComandoProgramaMatar>(p),
                _ => pContext.Mapper.Map<Instrucao, ComandoInvalido>(p),
            })
            .ForMember(p => p.Texto, p 
                => p.MapFrom(pS => pS.Texto))
            ;

        CreateMap<Instrucao, ComandoProgramaAbrir>();
        CreateMap<Instrucao, ComandoProgramaFechar>();
        CreateMap<Instrucao, ComandoProgramaMatar>();
        CreateMap<Instrucao, ComandoPrograma>();
        CreateMap<Instrucao, ComandoInvalido>();
    }
}