using Domain.Entity.Main.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
    {
    public interface IMessage_Repository:IBase_Operation<Message>
        {
        Task<List<Message>> ViewGetMessageByRoom(string idRoom,string emailOwn);

        Task<List<Message>> GetMessageByRoom(string idRoom,int page);

        Task<List<Message>> GetMessageWithFile(string fileId);
        }
    }
