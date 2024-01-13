using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepMaster.Models.Entity.User;

namespace Domain.Entity.Main.Titles
{
    public class Condition:EntityDb
    {
        [BsonElement("groupId")]
        public int GroupId {  get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("distance")]
        public int? Distance { get; set; }
        [BsonElement("timeDay")]
        public int? TimeDay { get; set; }
        [BsonElement("group_name")]
        public string GroupName { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("id")]
        public int IdLocal { get; set; }
        [BsonElement("aws_path")]
        public string AwsPath { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonIgnore]
        public string Dealt_Progress { get; set; }
        [BsonIgnore]
        public string Needed_Progress { get; set; }
        [BsonIgnore]
        public string awsLink { get; set; }       

    }

    
}
