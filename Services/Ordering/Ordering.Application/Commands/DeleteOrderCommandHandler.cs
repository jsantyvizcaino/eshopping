using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Commands;

internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository repository,
                                     ILogger<DeleteOrderCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _repository.GetByIdAsync(request.Id);
        if (orderToDelete == null) throw new OrderNotFoundException(nameof(Order), request.Id);

        await _repository.DeleteAsync(orderToDelete);
        _logger.LogInformation($"Order with Id {request.Id} is deleted successfully.");
        return Unit.Value;
    }
}
