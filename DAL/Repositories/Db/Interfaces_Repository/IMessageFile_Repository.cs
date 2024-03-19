using Domain.Entity.Main.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
    {
    public interface IMessageFile_Repository
        {
        public Task DeleteFile(string id);

        public Task<MessageFile> GetFile(string id);

        public Task<MessageFile> SetFile(MessageFile newFile);

        }
    }
