using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StepMaster.Models.Entity;

namespace Infrastructure.MongoDb.Repositories
{
    public class Region_Repository : IRegion_Repository
    {
        private readonly IMongoCollection<Region> _regionsDb;
        private IMy_Cache _cache;
        private string keyCache = "AllRegions";
        private ILogger<Day_Repository> _logger;
        public Region_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient, ILogger<Day_Repository> logger)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _regionsDb = database.GetCollection<Region>(TableName.Region);
            _cache = cache;
            _logger = logger;
        }
        public async Task<List<Region>> GetRegions()
        {
            try
            {
                var regions = (List<Region>)_cache.GetObject(keyCache);
                
                if (regions == null)
                {
                    regions = await _regionsDb.FindAsync(region => true)
                    .Result
                    .ToListAsync();
                    _cache.SetObject(keyCache, regions, 100);
                   
                }
                return regions;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        public async Task<Region> GetRegionById(string mongoId)
        {            
                var regions = (List<Region>)_cache.GetObject(keyCache);
                if (regions == null)
                {
                    regions = await GetRegions();
                }
                var result = regions.Find(a => a._id == mongoId);
                if(result.fullName == null || result._id == null)
                {
                    _logger.LogError("Region Not Found in GetRegionById");                
                    throw new HttpRequestException("500 Shit is happenes", null, HttpStatusCode.InternalServerError);                
                }            
                return result; 
        }

       
    }
}
