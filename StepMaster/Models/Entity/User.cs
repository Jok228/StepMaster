using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StepMaster.Models.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("login")]
        public string login { get; set; }
        [BsonElement("role")]
        public string? role { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("region_id")]
        public string? region_id { get; set; }
    }
}
