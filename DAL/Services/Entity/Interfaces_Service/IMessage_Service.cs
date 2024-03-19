using Application.Repositories.Db.Interfaces_Repository;
using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Interfaces_Service
    {
    public interface IMessage_Service
        {
        #region CRUD
        public Task<Message> GetObjectBy(string idOrEmail);

        public Task<Message> SetObject(Message value);

        public Task<Message> UpdateObject(Message newValue);

        public Task<Message> DeleteObject(string idOrEmail,string emailOwn);
        #endregion
        Task<List<Message>> ViewGetMessagesByRoom(string emailOwn,string roomId);

        Task<List<Message>> GetMessagesByRoom(string idRoom,int page);



        }
    }
