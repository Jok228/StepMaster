using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Interfaces_Service
    {
    public interface IRoom_Service
        {

        public Task<List<Room>> GetMyRooms(string email);

        public Task<Room> GetRoomTo(string emailOne,string emailTwo);

        }
    }
