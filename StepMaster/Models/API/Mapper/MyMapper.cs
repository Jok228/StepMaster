using Amazon.S3.Model.Internal.MarshallTransformations;
using Domain.Entity.Main.Message;
using Domain.Entity.Main.Room;
using StepMaster.Models.API.File;
using StepMaster.Models.API.Messages;

namespace StepMaster.Models.API.Mapper
    {
    public static class MyMapper
        {

       public static FileView MapFileView(MessageFile dbObject)
            {
            return new FileView ()
                {
                Id = dbObject._id,
                Name = dbObject.Name,
                Type = dbObject.Type,
                };
            }
        public static MessageView MapMessageView(Message message,List<FileView> fileView)
            {
            return new MessageView ()
                {
                FileIds = fileView,
                DateSend = message.DateSend,
                OwnEmail = message.OwnEmail,
                RoomId = message.RoomId,
                TextContent = message.TextContent,
                UsersSeen = message.UsersSeen,
                };
            }

        }
   
    }
