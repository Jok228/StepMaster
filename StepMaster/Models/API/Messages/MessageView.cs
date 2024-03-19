using MongoDB.Bson.Serialization.Attributes;
using StepMaster.Models.API.File;

namespace StepMaster.Models.API.Messages
    {
    public class MessageView
        {       
        public string RoomId { get; set; }
        public string TextContent { get; set; }
        public DateTime DateSend { get; set; }
        public List<string> UsersSeen { get; set; }
        public List<FileView> FileIds { get; set; }
        public string OwnEmail { get; set; }
        }
    }
    
