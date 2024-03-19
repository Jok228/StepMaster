using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.Main.Message;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Realization_Services
    {
    public class MessageFile_Service : IMessageFile_Service
        {
        private readonly IMessage_Repository _messageRepository;
        private readonly IMessageFile_Repository _messageFileRepository;
        private readonly IAws_Repository _awsRepository;


        public MessageFile_Service(IMessageFile_Repository messageFileRepository,IMessage_Repository messageRepository,IAws_Repository awsRepository)
            {
            _messageFileRepository = messageFileRepository;
            _messageRepository = messageRepository;
            _awsRepository = awsRepository;
            }

        public async  Task DeleteFile(string fileId)
            {
            var file = await _messageFileRepository.GetFile(fileId);
            await _awsRepository.DeleteFile (file);
            await _messageFileRepository.DeleteFile (fileId);            
            }

        public async Task<MessageFile> GetFile(string id)
            {
            var response = await _messageFileRepository.GetFile (id);
            return response;
            }

        public async Task<MessageFile> SetNewFile(MessageFile file)
            {
            var response = await _messageFileRepository.SetFile(file);           
            return response;
            }
        }
    }
