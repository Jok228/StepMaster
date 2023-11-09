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
using System.Text.Json;
using Amazon.Runtime.Internal;

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
        [Route("SendCode")]
        public async Task<Code> SendCode([FromForm] string email )
        {
            var codeStr = new Code();
            var checkUser = await _user.GetByLoginAsync(email);
            if (checkUser == null)
            {
                var code = CodeGenerate.GeneratedCode();
                
                var send = await _post.SendMessageAsync(email, code);
                if (send)
                {
                    
                    codeStr.code = code;
                    return codeStr;                    
                }
                else
                {
                    Response.StatusCode = 400;                   

                    return codeStr;


                }
                
            }
            else
            {
                Response.StatusCode = 409;
                
                return codeStr;
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
        [HttpPut]
        [Route("RecoveryPassword")]
        public async Task<User> RecoveryPassword([FromForm] string email)
        {

            var response = new User();

            var newPassword = CodeGenerate.RandomString(8);
            
            var user = await _user.GetByLoginAsync(email);


            if (user != null)
            {
                user.password = newPassword;
                var send = await _post.SendMessageAsync(email,newPassword);
                if(!send)
                {
                    Response.StatusCode = 450;
                    return response;
                }
                var result = await _user.RecoveryPasswordAsync(user);
                if (result != null && send)
                {
                    
                    
                    return user;
                    
                }
                else
                {
                    Response.StatusCode = 404;
                    return response;
                }                
            }
            else
            {

                Response.StatusCode = 404;
                return response;
            }    


        }

        private async Task Authenticate(string userName, string userRole)
        {
            // создаем один claim
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
        };
            var cookies = new Cookie("name", "value");
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
