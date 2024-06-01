using AutoMapper;
using GeladosBH.API.ViewModels;
using GeladosBH.Domain.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace GeladosBH.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {

            CreateMap<Produto, ProdutoViewModel>().ReverseMap();

            CreateMap<Carrinho, CarrinhoViewModel>()
                .ForMember(dest => dest.CarrinhoStatus, opt => opt.MapFrom(src => src.CarrinhoStatus))
                .ForMember(dest => dest.CarrinhoItens, opt => opt.MapFrom(src => src.CarrinhoItens));

            CreateMap<CarrinhoViewModel, Carrinho>()
                .ForMember(dest => dest.CarrinhoItens, opt => opt.MapFrom(src => src.CarrinhoItens));

            CreateMap<CarrinhoItem, CarrinhoItemViewModel>()
                .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));

            CreateMap<CarrinhoItemViewModel, CarrinhoItem>();

            CreateMap<CarrinhoStatus, CarrinhoStatusViewModel>().ReverseMap();

        }
    }
}
