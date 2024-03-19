using Microsoft.CodeAnalysis.Operations;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main.Room
    {
    public class Message:EntityDb
        {
        [BsonElement ("room_id")]
        public string RoomId { get; set; }
        [BsonElement ("text_content")]
        public string TextContent { get; set; }
        [BsonElement ("date_send")]
        public DateTime DateSend { get; set; }
        [BsonElement ("users_seen")]
        public  List<string> UsersSeen {  get; set; }
        [BsonElement ("file_ids")]
        public List<string> FileIds { get; set; }
        [BsonElement ("own_email")]
        public string OwnEmail { get; set; }
        }
    }
