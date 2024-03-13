using Catalogo.Core.Entities;

namespace Catalogo.Core.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrands();
}
