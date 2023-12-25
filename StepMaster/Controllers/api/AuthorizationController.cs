<<<<<<< HEAD
﻿
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http.Extensions;
using StepMaster.Services.ForDb.Interfaces;

using API.Controllers.CodeGenerate;
using Domain.Entity.Main;
using System.Security.Claims;
using API.Auth.AuthBase;
using API.Auth.AuthCookie;
using Application.Services.Post.Repositories;
using Domain.Entity.API;

=======
﻿using API.Entity.SecrurityClass;
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
>>>>>>> master

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
<<<<<<< HEAD
        public async Task Auth()
        {

            var response = await _user.GetByLoginAsync(User.Identity.Name);

            await Authenticate(response.Data,true);
            
=======
        public async Task<User> Auth()
        {
            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var response = await _user.GetByLoginAsync(User.Identity.Name);

            await Authenticate(User.Identity.Name, role);
            return response;
>>>>>>> master

        }
        [HttpPost]
        [Route("SendCode")]
        public async Task<Code> SendCode([FromForm] string email)
<<<<<<< HEAD
        {   
            var checkUser = await _user.GetByLoginAsync(email);
            if (checkUser.Data == null)
            {
                var  send = await _post.SendCodeUser(email);
                if (send != null)
                {
                    return send.Data;
=======
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
>>>>>>> master
                }
                else
                {
                    Response.StatusCode = 400;

<<<<<<< HEAD
                    return send.Data;
=======
                    return codeStr;


>>>>>>> master
                }

            }
            else
            {
                Response.StatusCode = 409;

<<<<<<< HEAD
                return new Code();
=======
                return codeStr;
>>>>>>> master
            }
        }
        [HttpPost]
        [Route("Registration")]
<<<<<<< HEAD
        public async Task<UserResponse> Registration([FromForm] User user)
        {

            user.role = "user";            

            await Authenticate(user, false);

            var response = new UserResponse(await _user.RegUserAsync(user));

            response.rating = null;
=======
        public async Task<User> Registration([FromForm] User user)
        {
            var response = await _user.RegUserAsync(user);

            await Authenticate(response.email, response.password);
>>>>>>> master

            Response.StatusCode = 201;

            return response;

<<<<<<< HEAD
=======

>>>>>>> master
        }
        
        [HttpGet]
        [Route("SendNewPassword")]
        public async Task SendNewPassword([FromForm] string email)
        {
<<<<<<< HEAD
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
=======

            var newPassword = CodeGenerate.RandomString(12);
            var host = Request.GetEncodedUrl().Split("/api/")[0];
            var send = await _post.SendPasswordOnMail(email, newPassword, host);

            _cache.Set(newPassword, newPassword, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            if (send)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }

        }

        private async Task Authenticate(string userName, string userRole)
>>>>>>> master
        {
            // создаем один claim
            var claims = new List<Claim>
        {
<<<<<<< HEAD
            new Claim(ClaimTypes.Name, user.email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role)
        };
           
=======
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
        };
            var cookies = new Cookie("name", "value");
>>>>>>> master
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
<<<<<<< HEAD
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
        [CustomAuthorizeUser("all")]
        public async Task Logout()
        {
            var userEmail = User.Identity.Name;
            if (User.Identity.IsAuthenticated)
            {
                var response = await _user.DeleteCookie(userEmail);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Response.StatusCode = (int)response.Status;
=======

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
>>>>>>> master
            }
            else
            {
                HttpContext.Response.StatusCode = 419;
            }
        }
<<<<<<< HEAD
        [HttpGet]
        [Route("UpdateCookies")]        
        public async Task UpdateCookies()
        {
            var cookies = Request.Headers.SingleOrDefault(header => header.Key == "Cookie").Value.ToString();
            if (cookies == string.Empty)
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
=======
>>>>>>> master

    }
}
