using StepMaster.Models.Entity;

namespace StepMaster.Services.ForDb.Interfaces;

public interface IRegion_Service
{
    Task<List<Region>> GetAllRegionsAsync();
}