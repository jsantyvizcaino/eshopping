﻿using Basket.Application.Command;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

internal class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.UserName);
        return Unit.Value;
    }
}
