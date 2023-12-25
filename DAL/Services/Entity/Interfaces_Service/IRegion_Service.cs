using Domain.Entity.API;
using Domain.Entity.Main;

namespace StepMaster.Services.ForDb.Interfaces;

public interface IRegion_Service
{
    Task<BaseResponse<List<Region>>> GetAllRegionsAsync();
}