using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Realization_Services
    {
    public class Room_Service : IRoom_Service
        {
        private readonly IUser_Repository _usersRepository;
        private readonly IRoom_Repository _roomRepository;
        private readonly IMessage_Repository _messageRepository;
        private readonly IClan_Repository _clanRepository;
        private readonly IClan_Service _clanService;
        public Room_Service(IUser_Repository usersRepository,IRoom_Repository roomService,IMessage_Repository messageRepository,IClan_Repository clanRepository,IClan_Service clanService)
            {
            _usersRepository = usersRepository;
            _roomRepository = roomService;
            _messageRepository = messageRepository;
            _clanRepository = clanRepository;
            _clanService = clanService;
            }

        public async Task<List<Room>> GetMyRooms(string email)
            {
            var rooms = await _roomRepository.GetRoomsByEmail (email); 
            return rooms;        
            }

        public async Task<Room> GetRoomTo(string emailOne, string emailTwo)
            {
            var userOne = await _usersRepository.GetObjectBy(emailOne);
            var userTwo = await _usersRepository.GetObjectBy(emailTwo);
            if (userOne.BlockedUsers.Contains (emailTwo) || userTwo.BlockedUsers.Contains (emailOne))
                {
                throw new HttpRequestException ("You or the user is located in blocked list each other",null,HttpStatusCode.BadRequest);
                }
            var response = await _roomRepository.GetRoomTo (emailOne,emailTwo);            
            return response;
            }
        }
    }
