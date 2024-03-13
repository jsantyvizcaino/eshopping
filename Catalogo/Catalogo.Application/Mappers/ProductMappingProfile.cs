using AutoMapper;
using Catalogo.Application.Commands;
using Catalogo.Application.Responses;
using Catalogo.Core.Entities;
using Catalogo.Core.Specification;

namespace Catalogo.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product,ProductResponse>().ReverseMap();
        CreateMap<Product,CreateProductCommand>().ReverseMap();
        CreateMap<ProductBrand,BrandResponse>().ReverseMap();
        CreateMap<ProductType,TypeResponse>().ReverseMap();
        CreateMap<Pagination<Product>,Pagination<ProductResponse>>().ReverseMap();
    }
}
