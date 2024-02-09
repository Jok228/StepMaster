using Microsoft.AspNetCore.Mvc;
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.API;
using Domain.Entity.Main;

namespace StepMaster.Controllers.api;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegion_Service _regions;
    public RegionsController(IRegion_Service regions)
    {
        _regions = regions;
    }
    
    [HttpGet]
    [Route("GetRegions")]
    public async Task<ResponseList<Region>> GetRegions()
    {
        
        var bodyResponse = await _regions.GetAllRegionsAsync();
        return new ResponseList<Region>(bodyResponse);
    }
}