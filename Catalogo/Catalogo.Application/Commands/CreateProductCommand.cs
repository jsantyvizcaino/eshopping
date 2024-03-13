using Catalogo.Application.Responses;
using Catalogo.Core.Entities;
using MediatR;

namespace Catalogo.Application.Commands;

public class CreateProductCommand: IRequest<ProductResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
    public decimal Price { get; set; }
}
