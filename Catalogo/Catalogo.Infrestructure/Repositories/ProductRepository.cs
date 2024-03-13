using Catalogo.Core.Entities;
using Catalogo.Core.Repositories;
using Catalogo.Core.Specification;
using Catalogo.Infrestructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalogo.Infrestructure.Repositories;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    public readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _context = catalogContext;
    }


    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p=>p.Id, id);
        DeleteResult deleteResult = await _context.Products
                                                    .DeleteOneAsync(filterDefinition);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount>0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
       return await _context.Brands.Find(b=>true)
                                .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context.Types.Find(t => true)
                               .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
        return await _context.Products
                             .Find(filter)
                             .ToListAsync();
    }

    public async Task<Product> GetProductById(string id)
    {
        return await _context.Products
                            .Find(p=>p.Id == id)
                            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p=>p.Name,name);
        return await _context.Products
                             .Find(filter)
                             .ToListAsync();
    }

    public async Task<Pagination<Product>> GetProducts(CatologSpecParams catologSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(catologSpecParams.Search))
        {
            var searchFilter = builder.Regex(x=>x.Name,new BsonRegularExpression(catologSpecParams.Search));
            filter &= searchFilter;
        }
        if (!string.IsNullOrEmpty(catologSpecParams.BrandId))
        {
            var brandFilter = builder.Eq(x=>x.Brands.Id,catologSpecParams.BrandId);
            filter &= brandFilter;
        }
        
        if (!string.IsNullOrEmpty(catologSpecParams.TypeId))
        {
            var typeFilter = builder.Eq(x=>x.Types.Id,catologSpecParams.TypeId);
            filter &= typeFilter;
        }

        if (!string.IsNullOrEmpty(catologSpecParams.Sort))
        {
            return new Pagination<Product>
            {
                PageSize = catologSpecParams.PageSize,
                PageIndex = catologSpecParams.PageIndex,
                Data = await DataFilter(catologSpecParams,filter),
                Count = await _context.Products.CountDocumentsAsync(p => true)
            };
        }

        return new Pagination<Product>
        {
            PageSize = catologSpecParams.PageSize,
            PageIndex = catologSpecParams.PageIndex,
            Data = await _context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip(catologSpecParams.PageSize * (catologSpecParams.PageIndex - 1))
                .Limit(catologSpecParams.PageSize)
                .ToListAsync(),
            Count = await _context.Products.CountDocumentsAsync(p => true)
        };
    }

   

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context.Products
                                                .ReplaceOneAsync(p=>p.Id==product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount>0;
    }

    private async Task<IReadOnlyList<Product>> DataFilter(CatologSpecParams catologSpecParams, FilterDefinition<Product> filter)
    {
        switch (catologSpecParams.Sort)
        {
            case "priceAsc":
                return await _context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Price"))
                .Skip(catologSpecParams.PageSize * (catologSpecParams.PageIndex - 1))
                .Limit(catologSpecParams.PageSize)
                .ToListAsync();
            case "priceDesc":
                return await _context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Descending("Price"))
                .Skip(catologSpecParams.PageSize * (catologSpecParams.PageIndex - 1))
                .Limit(catologSpecParams.PageSize)
                .ToListAsync();
            default:
                return await _context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip(catologSpecParams.PageSize * (catologSpecParams.PageIndex - 1))
                .Limit(catologSpecParams.PageSize)
                .ToListAsync();

        }
    }


}
