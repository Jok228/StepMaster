using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System.Net;

namespace Infrastructure.MongoDb.Repositories
{
    public class Rating_Repository : IRating_Repository
    {
        private readonly IMongoCollection<Rating> _ratings;
        private IMy_Cache _cache;
        private ILogger<Day_Repository> _logger;
        public Rating_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient, ILogger<Day_Repository> logger)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _ratings = database.GetCollection<Rating>(TableName.Rating);
            _cache = cache;
            _logger = logger;
        }
        public async Task<Rating> GetRating(string regionId)
        {
            try
            {
                var ratingRegion = await _ratings.FindAsync(rating => rating.regionId == regionId&& rating.date.Month == DateTime.Now.Month && rating.date.Year == DateTime.Now.Year) 
                    .Result
                    .FirstAsync(); //Logic for region -> find list rating region
                return ratingRegion;

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return await CreateRating(regionId);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);

            }
        }

        public async Task<Rating> CreateRating(string regionId)
        {
            var rating = new Rating(regionId);
            try
            {
                await _ratings.InsertOneAsync(rating);
                return rating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
            
        }
        public async Task<Rating> UpdateRating(Rating newRating)
        {
            try
            {
                _ratings.ReplaceOne(rating => rating._id == newRating._id, newRating);
                return newRating;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        public async Task<Rating> SetRating(Rating newRating)
        {
            
            try
            {
                await _ratings.InsertOneAsync(newRating);
                return newRating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        public async Task<List<Rating>> GetRatingsByUserEmail(string email)
        {
            var aggregateStage1 = new BsonDocument()
           {
               {
                   "$match",new BsonDocument() {
                       {"ratingUsers.email",email}
                   }
               }
           };
            var pipeline = new BsonDocument[] { aggregateStage1};
            try
            {
                var result = await _ratings.AggregateAsync<Rating>(pipeline).Result.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return new List<Rating>();
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        public async Task<bool> CheckRating(string regionId)
        {
            try
            {
                var ratingRegion = await _ratings.FindAsync(rating => rating.regionId == regionId && rating.date.Month == DateTime.Now.Month && rating.date.Year == DateTime.Now.Year)
                    .Result
                    .FirstAsync(); //Logic for region -> find list rating region
                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return false;
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);

            }
        }
    }
}
