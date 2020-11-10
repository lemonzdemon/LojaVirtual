using AutoMapper;
using BiaBraga.Domain.Models.Dtos;
using BiaBraga.Domain.Models.Entitys;

namespace BiaBraga.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserInsertDto>().ForMember(dest => dest.Gener, opt =>
            {
                opt.MapFrom(src => src.Gener);
            }).ReverseMap();
            CreateMap<User, UserViewDto>().ForMember(dest => dest.Gener, opt =>
            {
                opt.MapFrom(src => src.Gener);
            }).ReverseMap();

            CreateMap<Product, ProductViewDto>().ForMember(dest => dest.Categoria, opt =>
            {
                opt.MapFrom(src => src.Categoria);
            }).ReverseMap();
        }
    }
}
