using Catalogo.Core.Entities;
using Catalogo.Core.Specification;

namespace Catalogo.Core.Repositories;

public interface IProductRepository
{
    Task<Pagination<Product>> GetProducts(CatologSpecParams catologSpecParams);
    Task<Product> GetProductById(string id);
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductByBrand(string name);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}
