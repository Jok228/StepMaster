using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using StepMaster.Models.Entity;

namespace Domain.Entity.Main
{
    public class Day
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("calories")]
        public double? calories { get; set; }
        [BsonElement("distance")]
        public double? distance { get; set; }
        [BsonElement("steps")]
        public int? steps { get; set; }
        [BsonElement("plancalories")]
        public double? plancalories { get; set; }

        [BsonElement("plandistance")]
        public double? plandistance { get; set; }

        [BsonElement("plansteps")]
        public int? plansteps { get; set; }

        [BsonElement("date")]
        public DateTime date { get; set; }

        [BsonElement("email")]
        public string? email { get; set; }
        public Day UpdateDay(Day newValue)
        {
            if (newValue.plansteps != null) this.plansteps = newValue.plansteps;
            if(newValue.plandistance != null) this.plandistance = newValue.plandistance;
            if(newValue.plancalories != null) this.plancalories = newValue.plancalories;
            if(newValue.calories != null) this.calories = newValue.calories;
            if(newValue.distance != null) this.distance = newValue.distance;
            if (newValue.steps != null) this.steps = newValue.steps;
            return this;
        }
    }
}
