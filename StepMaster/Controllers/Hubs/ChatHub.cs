using API.Auth.AuthCookie;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.Main.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using StepMaster.Models.API.Messages;
using StepMaster.Models.Entity;

namespace StepMaster.Controllers.Hubs
    {
    public sealed class ChatHub : Hub
        {
        private readonly IMessage_Service _message_service;
        private readonly IRoom_Service _room_service;
        private readonly IRoom_Repository _roomReository;
        public ChatHub(IRoom_Service room_service,IMessage_Service message_service,IRoom_Repository roomReository)
            {
            _room_service = room_service;
            _message_service = message_service;
            _roomReository = roomReository;
            }

        static IList<UserConnection> _signalRUsers = new List<UserConnection> ();
        [CustomAuthorizeUser ("all")]
        public override async Task OnConnectedAsync()

            {
            bool isAuntificated = Context.User.Identity.IsAuthenticated;
            if (!isAuntificated)
                {
                Context.Abort ();
                }
            else
                {
                _signalRUsers.Add (new UserConnection ()
                    {
                    ConnectionId = Context.ConnectionId,
                    Email = Context.User.Identity.Name
                    });
                }
            }
        public override async Task OnDisconnectedAsync(Exception? exception)
            {
            var user = _signalRUsers.FirstOrDefault (a => a.ConnectionId == Context.ConnectionId);

            if (user != null)
                {
                _signalRUsers.Remove (_signalRUsers.Where (a => a.ConnectionId == Context.ConnectionId).FirstOrDefault ());
                }
            await base.OnDisconnectedAsync (exception);
            }
        public async Task EditMessage(MessageEdit messageView)
            {
            var email = Context.User.Identity.Name;
            var roomId = messageView.RoomId;
            var message = messageView.ConvertToMessage (email);
            await _message_service.UpdateObject (message);
            await SendMessages (message,roomId,email,"update message");
            }
        public async Task DeleteMessage (string id)
            {
            var email = Context.User.Identity.Name;
            var message = await _message_service.GetObjectBy (id);
            var roomId = message.RoomId;
            await _message_service.DeleteObject (id,email);
            await SendMessages (message,roomId,email,"delete message");
            }
        public async Task SendMessageToUser(MessageSendModel message)
            {
            var email = Context.User.Identity.Name;
            var newMessage = message.ConvertToMessage (email);
            var roomId = newMessage.RoomId;
            var emailSender = newMessage.OwnEmail;
            await _message_service.SetObject (newMessage);
            await SendMessages (newMessage,roomId,emailSender,"new message");

            }
        
        private async Task SendMessages(Message message,string roomId,string emailSender,string messageType)
            {
            var room = await _roomReository.GetRoom (roomId);
            foreach (var user in room.Users)
                {
                foreach (var userSignalR in _signalRUsers)
                    {
                    if (userSignalR.Email == user & userSignalR.Email != emailSender)
                        {
                        await Clients.Client (userSignalR.ConnectionId).SendAsync (messageType,message);
                        }
                    }
                }
            }
        public class UserConnection
            {
            public string Email { get; set; }
            public string ConnectionId { get; set; }
            }




        }
    }
