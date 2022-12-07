using AutoMapper;
using ComandInheritance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.AutoMapper
{
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
                                Verbo.Abrir => pContext.Mapper.Map<Instrucao, ComandoAbrir>(p),
                                _ => pContext.Mapper.Map<Instrucao, ComandoInvalido>(p),
                            })
                            .ForMember(p => p.Texto, p => p.MapFrom(pS => pS.Texto))
                            .ForAllMembers(p => p.Ignore());

            CreateMap<Instrucao, ComandoAbrir>();
            CreateMap<Instrucao, ComandoInvalido>();
        }
    }
}
