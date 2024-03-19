using Domain.Entity.Main.Room;
using System.Net;

namespace StepMaster.Models.API.Rooms
    {
    public class RoomCreate
        {
        public List<string> Users { get; set; }


        public Domain.Entity.Main.Room.Room ConverToRoom()
            {
            if (Users.Count < 2)
                {
                throw new HttpRequestException ("The count list users less 2",null,HttpStatusCode.InternalServerError);
                }
            return new Domain.Entity.Main.Room.Room ()
                {
                IsPublic = false,
                Messages = new List<string> (),
                Name = "NaDvoix",
                Users = this.Users,
                };
            }

        }
    }
