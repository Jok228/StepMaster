using Application.Repositories.Db.Interfaces_Repository;

using Domain.Entity.API;
using Domain.Entity.Main;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Services.ForDb.Interfaces;

namespace StepMaster.Services.ForDb.Repositories;

public class RegionRep : IRegion_Service
{
    private readonly IRegion_Repository _region;

    public RegionRep(IRegion_Repository region)
    {
       
        _region = region;
        

    }
    public async Task<BaseResponse<List<Region>>> GetAllRegionsAsync()
    {
        return await _region.GetRegions();
    }

}