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
using Domain.Entity.API;
using Infrastructure.MongoDb.DbHelper;
using Application.Repositories.Db.Interfaces_Repository;

namespace Infrastructure.MongoDb.Repositories
{
    public class User_Repository : IUser_Repository
    {
        private readonly IMongoCollection<User> _users;
        private IMy_Cache _cache;
        public User_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(TableName.User);
            _cache = cache;

        }

        public async Task<BaseResponse<User>> GetByCookie(string cookie)
        {
            try
            {
                var user = await _users.FindAsync(user => user.lastCookie == cookie)
                    .Result
                    .FirstAsync();
                
                return BaseResponse<User>.Create(user, MyStatus.Success);
            }
            catch (Exception ex) 
            {
                if(ex.Message == DbExMessage.NotFound)
                {
                    return BaseResponse<User>.Create(null, MyStatus.NotFound);
                }
                Console.WriteLine(ex.Message);
                return BaseResponse<User>.Create(null, MyStatus.Except);
            
            }
        }

        public async Task<BaseResponse<List<User>>> GetObjectsByRegion(string regionId)
        { 
            
            try
            {
                if(regionId == null)
                {
                    var usersAllList = await _users.FindAsync(users => true)
                    .Result
                    .ToListAsync();
                    return BaseResponse<List<User>>.Create(usersAllList, MyStatus.Success);
                }
               var usersList = await _users.FindAsync(users=>users.region_id == regionId)
                    .Result
                    .ToListAsync();
               return BaseResponse<List<User>>.Create(usersList,MyStatus.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NotFound)
                {
                    return BaseResponse<List<User>>.Create(new List<User>(), MyStatus.Success);
                }
                Console.WriteLine(ex.Message);
                return BaseResponse<List<User>>.Create(null,MyStatus.Except);
            }
        }

        public async Task<BaseResponse<User>> GetObjectBy(string email)
        {
            try
            {
                var user =  (User)_cache.GetObject(email);
                if (user == null)
                {
                    user = await _users.FindAsync(user => user.email == email)
                        .Result
                        .FirstAsync();                    
                    return BaseResponse<User>.Create(user, MyStatus.Success);
                }
                else
                {
                    return BaseResponse<User>.Create(user, MyStatus.Success);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {                    
                    return BaseResponse<User>.Create(null, MyStatus.NotFound);
                }
                Console.WriteLine(ex.Message);
                return BaseResponse<User>.Create(null, MyStatus.Except); ;
            }

        }

        public async Task<BaseResponse<User>> SetObject(User value)
        {
            
            try
            {
               await _users.InsertOneAsync(value);
               return BaseResponse<User>.Create(value,MyStatus.Success);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<User>.Create(null, MyStatus.Except);
            }
        }

        public async Task<BaseResponse<User>> UpdateObject(User newValue)
        {
          
            try
            {
                var filter = Builders<User>.Filter.Eq("email", newValue.email);
                await _users.ReplaceOneAsync(filter, newValue);
                _cache.DeleteObject(newValue.email);
                _cache.SetObject(newValue.email, newValue,10);
                return BaseResponse<User>.Create(newValue, MyStatus.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
                return BaseResponse<User>.Create(null, MyStatus.Except);
            }
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                _cache.DeleteObject(email);
                var response = await _users.DeleteOneAsync(user => user.email == email);
                if (response.DeletedCount == 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message} - - - {ex.StackTrace}");
                return false;
            }
        }
    }
}
