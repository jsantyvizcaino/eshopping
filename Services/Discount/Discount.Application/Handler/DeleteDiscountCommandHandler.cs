﻿using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handler;

internal class DeleteDiscountCommandHandler: IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;
    public readonly IMapper _mapper;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
        return deleted;
    }
}
