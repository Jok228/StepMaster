
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main.Titles;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.Settings;
using MongoDB.Driver;
using System.Net;

namespace Infrastructure.MongoDb.Repositories
{
    public class Condition_Repository : ICondition_Repository
    {
        private readonly IMongoCollection<Condition> _condition;
        private IMy_Cache _cache;
        private const string key = "listConditions";
        public Condition_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _condition = database.GetCollection<Condition>(TableName.Condition);
            _cache = cache;

        }
        public async Task<List<Condition>> GetConditionsAsync()
        {
            try
            {
                var list = (List<Condition>)_cache.GetObject(key);
                if(list == null)
                {
                    list = await _condition.FindAsync(condition => true).Result.ToListAsync();
                }
                _cache.SetObject(key, list,600);
                return list;
            }
            catch 
            {
                throw new HttpRequestException("MongoDb ..", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
