using ApiREST_Gestion.Dtos;
using ApiREST_Gestion.Modelos;
using AutoMapper;

namespace ApiREST_Gestion.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ClienteDto, Cliente>()
                .ReverseMap();
            CreateMap<ClienteCreacionDto, Cliente>();
        }
    }
}
