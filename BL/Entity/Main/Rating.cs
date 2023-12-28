using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using DnsClient;

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
        public List<Position> ratingUsers { get; set; }
        
        public Rating(string regionId)
        {
            lastUpdate = DateTime.MinValue;
            date = DateTime.UtcNow.Date;
            this.regionId = regionId;
            ratingUsers = new List<Position>();
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
        public string GetUserRanking(string email)
        {
            var filterPlaceInRegion = this.ratingUsers.FirstOrDefault(rating => rating.email == email);  //filter for find index
            int placeInRegion = this.ratingUsers.IndexOf(filterPlaceInRegion) + 1; //find index
            int userRegion = this.ratingUsers.Count(); //all user in list            
            return $"{placeInRegion}/{userRegion}";
        }
        public Position GetUserPosition(string email)
        {
            return this.ratingUsers.Where(position => position.email == email).FirstOrDefault();
        }
        public void DeleteUserPosition(Position position)
        {
            this.ratingUsers.Remove(position);
        }
        public void AddUserPosittion (Position position)
        {
            this.ratingUsers.Add(position);
            this.Sort();
        }
    }
    public struct Position
    {
        [BsonElement("step")]
        public int step { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        public Position( string email)
        {
            this.email = email;
            this.step = 0;
        }
    }
    public struct UserRanking
    {
        [BsonElement("placeInRegion")]
        public string placeInRegion { get; set; }
        [BsonElement("placeInCountry")]
        public string placeInCountry { get; set; }
        public UserRanking(int placeInRegion, int placeInCountry, int allRegion,int allCountry)
        {
            this.placeInRegion = $"{placeInRegion}/{allRegion}";
            this.placeInCountry = $"{placeInCountry}/{allCountry}";

        }

    }
}
