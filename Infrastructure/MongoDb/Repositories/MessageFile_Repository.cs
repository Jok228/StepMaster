using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main.Message;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace Infrastructure.MongoDb.Repositories
    {
    public class MessageFile_Repository : IMessageFile_Repository
        {
        private readonly IMongoCollection<MessageFile> _messageFileDb;
        private IMy_Cache _cache;
        private ILogger<Day_Repository> _logger;
        
        private readonly string _cacheKey = "files/";
        public MessageFile_Repository(IMy_Cache cache,IAPIDatabaseSettings settings,IMongoClient mongoClient,ILogger<Day_Repository> logger)
            {
            var database = mongoClient.GetDatabase (settings.DatabaseName);
            _messageFileDb = database.GetCollection<MessageFile> (TableName.Files);
            _cache = cache;
            _logger = logger;
            }
        public async Task<MessageFile> SetFile(MessageFile newFile)
            {
            try
                {
                var count =  _messageFileDb.Count (a => a._id == newFile._id);
                    if (count < 1)
                        {
                        await _messageFileDb.InsertOneAsync (newFile);
                        _cache.SetObject (_cacheKey + newFile._id,newFile,10);
                        return newFile;
                        }
                    else
                        {
                        var filter = Builders<MessageFile>.Filter.Eq ("_id",newFile._id);
                        await _messageFileDb.ReplaceOneAsync (filter,newFile);
                        _cache.DeleteObject (_cacheKey + newFile._id);
                        _cache.SetObject (_cacheKey + newFile._id,newFile,10);
                        return newFile;
                        }                  
               
                }
            catch(Exception ex)
                {                
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
           
            }

        public async Task<MessageFile> GetFile(string id)
            {
            try
                {
                var user = (MessageFile)_cache.GetObject (_cacheKey+id);
                if (user == null)
                    {
                    user = await _messageFileDb.FindAsync (user => user._id == id)
                        .Result
                        .FirstAsync ();
                    return user;
                    }
                else
                    {
                    return user;
                    }
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    throw new HttpRequestException ("404 Not Found",null,HttpStatusCode.NotFound);
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task DeleteFile(string id)
            {
            try
                {
                _cache.DeleteObject (_cacheKey + id);                
                await _messageFileDb.DeleteOneAsync (messageFile => messageFile._id == id);
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }
        }
    }
