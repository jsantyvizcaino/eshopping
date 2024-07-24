using Catalogo.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalogo.Infrestructure.Data;

internal class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            var typesData = File.ReadAllText(path);
            //var typesData = File.ReadAllText("../Catalogo.Infrestructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types is not null)
            {
                foreach (var item in types)
                {
                    typeCollection.InsertOneAsync(item);
                }
            }
        }
    }
}
