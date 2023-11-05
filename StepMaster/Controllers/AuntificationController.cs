using API.Entity.SecrurityClass;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DnsClient.Protocol;
using StepMaster.Services.AuthCookie;

namespace StepMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuntificationController : ControllerBase
    {
        private readonly IUser_Service _user;


        public AuntificationController(IUser_Service user)
        {
            _user = user;
        }
        [HttpGet]
        [Route("Auth")]
        [BasicAuthorization]
        public async Task<User> Auth()
        {
            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            var secret = User.Claims.First(x => x.Type == ClaimTypes.Hash).Value;
            var response = await _user.GetUser(User.Identity.Name, secret);
            await Authenticate(User.Identity.Name, role);
            return response;
            
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<User> Registration ([FromForm] User user)
        {
           var response = await _user.RegUserAsync(user);
           
           await Authenticate(response.login, response.password);

           Response.StatusCode = 201;

           return response;


        }

        private async Task Authenticate(string userName, string userRole)
        {
            // создаем один claim
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
        };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }        
        [HttpGet]        
        [Route("LogOut")]
        [Authorize(Roles ="user")]
        public async Task Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            else
            {
                HttpContext.Response.StatusCode = 419;
            }
        }
       
    }
}
