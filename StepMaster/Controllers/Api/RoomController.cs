using Amazon.Runtime.Internal;
using API.Auth.AuthCookie;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.Entity.Realization_Services;
using Domain.Entity.API;
using Domain.Entity.Main.Message;
using Domain.Entity.Main.Room;
using Infrastructure.MongoDb.Repositories;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.API.File;
using StepMaster.Models.API.Mapper;
using StepMaster.Models.API.Messages;
using StepMaster.Models.API.Room;
using StepMaster.Models.API.Rooms;
using StepMaster.Models.Entity;
using StepMaster.Services.ForDb.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace StepMaster.Controllers.api
    {

    [Route ("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
        {
        private readonly IRoom_Service _roomService;
        private readonly IRoom_Repository _roomRepository;
        private readonly IMessage_Service _messageService;
        private readonly IClan_Repository _clanRepository;
        private readonly IUser_Service _userService;
        private readonly IMessageFile_Service _messageFileService;
        private readonly IAws_Repository _awsRepository;

        public RoomController(IMessage_Service messageService,IRoom_Service roomService,IRoom_Repository roomRepository,IUser_Service user_Service,IClan_Repository clanRepository,IMessageFile_Service messageFileService,IAws_Repository awsRepository)
            {
            _roomRepository = roomRepository;
            _messageService = messageService;
            _roomService = roomService;
            _userService = user_Service;
            _clanRepository = clanRepository;
            _messageFileService = messageFileService;
            _awsRepository = awsRepository;
            }

        [HttpGet]
        [CustomAuthorizeUser ("all")]
        [Route ("GetMyRoomsView")]
        public async Task<ResponseRooms> GetMyRoomsView()
            {
            var email = User.Identity.Name;
            var clan = await _clanRepository.GetClanByUser (email);

            var rooms = await _roomService.GetMyRooms (email);
            var listViewRoom = new List<RoomViewModel>();
            foreach (var room in rooms)
                {
                var lastMessages = await _messageService.ViewGetMessagesByRoom (email,room._id);
                var lastText = string.Empty;
                if(lastMessages.Count > 0)
                    {
                    lastText = lastMessages.LastOrDefault ().TextContent;
                    }
                var roomViewModel = new RoomViewModel
                    {
                    IsPublic = room.IsPublic,
                    Name = room.Name,
                    _id = room._id,
                    FriendEmail = room.Users.Find (name => name != email)??"undefind",
                    LastMessage = lastText,
                    NumberNotViewedMessage = lastMessages.Count,
                    };
                listViewRoom.Add (roomViewModel);                
                }
            var response = new ResponseRooms ()
                {
                GlobalChat = _roomRepository.GetGlobalRoom().Result._id,
                ClanRoom = clan.RoomId,
                };
            response.OtherRoom = listViewRoom;
            return response;
            }        
        [HttpGet]
        [CustomAuthorizeUser ("all")]
        [Route ("GetMessagesByRoom")]
        public async Task<ResponseList<MessageView>> GetMessagesByRoom(string roomId,int page)
            {
            List<Message> messages = await _messageService.GetMessagesByRoom (roomId,page);
            List<MessageView> result = new List<MessageView> ();
            foreach(var mes in messages)
                {
                var listFile = new List<FileView> ();
                foreach(var file in mes.FileIds)
                    {
                    MessageFile fileDb = await _messageFileService.GetFile (file);
                    FileView fileView = MyMapper.MapFileView (fileDb);
                    fileView.Link = await _awsRepository.GetLink (fileDb.Path);
                    listFile.Add (fileView);
                    }

                MessageView messageView = MyMapper.MapMessageView (mes,listFile);
                result.Add (messageView);
                }
            
            return new ResponseList<MessageView> (result);
            }
        [HttpPost]
        [CustomAuthorizeUser ("all")]
        [Route ("CreateRoom")]
        public async Task<Room> GetRoom([FromForm] string emailTarget)
            {
            var email = User.Identity.Name;
            if (email == emailTarget)
                {
                throw new HttpRequestException ("You do not create chat with yourself",null,HttpStatusCode.BadRequest);
                }
            return await _roomService.GetRoomTo (email,emailTarget);      
            }

        }             
        
    
    }
