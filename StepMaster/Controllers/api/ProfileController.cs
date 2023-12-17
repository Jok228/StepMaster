using Amazon.Runtime.Internal.Util;
using Amazon.S3.Transfer;
using API.Auth.AuthCookie;
using API.Services.ForS3.Configure;
using API.Services.ForS3.Int;
using API.Services.ForS3.Rep;
using Application.Services.ForDb.Interfaces;
using Domain.Entity.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IAws3Services _aws3Services;
        private readonly IAppConfiguration _appConfiguration;
        private readonly IUser_Service _user;
        private readonly IRating_Service _rating;
        private string _pathUserAwatar = "/Icons/Avatar";


        public ProfileController(IAppConfiguration appConfiguration, IUser_Service users, IRating_Service rating)
        {
            _user = users;
            _appConfiguration = appConfiguration;
            _rating = rating;
            _aws3Services = new Aws3Services( _appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.BucketName, _appConfiguration.URL);
        }

        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditUser")]
        public async Task<UserResponse> EditUser([FromForm]UserResponse user)
        {
            var email = User.Identity.Name;
            var response = await _user.EditUser(email,user);
            switch(response.Status)
            {
                case MyStatus.Success:
                     Response.StatusCode = (int)response.Status;
                    return response.Data;
                        
                case MyStatus.Except:  
                    Response.StatusCode = (int)response.Status;
                    return null;
                case MyStatus.NotFound:
                    Response.StatusCode = (int)response.Status;
                    return null;
                case MyStatus.BadRequest:
                    Response.StatusCode = (int)response.Status;
                    return null;
            }
            return null;


        }
        [HttpPost]
        [CustomAuthorizeUser("all")]
        [Route("InsertAvatar")]
        public async Task InsertAvatar([FromForm] IFormFile image)
        {
            var userName = User.Identity.Name;
            var fullPath = userName + _pathUserAwatar;
            var response = await _aws3Services.InsertFile(fullPath, image);
            switch(response.Status)             
            {
                case MyStatus.Success:
                    Response.StatusCode = (int)response.Status;
                    break;
                case MyStatus.Except:
                    Response.StatusCode = (int)response.Status;
                    break;

            }
            
            
        }
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetUser")]
        public async Task<UserResponse> GetUser ()
        {
            var userName = User.Identity.Name;
            var path = userName + _pathUserAwatar;
            var user = new UserResponse(_user.GetByLoginAsync(userName).Result.Data);
            var link = await _aws3Services.GetLink(path);
            var ratingUser = await _rating.GetRatingUser(user.email,user.region_id);
            if(user != null)
            {
                user.rating = ratingUser;
                user.avatarLink = link.Data;
                return user;
            }
            else
            {
                Response.StatusCode = 500;
                return null;
            }

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditPassword")]
        public async Task<UserResponse> EditPassword([FromForm] string newPassword, [FromForm] string oldPassword)
        {
            var name = User.Identity.Name;
            var checkPassword = await _user.CheckPassword(name, oldPassword);
            switch (checkPassword.Status)
            {
                case MyStatus.Except:
                    Response.StatusCode = (int)checkPassword.Status;
                    break;
                case MyStatus.Unauthorized: 
                    Response.StatusCode = (int)checkPassword.Status;
                    break;
                case MyStatus.Success:
                    newPassword = HashCoder.GetHash(newPassword);
                    var user = await _user.GetByLoginAsync(name);
                    user.Data.password = newPassword;
                    var result = new UserResponse(await _user.UpdateUser(user.Data));
                    if (result != null)
                    {
                        Response.StatusCode = 200;
                        return result;
                    }
                    else
                    {
                        Response.StatusCode = 500;
                        return null;
                    }
                    break;
            }
            return null;

        }
    }
}
