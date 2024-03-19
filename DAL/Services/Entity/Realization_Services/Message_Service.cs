using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.FIreBase.Helpers;
using Application.Services.FIreBase.Interfaces;
using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.FIreBase.Helpers.FireBaseWriteMessages;

namespace Application.Services.Entity.Realization_Services
    {
    public class Message_Service : IMessage_Service
        {

        private readonly IMessage_Repository _messageRepository;
        private readonly IRoom_Repository _roomRepository;
        private readonly IUser_Repository _userRepository;
        private readonly IFireBase_Service _fireBaseService;
        private readonly IMessageFile_Service _messageFileService;

        public Message_Service(IMessage_Repository messageRepository,IRoom_Repository roomRepository,IUser_Repository userRepository,IFireBase_Service fireBaseService,IMessageFile_Service messageFileService = null)
            {
            _messageRepository = messageRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _fireBaseService = fireBaseService;
            _messageFileService = messageFileService;
            }

        #region CRUD
        public async Task<Domain.Entity.Main.Room.Message> DeleteObject(string idOrEmail,string emailOwn)
            {
            var message = await _messageRepository.GetObjectBy (idOrEmail);
            
            if (message.OwnEmail != emailOwn)
                {
                throw new HttpRequestException ("You not own this message",null,HttpStatusCode.Forbidden);
                }
            foreach (var file in message.FileIds)
                {
                await _messageFileService.DeleteFile (file);
                }
            return await _messageRepository.DeleteObject (idOrEmail);
            }

        public async Task<Message> GetObjectBy(string idOrEmail)
            {
            return await _messageRepository.GetObjectBy (idOrEmail);
            }

        public async Task<Message> SetObject(Message value)
            {
            var room = await _roomRepository.GetRoom (value.RoomId);
            

            foreach (var userEmail in room.Users)
                {
                if (value.OwnEmail != userEmail)
                    {
                    var targetUser = await _userRepository.GetObjectBy (userEmail);
                    var message = new TextMessage (new MessageParam
                        {
                        Email = userEmail,
                        TypePush = "2",
                        Text = value.OwnEmail
                        }).message;
                    if (targetUser.FireBaseToken != null)
                        {
                        await _fireBaseService.SendMessage (targetUser.FireBaseToken, message);
                        }
                    }
                }
            var newMessage = await _messageRepository.SetObject (value);
            room.AddMessage (newMessage._id);
            await _roomRepository.UpdateRoom (room);
            return newMessage;
            }

        public async Task<Message> UpdateObject(Message newValue)
            {
            var oldMessage = await _messageRepository.GetObjectBy(newValue._id);
            foreach(var oldFile in oldMessage.FileIds)
                {
                if (!newValue.FileIds.Contains (oldFile))
                    {
                    await _messageFileService.DeleteFile (oldFile);
                    }
                }

            return await _messageRepository.UpdateObject (newValue);
            }
        #endregion

        public async Task<List<Message>> ViewGetMessagesByRoom(string emailOwn,string roomId)
            {
            var listMessages = await _messageRepository.ViewGetMessageByRoom (roomId,emailOwn);
            listMessages.OrderBy (r => r.DateSend);
            return listMessages;
            }
        public async Task<List<Message>> GetMessagesByRoom(string roomId,int page)
            {
            return await _messageRepository.GetMessageByRoom (roomId,page);
            }

        }
    }
