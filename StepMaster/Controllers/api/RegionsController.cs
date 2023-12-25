using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.API;
using Domain.Entity.Main;
=======
using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;
using StepMaster.Services.Interfaces;
>>>>>>> master

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
<<<<<<< HEAD
        
=======
>>>>>>> master
        var bodyResponse = await _regions.GetAllRegionsAsync();

        var regions = new ResponseList<Region>();
        regions.Result = bodyResponse;
        return regions;
    }
}