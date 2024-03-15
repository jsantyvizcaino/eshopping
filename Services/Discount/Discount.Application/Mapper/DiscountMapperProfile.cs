using AutoMapper;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mapper;

internal class DiscountMapperProfile : Profile
{
    public DiscountMapperProfile()
    {
        CreateMap<Coupon,CouponModel>().ReverseMap();
    }
}
