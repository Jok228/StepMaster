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
using StepMaster.Models.CodeGenerate;
using StepMaster.Models.HashSup;
using StepMaster.Services.AuthCookie;

namespace StepMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUser_Service _user;
        private readonly IPost_Service _post;


        public AuthorizationController(IUser_Service user, IPost_Service post)
        {
            _post = post;
            _user = user;
        }
        [HttpGet]
        [Route("Auth")]
        [BasicAuthorization]
        public async Task<User> Auth()
        {
            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            
            var response= await _user.GetByLoginAsync(User.Identity.Name);
            
            await Authenticate(User.Identity.Name, role);
            return response;
            
        }
        [HttpPost]
        [Route("CheckUser")]
        public async Task<string> CheckUser ([FromForm] string email )
        {
            var checkUser = await _user.GetByLoginAsync(email);
            if (checkUser == null)
            {
                var code = CodeGenerate.GeneratedCode();
                var send = await _post.SendMessageAsync(email, code);
                if (send)
                {
                    return code;
                }
                else
                {
                    Response.StatusCode = 400;
                    return "failed send message code on email";
                }
                
            }
            else
            {
                Response.StatusCode = 409;
                return "User with this email already have";
            }
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<User> Registration ([FromForm] User user)
        {
           var response = await _user.RegUserAsync(user);
           
           await Authenticate(response.email, response.password);

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
        [CustomAuthorizeUser("admin")]
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
