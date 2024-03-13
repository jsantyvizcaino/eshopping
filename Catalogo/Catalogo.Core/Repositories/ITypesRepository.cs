using Catalogo.Core.Entities;

namespace Catalogo.Core.Repositories;

public interface ITypesRepository
{
    Task<IEnumerable<ProductType>> GetAllTypes();
}
