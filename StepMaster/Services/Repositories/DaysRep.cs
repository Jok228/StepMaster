using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;
using StepMaster.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<BaseResponse<List<Day>>> GetDaysUserByEmail(string email)
        {
            BaseResponse<List<Day>> response = new BaseResponse<List<Day>>();
            _cache.TryGetValue(_cacheName += email, out List<Day> list);
            if(list == null) 
            {
                try
                {
                    list = await _days.FindAsync(d => d.email == email)
                        .Result
                        .ToListAsync();
                    _cache.Set(_cacheName+email, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    response.Data = list;
                    response.Status = MyStatus.Success;
                    return response;

                }
                catch
                {
                    response.Status = MyStatus.Except;
                    return response;
                }
            }
            else
            {
                response.Data = list;
                response.Status = MyStatus.Success;
                return response;
            }
            
        }

        public async Task<BaseResponse<Day>> SetDayAsync(Day day, string email)
        {
            BaseResponse<Day> response = new BaseResponse<Day>();
            _cache.TryGetValue(_cacheName += day.email, out Day checkday);
            if(checkday != null)
            {
                _cache.Remove(_cacheName+day.email);
            }
            try
            {
                var count =  _days.FindAsync(d=>d.email == email && d.date == day.date)
                    .Result
                    .Any();
                if (count)
                {
                    
                    response.Status = MyStatus.Exists;
                    return response;
                }
                await _days.InsertOneAsync(day);
                response.Data = day;
                response.Status = MyStatus.SuccessCreate;
                return response;
            }
            catch
            {
                response.Status = MyStatus.Except;
                return response;
            }

        }

        public async  Task<BaseResponse<Day>> UploadDayAsync(Day uploadday)
        {
            BaseResponse<Day> response = new BaseResponse<Day>();
            _cache.TryGetValue(_cacheName += uploadday.email, out Day checkday);
            try
            {
                var filter = Builders<Day>.Filter.Eq("_id", uploadday._id);
                
                var day =  _days.FindAsync(a => a._id == uploadday._id)
                    .Result
                    .FirstAsync()
                    .Result;
                
                    await _days.ReplaceOneAsync(filter, uploadday);
                    response.Data = uploadday;
                    response.Status = MyStatus.Success;
                    return response;
                          
               
                
               
            }
            catch (Exception ex)
            {
                if(ex.Message == "One or more errors occurred. (Sequence contains no elements)")
                {
                    response.Status = MyStatus.NotFound;
                    return response;
                }
                response.Status = MyStatus.Except;
                return response;

            }
        }
    }
}
