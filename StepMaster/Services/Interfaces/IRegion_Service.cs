using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces;

public interface IRegion_Service
{
    Task<List<Region>> GetAllRegionsAsync(); 
}