using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;
using StepMaster.Services.Interfaces;
using System.Security.Claims;

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

        var regions = new ResponseList<Region>();
        regions.Result = bodyResponse;
        return regions;
    }
}