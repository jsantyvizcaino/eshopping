using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

internal class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckOutOrderCommandHandler> _logger;

    public CheckOutOrderCommandHandler(IOrderRepository repository, IMapper mapper,
                                       ILogger<CheckOutOrderCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var generatedOrder = await _repository.AddAsync(orderEntity);
        _logger.LogInformation(($"Order {generatedOrder} successfully created."));
        return generatedOrder.Id;
    }
}
