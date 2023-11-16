using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Services.Repositories
{
    public class BodiesRep : IBodies_Service
    {
        private static string _cacheName = "Body"; 
        IMemoryCache _cache;
        private readonly IMongoCollection<Body> _bodies;

        public BodiesRep(IAPIDatabaseSettings settings, IMongoClient mongoClient, IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bodies = database.GetCollection<Body>("Bodies");
            _cache = cache;

        }
        public async Task<Body> GetBodyByEmail(string email)
        {
            _cache.TryGetValue(_cacheName += email, out Body body);
            if(body == null)
            {
                try
                {
                    body = await _bodies.FindAsync(b => b.email == email)
                        .Result
                        .FirstAsync();
                    _cache.Set(_cacheName+=email, body, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    return body;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return body;
            }
            
        }

        public async Task<Body> SetEditBody(Body body)
        {
            try
            {
                await _bodies.InsertOneAsync(body);
                return body;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
