using Catalogo.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalogo.Infrestructure.Data;

internal class ProductContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText(path);
            //var productsData = File.ReadAllText("../Catalogo.Infrestructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products is not null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}
