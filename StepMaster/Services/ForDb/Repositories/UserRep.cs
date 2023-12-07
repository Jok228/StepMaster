using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System.Security.Cryptography;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.HashSup;
using MongoDB.Bson;
using StepMaster.Services.ForDb.Interfaces;
using StepMaster.Models.Entity.Response;
using Microsoft.OpenApi.Models;

namespace StepMaster.Services.ForDb.Repositories
{
    public class UserRep : IUser_Service
    {
        IMemoryCache _cache;
        private readonly IMongoCollection<User> _users;

        public UserRep(IAPIDatabaseSettings settings, IMongoClient mongoClient, IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("User");
            _cache = cache;

        }
        public async Task<List<User>> GetAllUser()
        {
            var list = new List<User>();
            list = await _users.FindAsync(_ => true).Result.ToListAsync();
            return list;
        }


        public async Task<User> GetByLoginAsync(string email)
        {
            try
            {
                _cache.TryGetValue(email, out User? user);
                if (user == null)
                {
                    user = await _users.FindAsync(user => user.email == email).Result.FirstAsync();
                    _cache.Set(email, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    return user;
                }
                else
                {
                    return user;
                }

            }
            catch
            {
                return null;
            }
        }

        public async Task<User> RegUserAsync(User newUser)
        {
            try
            {

                newUser.password = HashCoder.GetHash(newUser.password);
                newUser.role = "user";


                await _users.InsertOneAsync(newUser);
                return newUser;
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> UpdateUser(User userUpdate)
        {
            try
            {

                var filter = Builders<User>.Filter.Eq("email", userUpdate.email);
                await _users.ReplaceOneAsync(filter, userUpdate);
                _cache.Remove(userUpdate.email);

                return userUpdate;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetUserbyCookie(string cookies)
        {

            try
            {
                var user = await _users.FindAsync(user => user.lastCookie == cookies)
                    .Result
                    .FirstAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BaseResponse<bool>> CheckPassword(string login, string password)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _cache.TryGetValue(login, out User? user);
                if(user == null)
                {
                    user = await _users.FindAsync(u => u.email == login)
                    .Result
                    .FirstAsync();
                    _cache.Set(user, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
                
                
                if (HashCoder.Verify(user.password, password))
                {
                    response.Data = true;
                    response.Status = MyStatus.Success;
                    return response;

                }
                else
                {
                    response.Data = false;
                    response.Status = MyStatus.Unauthorized;
                    return response;
                }
                   
                
                
            }
            catch (Exception ex)
            {
                if(ex.Message == "Sequence contains no elements")
                {
                    response.Data = false;
                    response.Status = MyStatus.NotFound;
                    return response;
                }
                response.Data= false;
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                return response;
            }
        }
    }
}
