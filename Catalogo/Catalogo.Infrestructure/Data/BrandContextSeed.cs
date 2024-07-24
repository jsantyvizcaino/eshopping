using Catalogo.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalogo.Infrestructure.Data;

internal static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(b=>true).Any();
        string path = Path.Combine("Data", "SeedData", "brands.json");
        if(!checkBrands)
        {
            var brandsData = File.ReadAllText(path);
            //var brandsData = File.ReadAllText("../Catalogo.Infrestructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if(brands is not null)
            {
                foreach (var item in brands)
                {
                    brandCollection.InsertOneAsync(item);
                }
            }
        }
    }
}
