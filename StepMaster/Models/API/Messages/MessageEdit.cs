using Domain.Entity.Main.Room;

namespace StepMaster.Models.API.Messages
    {
    public class MessageEdit
        {
        public string _id { get; set; }
        public string RoomId { get; set; }

        public string TextContent { get; set; }

        public DateTime DateSend { get; set; }

        public List<string> UsersSeen { get; set; }

        public List<string> FileIds { get; set; }

        public string OwnEmail { get; set; }

        public Message ConvertToMessage(string email)
            {
            return new Message ()
                {
                OwnEmail = email,
                FileIds = this.FileIds,
                UsersSeen = this.UsersSeen,
                DateSend = this.DateSend,
                TextContent = this.TextContent,
                RoomId = this.RoomId,
                _id = this._id
                };
            }

        }
    }
