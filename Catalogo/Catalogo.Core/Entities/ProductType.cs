using MongoDB.Bson.Serialization.Attributes;

namespace Catalogo.Core.Entities;

public class ProductType : BaseEntity
{
    [BsonElement("Name")]
    public string Name { get; set; }
}
