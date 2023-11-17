using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StepMaster.Models.Entity
{
    public class Body
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }        

        [BsonElement("userid")]
        public string userid { get; set; }        

        [BsonElement("email")]
        public string email { get; set; }

        //[BsonElement("weight")]
        //public float weight { get; set; }

        //[BsonElement("height")]
        //public float height { get; set; }

        //[BsonElement("fat")]
        //public float fat { get; set; }

        //[BsonElement("muscles")]
        //public float muscles { get; set; }
    }
}
