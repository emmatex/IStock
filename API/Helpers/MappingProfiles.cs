using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<ProductToCreateDto, Product>();
            CreateMap<ProductToUpdateDto, Product>();
            CreateMap<ProductBrand, ProductBrandDto>();
            CreateMap<CreateUpdateBrandDto, ProductBrand>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<CreateUpdateTypeDto, ProductType>();
            CreateMap<StockState, StockStateDto>();
            CreateMap<CreateUpdateStockStateDto, StockState>();

            CreateMap<StockReceive, StockReceiveDto>();
            CreateMap<StockReceive, SingleStockReceiveDto>();
            CreateMap<CreateUpdateReceiveDto, StockReceive>();
            CreateMap<StockReceiveDetail, StockReceiveDetailDto>();
            CreateMap<CreateUpdateReceiveDetailDto, StockReceiveDetail>();


        }
    }
}
