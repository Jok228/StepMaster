using Amazon.Runtime.Internal.Util;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main;
using Domain.Entity.Main.Room;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Repositories
    {
    public class Message_Repository : IMessage_Repository
        {
        private readonly IMongoCollection<Message> _messageDb;
        private IMy_Cache _cache;
        private ILogger<User_Repository> _logger;
        private readonly string _cacheKey = "message";
        private readonly int _limit = 20;
        public Message_Repository(IMy_Cache cache,IAPIDatabaseSettings settings,IMongoClient mongoClient,ILogger<User_Repository> logger)
            {
            var database = mongoClient.GetDatabase (settings.DatabaseName);
            _messageDb = database.GetCollection<Message> (TableName.Message);
            _cache = cache;
            _logger = logger;
            }
        public async Task<Message> DeleteObject(string idOrEmail)
            {
            try
                {
                _cache.DeleteObject (_cacheKey + idOrEmail);
                var response = await _messageDb.FindAsync (message => message._id == idOrEmail).Result.FirstAsync();
                await _messageDb.DeleteOneAsync (message => message._id == idOrEmail);
                return response;
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }

            }

        public async Task<Message> GetObjectBy(string idOrEmail)
            {
            try
                {
                var message = (Message)_cache.GetObject (idOrEmail);
                if (message == null)
                    {
                    message = await _messageDb.FindAsync (message => message._id == idOrEmail)
                        .Result
                        .FirstAsync ();
                    return message;
                    }
                else
                    {
                    return message;
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

        public async Task<Message> SetObject(Message value)
            {
            try
                {
                await _messageDb.InsertOneAsync (value);
                return value;
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Message> UpdateObject(Message newValue)
            {
            try
                {
                var filter = Builders<Message>.Filter.Eq ("_id",newValue._id);
                await _messageDb.ReplaceOneAsync (filter,newValue);
                _cache.DeleteObject (newValue._id);
                _cache.SetObject (newValue._id,newValue,10);
                return newValue;
                }
            catch (Exception ex)
                {
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<List<Message>> ViewGetMessageByRoom(string idRoom,string emailOwn)
            {
            try
                {
                    var message = await _messageDb.FindAsync (message => message.RoomId == idRoom 
                    && !message.UsersSeen.Contains(emailOwn) 
                    && message.OwnEmail != emailOwn) 
                        .Result
                        .ToListAsync ();
                    return message;
                    }                
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    return new List<Message>();
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }
            }

        public async Task<List<Message>> GetMessageByRoom(string idRoom,int page)
            {
            var skip = page * _limit;
            var aggregateOne = new BsonDocument ()
            {
                {
                    "$match", new BsonDocument()
                    {
                        {
                            "room_id",idRoom
                        }
                    }

                }
            };
            var aggregateTwo = new BsonDocument ()
                {
                    {
                    "$sort",new BsonDocument ()
                        {
                            {
                            "date_send",-1
                            }
                        }
                    }
                };
            var aggregateThree = new BsonDocument ()
            {
                {
                    "$skip", skip
                }
            };
            var aggreagetFour = new BsonDocument ()
            {
                {
                    "$limit", _limit
                }
            };
            var pipeline = new BsonDocument[] { aggregateOne,aggregateTwo,aggregateThree,aggreagetFour };            
            try
                {
                return await _messageDb.Aggregate<Message>(pipeline).ToListAsync ();
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                     return new List<Message> ();
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }


            }

        public async Task<List<Message>> GetMessageWithFile(string fileId)
            {
            try
                {
                var messages = await _messageDb.FindAsync (message => message.FileIds.Contains (fileId))
                    .Result
                    .ToListAsync();
                return messages;
                }
            catch (Exception ex)
                {
                if (ex.Message == DbExMessage.NoElements)
                    {
                    return new List<Message> ();
                    }
                _logger.LogError (ex.Message);
                throw new HttpRequestException ("500 Shit happens",null,HttpStatusCode.InternalServerError);
                }

            }
        }
    }
