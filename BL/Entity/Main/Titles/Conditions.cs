using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main.Titles
{
    public class Condition:EntityDb
    {
        [BsonElement("groupId")]
        public int groupId {  get; set; }
        [BsonElement("type")]
        public string type { get; set; }
        [BsonElement("distance")]
        public int distance { get; set; }
        [BsonElement("timeDay")]
        public int? timeDay { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("id")]
        public int localId { get; set; }
        [BsonElement("aws_path")]
        public string aws_path { get; set; }
    }

    
}
