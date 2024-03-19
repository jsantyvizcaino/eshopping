using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

internal class GetOrderListQueryHandler: IRequestHandler<GetOrderListQuery,List<OrderResponse>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _repository.GetOrdersByUserName(request.UserName);
        return _mapper.Map<List<OrderResponse>>(orderList);
    }
}
