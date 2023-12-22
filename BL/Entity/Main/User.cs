using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Globalization;
using MongoDB.Driver;

namespace StepMaster.Models.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("nickname")]
        public string nickname { get; set; }
        [BsonElement("fullname")]
        public string fullname { get; set; }
        [BsonElement("role")]
        public string? role { get; set; }
        [BsonElement("rating")]
       
        public PlaceUserOnRating? rating { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("region_id")]
        public string region_id { get; set; }
        [BsonElement("gender")]
        public string gender { get; set; }
        [BsonElement("lastCookie")]
        public string? lastCookie { get; set; }

        [BsonIgnore]
        public string? AvatarLink { get; set; }

        public User UpdateUser( User newValue)
        {
            if(newValue.fullname != null) this.fullname = newValue.fullname;
            if(newValue.nickname != null) this.nickname = newValue.nickname;
            if(newValue.region_id != null) this.region_id = newValue.region_id;
            return this;
        }
    }
    
}
