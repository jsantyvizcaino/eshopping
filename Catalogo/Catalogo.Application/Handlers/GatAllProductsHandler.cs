using Catalogo.Application.Mappers;
using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using Catalogo.Core.Repositories;
using Catalogo.Core.Specification;
using MediatR;

namespace Catalogo.Application.Handlers;

public class GatAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GatAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProducts(request.CatologSpecParams);
        var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
        return productResponseList;
        
    }
}
