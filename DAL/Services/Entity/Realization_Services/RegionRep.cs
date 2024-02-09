using Application.Repositories.Db.Interfaces_Repository;

using Domain.Entity.Main;
using StepMaster.Services.ForDb.Interfaces;

namespace StepMaster.Services.ForDb.Repositories;

public class RegionRep : IRegion_Service
{
    private readonly IRegion_Repository _regionRepository;

    public RegionRep(IRegion_Repository region)
    {
       
        _regionRepository = region;
        

    }
    public async Task<List<Region>> GetAllRegionsAsync()
    {
        return await _regionRepository.GetRegions();
    }

    public async Task<Region> GetRegionById(string mongoId)
    {      
        return await _regionRepository.GetRegionById(mongoId);
    }
}