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
        public double calories { get; set; }
        [BsonElement("distance")]
        public double distance { get; set; }
        [BsonElement("steps")]
        public int steps { get; set; }
        [BsonElement("plancalories")]
        public double plancalories { get; set; }

        [BsonElement("plandistance")]
        public double plandistance { get; set; }

        [BsonElement("plansteps")]
        public int plansteps { get; set; }

        [BsonElement("date")]
        public string date { get; set; }

        [BsonElement("email")]
        public string email { get; set; }
    }
}
