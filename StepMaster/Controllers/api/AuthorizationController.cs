
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http.Extensions;

using System.Security.Claims;
using API.Auth.AuthBase;
using API.Auth.AuthCookie;
using StepMaster.Models.Entity;
using Application.Logic.CodeGenerate;
using Domain.Entity.Main;
using StepMaster.Services.ForDb.Interfaces;
using Application.Services.Post.Repositories;
using StepMaster.Auth.ResponseLogic;
using Domain.Entity.API;
using StepMaster.Models.API.UserModel;
using Infrastructure.MongoDb.Cache.Interfaces;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUser_Service _user;
        private readonly IPost_Service _post;
        private readonly IMy_Cache _cache;

        public AuthorizationController(IUser_Service user, IPost_Service post, IMy_Cache cache)
        {
            _post = post;
            _user = user;
            _cache = cache;
        }
        [HttpGet]
        [Route("Auth")]
        [BasicAuthorization]
        public async Task Auth()
        {

            var response = await _user.GetByLoginAsync(User.Identity.Name);

            await Authenticate(response.Data,true);
            

        }
        [HttpPost]
        [Route("SendCode")]
        public async Task<Code> SendCode([FromForm] string email)
        {   
            var checkUser = await _user.GetByLoginAsync(email);
            if (checkUser.Status == MyStatus.Success)
            {
                var  send = await _post.SendCodeUser(email);
                return ResponseLogic<Code>.Response(Response, send.Status, send.Data);
            }
            return ResponseLogic<Code>.Response(Response, checkUser.Status, new Code());
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<UserResponse> Registration([FromForm] UserRegModel user)
        {
            var response = await _user.RegUserAsync(UserRegModel.GetFullUser(user));

            await Authenticate(response.Data, false);

            Response.StatusCode = 201;

            return ResponseLogic<UserResponse>.Response(Response, response.Status, new UserResponse(response.Data));

        }
        
        [HttpGet]
        [Route("SendNewPassword")]
        public async Task SendNewPassword([FromForm] string email)
        {
            var user = await _user.GetByLoginAsync(email);
            var newPassword = CodeGenerate.RandomString(12);
            var host = Request.GetEncodedUrl().Split("/api/")[0];           
            if (user.Status == MyStatus.Success)
            {
                
                var send  = await _post.SendPasswordOnMail(email, newPassword, host);
                _cache.SetObject(email + newPassword, newPassword, 10);
                if (send.Status ==MyStatus.Success)
                {
                    Response.StatusCode = 200;
                }
                else
                {
                    Response.StatusCode = (int)send.Status;
                }
            }
            Response.StatusCode = (int)user.Status;
        }

        private async Task Authenticate(User user, bool firstReg)
        {
            
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role)
        };
           
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);          
            
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            HttpContext.Response.Headers.TryGetValue("Set-Cookie", out var newCookies);
            var cookies = newCookies.ToString().Split(';')[0];
            user.lastCookie = cookies;
            if (firstReg)
            {
               await _user.UpdateUser(user);
            }
            
            
        }
        [HttpGet]
        [Route("LogOut")]
        [CustomAuthorizeUser("all")]
        public async Task Logout()
        {            
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var response = await _user.DeleteCookie(userEmail);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Response.StatusCode = (int)response.Status;
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
            var cookies = Request.Headers.FirstOrDefault(header => header.Key == "Cookie").Value.ToString();
            if (cookies == string.Empty)
            {
                Response.StatusCode = 401;
            }
            else
            {
               var user = await _user.GetUserbyCookie(cookies);
                if (user.Status == MyStatus.Success)
                {
                    await Authenticate(user.Data, true);
                    Response.StatusCode = 200;
                    
                }
                else
                {
                    Response.StatusCode = 404;
                }
            }
        }

    }
}
