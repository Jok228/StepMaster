using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

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
    }
    public class UserResponse
    {       
        
        public string email { get; set; }
       
        public string nickname { get; set; }
     
        public string fullname { get; set; }
        
        public string? role { get; set; }

        
        public PlaceUserOnRating? rating { get; set; }


        public string region_id { get; set; }
        
        public string gender { get; set; }
       
        public string? avatarLink { get; set; }
        public UserResponse(User user)
        {
            this.rating = user.rating;
            this.email = user.email;
            this.nickname = user.nickname;
            this.fullname = user.fullname;
            this.role = user.role;  
            this.gender = user.gender;            
            this.region_id = user.region_id;
        }
    }
}
