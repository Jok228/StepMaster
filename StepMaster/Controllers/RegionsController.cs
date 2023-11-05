using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController:ControllerBase
{
    private readonly IRegion_Service _regions;
    public RegionsController(IRegion_Service regions)
    {
        _regions = regions;
    }
    [HttpGet]
    [Route("GetRegions")]
    public async Task<List<Region>> GetRegions ()
    {
        return await _regions.GetAllRegionsAsync();
    }
}