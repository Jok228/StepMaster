using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StepMaster.Models.Entity;

public struct Region
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }
    [BsonElement("fullName")]
    public string fullName { get; set; }
    public Region(string name, string full)
    {
        this.fullName = $"{full} {name}";
    }
            
            
}