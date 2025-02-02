﻿using AutoMapper;
using EventBus.Messages.Events;
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
        CreateMap<CheckOutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        CreateMap<CheckOutOrderCommand, BasketCheckoutEventV2>().ReverseMap();
        
    }
}
