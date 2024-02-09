using API.Auth.AuthCookie;
using Microsoft.AspNetCore.Mvc;
namespace StepMaster.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class CheckCookies : ControllerBase
{
    [CustomAuthorizeUser("user")]
    [HttpGet]
    [Route("MethodForUser")]
    public async Task<string> MethodForUser()
    {
        return "You User";
    }
    
    [CustomAuthorizeUser("admin")]
    [HttpGet]
    [Route("MethodForAdmin")]
    public async Task<string> MethodForAdmin()
    {
        return "You Admin";
    }
}