using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StepMaster.Models.Entity
{
    public class Day
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("calories")]
        public float calories { get; set; }
        [BsonElement("distance")]
        public float distance { get; set; }
        [BsonElement("steps")]
        public int steps { get; set; }
        [BsonElement("plancalories")]
        public float plancalories { get; set; }

        [BsonElement("plandistance")]
        public float plandistance { get; set; }

        [BsonElement("plansteps")]
        public int plansteps { get; set; }

        [BsonElement("userid")]
        public string userid { get; set; }
        [BsonElement("date")]
        public string date { get; set; }

        [BsonElement("email")]
        public string email { get; set; }
    }
}
