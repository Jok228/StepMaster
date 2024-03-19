using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.Main;
using Domain.Entity.Main.Room;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Main.ClassWithPosition;

namespace Application.Services.Entity.Realization_Services
{
    public class Clan_Service : IClan_Service
    {
        private readonly IUser_Repository _userRepository;
        private readonly IDay_Repository _daysRepository;
        private readonly IClan_Repository _clansRepository;
        private readonly IRoom_Repository _roomsRepository;
        private readonly IMessage_Repository _messageRepository;
        private readonly IMessageFile_Repository _messageFileRepository;
        private readonly IMessageFile_Service _messageFileService;
        public Clan_Service(IDay_Repository days,IClan_Repository clansRepository,IUser_Repository userRepository,IRoom_Repository roomsRepository,IMessage_Repository messageRepository,IMessageFile_Repository messageFileRepository,IMessageFile_Service messageFileService)
            {
            _daysRepository = days;
            _clansRepository = clansRepository;
            _userRepository = userRepository;
            _roomsRepository = roomsRepository;
            _messageRepository = messageRepository;
            _messageFileRepository = messageFileRepository;
            _messageFileService = messageFileService;
            }

        public async Task<Clan> AddUser(string mongoIdClan, string email)
        {
            var oldClan = await _clansRepository.CheckClansByUser(email);
            if(oldClan)
            {
                throw new HttpRequestException("The user belongs to another clan", null, HttpStatusCode.Conflict);
            }
            var user = await _userRepository.GetObjectBy(email);
            var position = new Position()
            {
                NickName= user.NickName,
                Email = email,
                Step = await _daysRepository.GetAllStepsUser(email, null)
            };
            
            var editClan = await _clansRepository.GetObjectBy(mongoIdClan);
            editClan.AddUserPosittion(position);

            #region Room logic
            var roomClan = await _roomsRepository.GetRoom (editClan.RoomId);
            roomClan.AddUser(email);
            await _roomsRepository.UpdateRoom(roomClan);
            #endregion

            return await _clansRepository.UpdateObject(editClan);
           
        }
        public async  Task<Clan> LeaveUser(string mongoIdClan, string email)
        {
            var editClan = await _clansRepository.GetObjectBy(mongoIdClan);
            if(email == editClan.OwnerUserEmail)
            {
                editClan.OwnerUserEmail = editClan.RatingUsers.FirstOrDefault().Email;
            }
            var targetPosition = editClan.GetUserPosition(email);
            editClan.DeleteUserPosition(targetPosition);
            

            if (editClan.RatingUsers.Count == 0)
            {
                await DeleteClan(mongoIdClan);
                return null;
            }
            #region Room logic
            var roomClan = await _roomsRepository.GetRoom (editClan.RoomId);
            roomClan.RemoveUser (email);
            await _roomsRepository.UpdateRoom (roomClan);
            #endregion


            return await _clansRepository.UpdateObject(editClan);

        }

        public async Task<List<ClanLite>> GetAllClansBySortType(string nameSearch,Clan.SortType type, string regionName, int numberPage = 0)
        {
            var result = new List<ClanLite>();
            if (type == Clan.SortType.MaxToMin)
            {
                result = await _clansRepository.GetClansMaxToMin(nameSearch, numberPage);
            }
            else if (type == Clan.SortType.MinToMax)
            {
                result = await _clansRepository.GetClansMinToMax(nameSearch, numberPage);
            }
            else if (type == Clan.SortType.MyRegion)
            {
                result = await _clansRepository.GetClansMyRegion(nameSearch, regionName, numberPage);
            }
            else if (type == Clan.SortType.NumsFreePlace)
            {
                result = await _clansRepository.GetClansByFreePlace(nameSearch, numberPage);
            }
            else
            {
                throw new HttpRequestException("Bad code type. Type is must be 0,1,2 or 3",null,HttpStatusCode.BadRequest);
            }
            foreach(var clan in result)
            {
                clan.NumberOfAllPlace = int.Parse( _clansRepository.GetNumberOfPlace(clan._id).Result.Split('/')[0]);
            }
            return result;
        }

       

        public async Task<Clan> SetNewClan(Clan newClan)
        {
            var checkClan = await _clansRepository.GetClanOrNullByName(newClan.Name, newClan.RegionName);
            if(checkClan != null)
            {
                throw new HttpRequestException("The clan with too name and region already exist in Db ", null, HttpStatusCode.Conflict);
            }

            #region Create room
            var room = new Room ()
                {
                IsPublic = true,
                Messages = new List<string>(),
                Name = newClan.Name,
                Users = new List<string> ()
                    {
                    newClan.OwnerUserEmail,
                    },
                };

            room = await _roomsRepository.SetRoom(room);
            newClan.RoomId = room._id;
            #endregion

            var result = await _clansRepository.SetObject (newClan);

            await _clansRepository.UpdateRatingClans();
            return result;
        }

        public async Task<bool> CheckOwner(string mongoIdClan, string email)
        {
            var clan = await _clansRepository.GetObjectBy(mongoIdClan);
            if(email == clan.OwnerUserEmail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteClan(string mongoIdClan)
        {
            var clan = await _clansRepository.GetClansById(mongoIdClan);

            #region Room logic
            var roomClan = await _roomsRepository.GetRoom (clan.RoomId);
            foreach(var message in roomClan.Messages)
                {
                var messageFull = await _messageRepository.GetObjectBy (message);
                foreach(var file in messageFull.FileIds)
                    {
                    await _messageFileService.DeleteFile(file);
                    }
                await _messageRepository.DeleteObject(message);
                }
            await _roomsRepository.DeleteRoom (roomClan._id);
            #endregion

            await _clansRepository.DeleteObject(mongoIdClan);
            
            await _clansRepository.UpdateRatingClans();
        }
        public async Task UpdateStepsInClanByUser(string email)
        {
            if (!await _clansRepository.CheckClansByUser(email))
            {
                return;
            }
            var clan = await _clansRepository.GetClanByUser(email);
            var user = await  _userRepository.GetObjectBy(email);
            var newPosition = new Position()
            {
                NickName = user.NickName,
                Email = email,
                Step = await _daysRepository.GetAllStepsUser(email, null)
            };
            
                clan.UpdatePosition(newPosition);
                await _clansRepository.UpdateObject(clan);
            
        }

        public async Task<Clan> GetClan(string mongoId)
        {
            var clan = await _clansRepository.GetObjectBy(mongoId);
            clan.NumberOfRegionPlace = await _clansRepository.GetNumberOfPlaceRegion(clan.RegionName, clan._id);
            clan.NumberOfAllPlace = await _clansRepository.GetNumberOfPlace(clan._id);
            return clan;
        }
      
    }
}
