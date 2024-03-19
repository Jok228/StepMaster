namespace StepMaster.Models.API.Room
    {
    public class RoomViewModel
        {
        public string _id {  get; set; }

        public bool IsPublic { get; set; }

        public string Name { get; set; }

        public string LastMessage { get; set; }

        public string FriendEmail { get; set; }

        public int NumberNotViewedMessage { get; set; }

        }
    public class ResponseRooms
        {
        public string GlobalChat { get; set; }

        public string ClanRoom { get; set; }

        public List<RoomViewModel> OtherRoom { get; set; }

        }
    }
