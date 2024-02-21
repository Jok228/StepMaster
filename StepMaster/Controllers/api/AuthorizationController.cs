
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
using Domain.Entity.API;
using StepMaster.Models.API.UserModel;
using Infrastructure.MongoDb.Cache.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using Application.Repositories.Db.Interfaces_Repository;
using System.Net;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUser_Service _usersService;
        private readonly IPost_Service _postService;
        private readonly IMy_Cache _cacheService;
        private readonly IRating_Service _ratingService;
        private readonly IClan_Repository _clanRepository;

        public AuthorizationController(IUser_Service user, IPost_Service post, IMy_Cache cache, IRating_Service ratingService, IClan_Repository clanRepository)
        {
            _postService = post;
            _usersService = user;
            _cacheService = cache;
            _ratingService = ratingService;
            _clanRepository = clanRepository;
        }
        [HttpGet]
        [Route("Auth")]
        [BasicAuthorization]
        public async Task Auth()
        {

            var response = await _usersService.GetByLoginAsync(User.Identity.Name);

            await Authenticate(response,true);
            

        }
        [HttpPost]
        [Route("SendCode")]
        public async Task<Code> SendCode([FromForm] string email)
        {              
            if (!await _usersService.CheckUser(email))
            {
                var  send = await _postService.SendCodeUser(email);
                return send;
            }
            else
            {
                throw new HttpRequestException("409 This User already registred", null, HttpStatusCode.Conflict);
            }
        }
        [HttpPost]
        [CustomAuthorizeUser("all")]
        [Route("SetToken")]
        public async Task<User> SetToken([FromForm] string token)
        {
            var email = User.Identity.Name;
            var user = await _usersService.GetByLoginAsync(email);
            user.FireBaseToken = token;
            return await _usersService.UpdateUser(user);   
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<UserLargeResponse> Registration([FromForm] UserRegModel user)
        {
            if(! await _usersService.CheckUser(user.email))
            {
                var resUser = await _usersService.RegUserAsync(UserRegModel.GetFullUser(user));
                var resRating = await _ratingService.AddNewPosition(user.email, user.region_id);
                await Authenticate(resUser, true);
                await _usersService.UpdateUser(resUser);
                return new UserLargeResponse(resUser, resRating, null, null);
            }
            else
            {
                throw new HttpRequestException("409 This User already registred", null, HttpStatusCode.Conflict);
            }

        }
        
        [HttpGet]
        [Route("SendNewPassword")]
        public async Task SendNewPassword([FromForm] string email)
        {          
            if (await _usersService.CheckUser(email))
            {
                var newPassword = CodeGenerate.RandomString(12);
                var host = Request.GetEncodedUrl().Split("/api/")[0];
                await _postService.SendPasswordOnMail(email, newPassword, host);
                _cacheService.SetObject(email + newPassword, newPassword, 10);
            }
            else
            {
                Response.StatusCode = 404;
            }
            
        }

        private async Task Authenticate(User user, bool firstReg)
        {
            
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
        };
           
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);          
            
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            HttpContext.Response.Headers.TryGetValue("Set-Cookie", out var newCookies);
            var cookies = newCookies.ToString().Split(';')[0];
            user.LastCookie = cookies;
            if (firstReg)
            {
               await _usersService.UpdateUser(user);
            }
            await _usersService.UpdateLastBeOnline(user.Email);
            
            
        }
        [HttpGet]
        [Route("LogOut")]
        [CustomAuthorizeUser("all")]
        public async Task Logout()
        {            
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var response = await _usersService.DeleteCookie(userEmail);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (response)
                {
                    Response.StatusCode = 200;
                }
                else
                {
                    Response.StatusCode = 500;
                }
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
               var user = await _usersService.GetUserbyCookie(cookies);                
               await Authenticate(user, true);
               Response.StatusCode = 200;
            }
        }
        [HttpDelete]
        [Route("DeleteUser")]      
        public async Task DeleteUser([FromForm]string email)
        {            
            var response = await _usersService.DeleteUser(email);

        }
    }
}
