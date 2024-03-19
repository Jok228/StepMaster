using Application.Repositories.Db.Interfaces_Repository;
using Domain.Entity.Main.Message;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Interfaces_Service
    {
    public interface IMessageFile_Service
        {
        Task DeleteFile(string id);
        Task<MessageFile> GetFile(string id);
        Task<MessageFile> SetNewFile(MessageFile file);

        }
    }
