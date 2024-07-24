using Catalogo.Application.Commands;
using Catalogo.Application.Mappers;
using Catalogo.Application.Responses;
using Catalogo.Core.Entities;
using Catalogo.Core.Repositories;
using MediatR;

namespace Catalogo.Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;



    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if(productEntity is null)
        {
            throw new ApplicationException("no se pudo crear el producto");
        }

        var newProduct = await _productRepository.CreateProduct(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        return productResponse;
    }
}
