using API.Auth.AuthCookie;
using Application.Services.FIreBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace StepMaster.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class CheckCookies : ControllerBase
{

    private readonly IFireBase_Service _fireBaseService;
    
   public CheckCookies(IFireBase_Service fireBase)
    {
        _fireBaseService = fireBase;
    }

    [HttpGet]
    [Route("MethodForUser")]
   
    public async Task MethodForUser()
    {
    }
    
    
}