using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Services.Repositories
{
    public class DaysRep : IDays_Service
    {
        private static string _cacheName = "Day";
        IMemoryCache _cache;
        private readonly IMongoCollection<Day> _days;

        public DaysRep(IAPIDatabaseSettings settings, IMongoClient mongoClient, IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _days = database.GetCollection<Day>("Days");
            _cache = cache;

        }
        public async Task<List<Day>> GetDaysUserByEmail(string email)
        {
            _cache.TryGetValue(_cacheName += email, out List<Day> list);
            if(list == null) 
            {
                try
                {
                    list = await _days.FindAsync(d => d.email == email)
                        .Result
                        .ToListAsync();
                    _cache.Set(_cacheName+email, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    return list;

                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return list;
            }
            
        }

        public async Task<Day> SetDayAsync(Day day)
        {
            _cache.TryGetValue(_cacheName += day.email, out Day checkday);
            if(checkday != null)
            {
                _cache.Remove(_cacheName+day.email);
            }
            try
            {
                await _days.InsertOneAsync(day);
                return day;
            }
            catch
            {
                return null;
            }

        }
    }
}
