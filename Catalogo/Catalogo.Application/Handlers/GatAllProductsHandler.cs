using Catalogo.Application.Mappers;
using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using Catalogo.Core.Repositories;
using Catalogo.Core.Specification;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalogo.Application.Handlers;

public class GatAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<GatAllProductsHandler> _logger;

    public GatAllProductsHandler(IProductRepository productRepository, ILogger<GatAllProductsHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProducts(request.CatologSpecParams);
        var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
        _logger.LogDebug("Recived product list.Total Count: {productList}",productList.Count);
        return productResponseList;
        
    }
}
