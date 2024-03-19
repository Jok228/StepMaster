using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entity.Main.Message
    {
    public class MessageFile:EntityDb
        {
        [BsonElement ("name")]
        public string Name { get; set; }
        [BsonElement ("path")]
        public string Path { get; set; }
        [BsonElement ("type")]
        public string Type { get; set; }
        public MessageFile(IFormFile file)
            {
            int idx = file.FileName.Split ('.')[1].Length+1;


            _id = ObjectId.GenerateNewId().ToString();
            Name = file.FileName.Substring(0,file.FileName.Length - idx);
            Type = '.'+file.FileName.Split('.')[file.Name.Split ('.').Length];
            Path = "StepMaster/MessageFile/" + _id + Type;
            }
        }
    
    }
