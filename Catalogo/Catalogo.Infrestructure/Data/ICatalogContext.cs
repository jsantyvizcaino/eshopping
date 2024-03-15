using Catalogo.Core.Entities;
using MongoDB.Driver;

namespace Catalogo.Infrestructure.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<ProductType> Types { get; }
    IMongoCollection<ProductBrand> Brands { get; }
}
