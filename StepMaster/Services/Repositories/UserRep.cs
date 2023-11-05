using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;
using System.Security.Cryptography;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.HashSup;

namespace StepMaster.Services.Repositories
{
    public class UserRep : IUser_Service
    {
        IMemoryCache _cache;
        private readonly IMongoCollection<User> _users;        
        
        public UserRep(IAPIDatabaseSettings settings, IMongoClient mongoClient ,IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("User");
            _cache = cache;

        }
        public async  Task<List<User>> GetAllUser()
        {
            var list = new List<User>();
            list = await _users.FindAsync(_ => true).Result.ToListAsync();
            return list;
        }

        public async Task<User> GetUser(string login, string password)
        {
            try
            {
                User user = await _users.FindAsync( user => user.login == login && user.password == password).Result.FirstAsync();
                return user;
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
                newUser.region_id = "1";

                await _users.InsertOneAsync(newUser);
                return newUser;
            }
            catch
            {
                return null;
            }
        }
    }
}
