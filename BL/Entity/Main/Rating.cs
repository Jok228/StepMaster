using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using DnsClient;
using Domain.Entity.Main;

namespace StepMaster.Models.Entity
{
    public class Rating:ClassWithPosition
    {       
        [BsonElement("date")]
        public DateTime date { get; set; }
        [BsonElement("lastUpdate")]
        public DateTime lastUpdate { get; set; }

        [BsonElement("regionId")]
        public string regionId { get; set; }
        
        public Rating(string regionId)
        {
            lastUpdate = DateTime.MinValue;
            date = DateTime.UtcNow.Date;
            this.regionId = regionId;
            RatingUsers = new List<Position>();
        }
        override public Rating Sort()
        {   
                this.RatingUsers = this.RatingUsers // sort
            .OrderBy(rating => rating.Step)
            .Reverse()
            .ToList();//Create rating but for userDB,
                this.lastUpdate = DateTime.UtcNow;
          return this;
        }        
    }   
    public struct UserRanking
    {
        [BsonElement("placeInRegion")]
        public string placeInRegion { get; set; }
        [BsonElement("placeInCountry")]
        public string placeInCountry { get; set; }
        [BsonElement("placeInClan")]
        public string? placeInClan { get; set; }
        public UserRanking(int placeInRegion, int placeInCountry, int allRegion,int allCountry, int? placeInClan = null,int? allClan = null)
        {
            this.placeInRegion = $"{placeInRegion}/{allRegion}";
            this.placeInCountry = $"{placeInCountry}/{allCountry}";
            this.placeInClan = $"{placeInClan}/{allClan}";

        }

    }
}
