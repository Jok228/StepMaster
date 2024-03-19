using Amazon.Runtime.Internal.Util;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.Cache.Implementation;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.ForDb.APIDatebaseSet;
using Infrastructure.MongoDb.Settings;

using Infrastructure.MongoDb.DbHelper;
using Application.Repositories.Db.Interfaces_Repository;
using System.Net;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Infrastructure.MongoDb.Repositories
{
    public class User_Repository : IUser_Repository
    {
        private readonly IMongoCollection<User> _usersDb;
        private IMy_Cache _cache;
        private ILogger<User_Repository> _logger;
        private int _limit = 16;
        public User_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient, ILogger<User_Repository> logger)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _usersDb = database.GetCollection<User>(TableName.User);
            _cache = cache;
            _logger = logger;
        }

        public async Task<User> GetByCookie(string cookie)
        {
            try
            {
                var user = await _usersDb.FindAsync(user => user.LastCookie == cookie)
                    .Result
                    .FirstAsync();
                return user;
            }
            catch (Exception ex) 
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null,HttpStatusCode.InternalServerError);

            }
        }
        public async Task<List<User>> GetUserByCountry()
        {
            var usersAllList = await _usersDb.FindAsync(users => true)
                    .Result
                    .ToListAsync();
            return usersAllList;
        }

        public async Task<List<User>> GetObjectsByRegion(string regionId)
        {             
            try
            {
               var usersList = await _usersDb.FindAsync(users=>users.RegionId == regionId)
                    .Result
                    .ToListAsync();
                return usersList;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null,HttpStatusCode.InternalServerError);
            }
        }

        public async Task<User> GetObjectBy(string email)
        {
           
            try
            {
                var user =  (User)_cache.GetObject(email);
                if (user == null)
                {
                    user = await _usersDb.FindAsync(user => user.Email == email)
                        .Result
                        .FirstAsync();
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
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null,HttpStatusCode.InternalServerError);
            }

        }

        public async Task<User> SetObject(User value)
        {
            
            try
            {
               await _usersDb.InsertOneAsync(value);
                return value;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null,HttpStatusCode.InternalServerError);
            }
        }

        public async Task<User> UpdateObject(User newValue)
        {
          
            try
            {
                var filter = Builders<User>.Filter.Eq("email", newValue.Email);
                await _usersDb.ReplaceOneAsync(filter, newValue);
                _cache.DeleteObject(newValue.Email);
                _cache.SetObject(newValue.Email, newValue,10);
                return newValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null,HttpStatusCode.InternalServerError);
            }
        }

        public async Task<User> DeleteObject(string email)
        {
            try
            {
                _cache.DeleteObject(email);
                var response = await _usersDb.FindAsync(user => user.Email == email).Result.FirstAsync();
                await _usersDb.DeleteOneAsync(user => user.Email == email);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> CheckUser(string email)
        {
            try
            {
                var user = (User)_cache.GetObject(email);
                if (user == null)
                {
                    user = await _usersDb.FindAsync(user => user.Email == email)
                        .Result
                        .FirstAsync();                    
                }
                return true;
            }
            catch (Exception ex)
            {              
                if (ex.Message == DbExMessage.NoElements)
                {
                    return false;
                }
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        
        }
        #region Friend views
        public async Task<List<User>> GetOnlineUsersByList(List<string> users,int page,bool isOnline)
        {
            var skip = page * _limit;

            var aggregateOne = new BsonDocument()
            {
                {
                  "$match", new BsonDocument()
                  {
                      {
                          "email", new BsonDocument()
                          {
                              {
                                  "$in", new BsonArray(users)
                              }
                          }
                      }
                  }
                }
            };
            var aggregateTwo = new BsonDocument();
            if (isOnline)
            {
                aggregateTwo = new BsonDocument()
               {
                  {

                    "$match", new BsonDocument()
                    {
                        {
                            "lastBeOnline", new BsonDocument()
                            {
                                {
                                    "$gte",DateTime.UtcNow.AddMinutes(-5)
                                }
                            }
                        }
                    }
                  }
               };



            }
            else
            {
                aggregateTwo = new BsonDocument()
               {
                  {

                    "$match", new BsonDocument()
                    {
                        {
                            "lastBeOnline", new BsonDocument()
                            {
                                {
                                    "$lte",DateTime.UtcNow.AddMinutes(-5)
                                }
                            }
                        }
                    }
                  }
               };
            }
           

            var aggreagetThree = new BsonDocument()
            {
                {
                    "$skip",skip
                }
            };

            var aggreagetFour = new BsonDocument()
            {
                {
                    "$limit",_limit
                }
            };

            var pipeline = new BsonDocument[] { aggregateOne, aggregateTwo, aggreagetThree, aggreagetFour };
            try
            {
                return await _usersDb.Aggregate<User>(pipeline).ToListAsync();           
                
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return new List<User>();
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }


        }

        public async Task<List<User>> GetUsers(string searchText, string? regionId, int page)
        {
            var skip = page * _limit;
            var aggregateOne = new BsonDocument()
            {
                {
                    "$match", new BsonDocument()
                    {
                        {
                            "nickname",new BsonDocument()
                            {
                                {
                                    "$regex",searchText
                                }
                            }
                        }
                    }
                    
                }
            };
            var aggregateTwo = new BsonDocument();
            if (regionId != null)
            {
                aggregateTwo = new BsonDocument()
                {
                    {
                        "$match", new BsonDocument()
                        {
                            {
                                "region_id",regionId
                            }
                        }
                    }
                };
            }
            var aggregateThree = new BsonDocument()
            {
                {
                    "$skip", skip
                }
            };
            var aggreagetFour = new BsonDocument()
            {
                {
                    "$limit", _limit
                }
            };
            var pipeline =  new BsonDocument [4];
            if (regionId != null)
            {
                pipeline = new BsonDocument[] { aggregateOne, aggregateTwo, aggregateThree, aggreagetFour };
            }
            else
            {
                pipeline = new BsonDocument[] { aggregateOne, aggregateThree, aggreagetFour };
            }
            try
            {
                return await _usersDb.Aggregate<User>(pipeline).ToListAsync();

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return new List<User>();
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }

        #endregion

    }
}
