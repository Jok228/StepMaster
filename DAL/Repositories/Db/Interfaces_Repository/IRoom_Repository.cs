using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
    {
    public interface IRoom_Repository
        {
        Task<Room> GetGlobalRoom();
        Task<Room> GetRoom(string id);

        Task<Room> GetRoomTo(string emailOne,string emailTwo,bool isPublic = false);

        Task DeleteRoom(string id);

        Task<Room> SetRoom(Room newRoom);

        Task<Room> UpdateRoom(Room room);

        Task<List<Room>> GetRoomsByEmail(string email,bool isPublic = false);

        }
    }
