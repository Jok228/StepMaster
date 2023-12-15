using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

using StepMaster.Models.Entity;
using Domain.Entity.API;
using StepMaster.Services.ForDb.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Entity.Main;
using Application.Services.ForDb.APIDatebaseSet;
using System.Collections.Generic;

namespace StepMaster.Services.ForDb.Repositories
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
            var list = new List<Day>();
                try
                {
                    list = await _days.FindAsync(d => d.email == email)
                        .Result
                        .ToListAsync();                    
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

        public async Task<BaseResponse<Day>> SetDayAsync(Day day, string email)
        {
            BaseResponse<Day> response = new BaseResponse<Day>();
            day.date = DateTime.Now;
            try
            {
                var count = _days.FindAsync(d => d.email == email && d.date.Month == day.date.Month && d.date.Day == day.date.Day)
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

        public async Task<BaseResponse<Day>> UploadDayAsync(Day uploadday)
        {
            BaseResponse<Day> response = new BaseResponse<Day>();            
            try
            {
                var filter = Builders<Day>.Filter.Eq("_id", uploadday._id);

                var day = _days.FindAsync(day => day._id == uploadday._id)
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
                if (ex.Message == "One or more errors occurred. (Sequence contains no elements)")
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
