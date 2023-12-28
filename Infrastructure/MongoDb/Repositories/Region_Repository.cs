using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.API;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.Settings;
using MongoDB.Driver;
using StepMaster.Models.Entity;

namespace Infrastructure.MongoDb.Repositories
{
    public class Region_Repository : IRegion_Repository
    {
        private readonly IMongoCollection<Region> _regions;
        private IMy_Cache _cache;
        private string keyCache = "AllRegions";
        public Region_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _regions = database.GetCollection<Region>(TableName.Region);
            _cache = cache;

        }
        public async Task<BaseResponse<List<Region>>> GetRegions()
        {
            try
            {
                var regions = (List<Region>)_cache.GetObject(keyCache);
                
                if (regions == null)
                {
                    regions = await _regions.FindAsync(region => true)
                    .Result
                    .ToListAsync();
                    _cache.SetObject(keyCache, regions, 100);
                   
                }                
                return BaseResponse<List<Region>>.Create(regions,MyStatus.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<List<Region>>.Create(null, MyStatus.Except);
            }
        }
    }
}
