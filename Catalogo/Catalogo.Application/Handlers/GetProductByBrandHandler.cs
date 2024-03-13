using Catalogo.Application.Mappers;
using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using Catalogo.Core.Repositories;
using MediatR;

namespace Catalogo.Application.Handlers;

internal class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByBrand(request.Brandname);
        var productResponseList = ProductMapper.Mapper.Map<List<ProductResponse>>(productList);
        return productResponseList;
    }
}
