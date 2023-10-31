using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StepMaster.Models.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("login")]
        public string login { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
    }
}
