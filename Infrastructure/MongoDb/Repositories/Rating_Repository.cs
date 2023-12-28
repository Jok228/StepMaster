using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;

using DnsClient;
using Domain.Entity.API;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Implementation;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Repositories
{
    public class Rating_Repository : IRating_Repository
    {
        private readonly IMongoCollection<Rating> _ratings;
        private IMy_Cache _cache;
        public Rating_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _ratings = database.GetCollection<Rating>(TableName.Rating);
            _cache = cache;

        }
        public async Task<BaseResponse<Rating>> GetRating(string regionId)
        {
            try
            {
                var ratingRegion = await _ratings.FindAsync(rating => rating.regionId == regionId&& rating.date.Month == DateTime.Now.Month && rating.date.Year == DateTime.Now.Year) 
                    .Result
                    .FirstAsync(); //Logic for region -> find list rating region
                return BaseResponse<Rating>.Create(ratingRegion, MyStatus.Success);

            }
            catch (Exception ex)
            {
                if(ex.Message == DbExMessage.NoElements)
                {
                    return BaseResponse<Rating>.Create(null, MyStatus.NotFound);
                }
                return BaseResponse<Rating>.Create(null, MyStatus.Except);

            }
        }

        public async Task<BaseResponse<Rating>> CreateRating(string regionId)
        {
            var rating = new Rating(regionId);
            try
            {
                await _ratings.InsertOneAsync(rating);
                return BaseResponse<Rating>.Create(rating, MyStatus.SuccessCreate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<Rating>.Create(null,MyStatus.Except);
            }
            
        }
        public async Task<BaseResponse<Rating>> UpdateRating(Rating newRating)
        {
            try
            {
                _ratings.ReplaceOne(rating => rating._id == newRating._id, newRating);
                return BaseResponse<Rating>.Create(newRating, MyStatus.Success);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<Rating>.Create(null, MyStatus.Except);
            }
        }

        public async Task<BaseResponse<Rating>> SetRating(Rating newRating)
        {
            
            try
            {
                await _ratings.InsertOneAsync(newRating);
                return BaseResponse<Rating>.Create(newRating, MyStatus.SuccessCreate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<Rating>.Create(null, MyStatus.Except);
            }
        }
    }
}
