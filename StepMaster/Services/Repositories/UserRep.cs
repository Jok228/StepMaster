using API.DAL.Entity.APIDatebaseSet;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Services.Repositories
{
    public class UserRep : IUser_Service
    {
        
        private readonly IMongoCollection<User> _users;        
        
        public UserRep(IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("User");            
            
        }
        public async  Task<List<User>> GetAllUser()
        {
            var list = new List<User>();
            list = await _users.FindAsync(_ => true).Result.ToListAsync();
            return list;
        }

        public Task NewUser()
        {
            throw new NotImplementedException();
        }
    }
}
