using Domain.Entity.Main.Room;

namespace StepMaster.Models.API.Messages
    {
    public class MessageSendModel
        {
        public string RoomId { get; set; }

        public string TextContent { get; set; }

        public List<string>? FileIds { get; set; }
        
        public Message ConvertToMessage(string emailOwn)
            {
            return new Message ()
                {
                RoomId = this.RoomId,
                TextContent = this.TextContent,
                OwnEmail = emailOwn,
                FileIds = this.FileIds,
                DateSend = DateTime.UtcNow,
                UsersSeen = new List<string> (),
                };
            }

        }
    }
