using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main
{
    abstract public class EntityDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id {  get; set; }
    }
}
