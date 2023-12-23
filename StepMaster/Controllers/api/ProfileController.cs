using Amazon.Runtime.Internal.Util;
using Amazon.S3.Transfer;
using API.Auth.AuthCookie;
using API.Services.ForS3.Configure;

using API.Services.ForS3.Rep;
using Application.Repositories.S3.Interfaces;
using Domain.Entity.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Auth.AuthRequest;
using StepMaster.Auth.ResponseLogic;
using StepMaster.Models.API.UserModel;
using StepMaster.Models.Entity;

using StepMaster.Models.HashSup;

using StepMaster.Services.ForDb.Interfaces;
using System.IO;
using System.Text;
using ZstdSharp.Unsafe;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IAws_Repository _aws;
        private readonly IAppConfiguration _appConfiguration;
        private readonly IUser_Service _user;      
        


        public ProfileController(IAppConfiguration appConfiguration, IUser_Service users, IAws_Repository aws)
        {
            _aws = aws;
            _user = users;
            _appConfiguration = appConfiguration;            
            
        }

        [HttpPut]
        [CustomAuthorizeUser("all")]
        [ModelValidateFilterAttribute]
        [Route("EditUser")]
        public async Task<User> EditUser([FromForm]UserEditModel userEdit)
        {
            var email = User.Identity.Name;
            var baseUser = userEdit.ConvertToBase();
            var response = await _user.EditUser(email,baseUser);
            return  ResponseLogic<User>.Response(Response, response.Status,response.Data); 
        }
        [HttpPost]
        [CustomAuthorizeUser("all")]
        [Route("InsertAvatar")]
        public async Task InsertAvatar([FromForm] IFormFile image)
        {
            var userName = User.Identity.Name;
            
            var response = await _aws.InsertFile(userName, image);
            Response.StatusCode = response?201:500;         
                      
            
        }
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetUser")]
        public async Task<UserResponse> GetUser ()
        {
            var email = User.Identity.Name;
            var response = await _user.GetFullUser(email);            
            return ResponseLogic<UserResponse>.Response(Response, response.Status, new UserResponse(response.Data));
         
        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditPassword")]
        public async Task<UserResponse> EditPassword([FromForm] string newPassword, [FromForm] string oldPassword)
        {
            var email = User.Identity.Name;
            var response = await _user.EditPassword(email, newPassword, oldPassword);           
            return ResponseLogic<UserResponse>.Response(Response, response.Status, new UserResponse(response.Data));


        }
    }
}
