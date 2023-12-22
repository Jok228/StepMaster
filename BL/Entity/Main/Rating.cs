using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace StepMaster.Models.Entity
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("date")]
        public DateTime date { get; set; }
        [BsonElement("lastUpdate")]
        public DateTime lastUpdate { get; set; }

        [BsonElement("regionId")]
        public string regionId { get; set; }
        [BsonElement("ratingUsers")]
        public List<UserRating> ratingUsers { get; set; }
        
        public Rating(string regionId)
        {
            lastUpdate = DateTime.MinValue;
            date = DateTime.Now.Date;
            this.regionId = regionId;
            ratingUsers = new List<UserRating>();
        }
        public Rating Sort()
        {     
            
            
                this.ratingUsers = this.ratingUsers // sort
            .OrderBy(rating => rating.step)
            .Reverse()
            .ToList();//Create rating but for userDB,
                this.lastUpdate = DateTime.UtcNow;
            
          return this;
        }

    }
    public struct UserRating
    {
        [BsonElement("step")]
        public int step { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        public UserRating( string email, string name)
        {
            this.email = email;
            this.name = name;
            this.step = 0;
        }
    }
    public struct PlaceUserOnRating
    {
        [BsonElement("placeInRegion")]
        public string placeInRegion { get; set; }
        [BsonElement("placeInCountry")]
        public string placeInCountry { get; set; }
        public PlaceUserOnRating(int placeInRegion, int placeInCountry, int allRegion,int allCountry)
        {
            this.placeInRegion = $"{placeInRegion}/{allRegion}";
            this.placeInCountry = $"{placeInCountry}/{allCountry}";

        }

    }
}
