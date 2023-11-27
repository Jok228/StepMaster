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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUser_Service _user;
        private readonly IPost_Service _post;
        IMemoryCache _cache;

        public AuthorizationController(IUser_Service user, IPost_Service post, IMemoryCache cache)
        {
            _post = post;
            _user = user;
            _cache = cache;
        }
        [HttpGet]
        [Route("Auth")]
        [BasicAuthorization]
        public async Task<User> Auth()
        {
            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var response = await _user.GetByLoginAsync(User.Identity.Name);

            await Authenticate(response,true);
            return response;

        }
        [HttpPost]
        [Route("SendCode")]
        public async Task<Code> SendCode([FromForm] string email)
        {         
            
            var codeStr = new Code();
            var checkUser = await _user.GetByLoginAsync(email);
            if (checkUser == null)
            {
                var code = CodeGenerate.GeneratedCode();                
                var  send = await _post.SendMessageAsync(email, code);
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
        public async Task<User> Registration([FromForm] User user)
        {

            user.role = "user";            

            await Authenticate(user, false);

            var response = await _user.RegUserAsync(user);

            Response.StatusCode = 201;

            return response;

        }
        
        [HttpGet]
        [Route("SendNewPassword")]
        public async Task SendNewPassword([FromForm] string email)
        {
            var user = await _user.GetByLoginAsync(email);
            var newPassword = CodeGenerate.RandomString(12);
            var host = Request.GetEncodedUrl().Split("/api/")[0];
            bool send = false;
            if (user != null)
            {
                
               send  = await _post.SendPasswordOnMail(email, newPassword, host);

                _cache.Set(email+=newPassword, newPassword, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                if (send)
                {
                    Response.StatusCode = 200;
                }
                else
                {
                    Response.StatusCode = 400;
                }
            }
            else
            {
                Response.StatusCode = 404;
            }
           

        }

        private async Task Authenticate(User user, bool firstReg)
        {
            // создаем один claim
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role)
        };
           
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            // 
            
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            HttpContext.Response.Headers.TryGetValue("Set-Cookie", out var newCookies);
            var cookies = newCookies.ToString().Split(';')[0];
            user.lastCookie = cookies;
            if (firstReg)
            {
               var status =  await _user.UpdateUser(user);

            }
            
            
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
        [HttpGet]
        [Route("UpdateCookies")]        
        public async Task UpdateCookies()
        {
            var cookies = Request.Headers.SingleOrDefault(header => header.Key == "Cookie").Value.ToString();
            if (cookies == null)
            {
                Response.StatusCode = 401;
            }
            else
            {
               var user = await _user.GetUserbyCookie(cookies);
                if (user == null)
                {
                    Response.StatusCode = 404;
                }
                else
                {
                    await Authenticate(user,true);                  
                   Response.StatusCode=200;

                }
            }
        }

    }
}
