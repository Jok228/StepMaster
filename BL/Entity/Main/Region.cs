using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.Main;

public struct Region
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }
    [BsonElement("fullName")]
    public string fullName { get; set; }
    public Region(string name, string full)
    {
        fullName = $"{full} {name}";
    }


}