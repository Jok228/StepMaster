using Domain.Entity.Main;

namespace StepMaster.Services.ForDb.Interfaces;

public interface IRegion_Service
{
    Task<List<Region>> GetAllRegionsAsync();

    Task<Region> GetRegionById(string mongoId);
}