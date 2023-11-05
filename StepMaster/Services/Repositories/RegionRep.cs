using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Services.Repositories;

public class RegionRep:IRegion_Service
{
    private string RegionsCache = "Regions";
    IMemoryCache _cache;
    private readonly IMongoCollection<Region> _region;        
        
    public RegionRep(IAPIDatabaseSettings settings, IMongoClient mongoClient,IMemoryCache cache)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _region = database.GetCollection<Region>("Regions");
        _cache = cache;

    }
    public async Task<List<Region>> GetAllRegionsAsync()
    {
        _cache.TryGetValue(this.RegionsCache, out List<Region>? regions);
        if (regions == null)
        {
            regions = await _region.FindAsync(x => true).Result.ToListAsync();
            _cache.Set(this.RegionsCache, regions,new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        return regions;
    }
}