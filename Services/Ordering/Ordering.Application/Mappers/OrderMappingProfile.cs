using AutoMapper;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers;

internal class OrderMappingProfile: Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order,OrderResponse>().ReverseMap();
        CreateMap<Order, CheckOutOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        
    }
}
