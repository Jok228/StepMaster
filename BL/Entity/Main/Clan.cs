using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Main.ClassWithPosition;

namespace Domain.Entity.Main
{
    public class Clan:ClassWithPosition
    {
        
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("region_name")]
        public string RegionName { get; set; }
        [BsonElement("max_users")]
        public int MaxUsers { get; set; }
        [BsonElement("owner_user_email")]
        public string OwnerUserEmail { get; set; }

        [BsonElement("data_create")]
        public DateTime DataCreate { get; set; }
        [BsonIgnore]
        public string NumberOfAllPlace { get; set; }
        [BsonIgnore]
        public string NumberOfRegionPlace { get; set; }
        public Clan()
        {
            RatingUsers = new List<Position>();
        }
        public enum SortType 
        {
          MaxToMin = 0,
          MinToMax = 1,
          MyRegion = 2,
          NumsFreePlace = 3,        
        }
    }
    public class ClanLite:EntityDb
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("region_name")]
        public string RegionName { get; set; }
        [BsonElement("count_users")]
        public int CountUsers { get; set; }
        public int NumberOfAllPlace { get; set; }

    }
}
