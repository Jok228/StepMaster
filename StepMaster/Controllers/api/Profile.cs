using Amazon.Runtime.Internal.Util;
using API.Services.ForS3.Configure;
using API.Services.ForS3.Int;
using API.Services.ForS3.Rep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;
using StepMaster.Models.HashSup;
using StepMaster.Services.AuthCookie;
using StepMaster.Services.ForDb.Interfaces;
using ZstdSharp.Unsafe;
using static Google.Apis.Requests.BatchRequest;

namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Profile : ControllerBase
    {
        private readonly IAws3Services _aws3Services;
        private readonly IAppConfiguration _appConfiguration;
        private readonly IUser_Service _user;



        public Profile(IAppConfiguration appConfiguration, IUser_Service users)
        {
            _user = users;
            _appConfiguration = appConfiguration;
            _aws3Services = new Aws3Services( _appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.BucketName, _appConfiguration.URL);
        }
        [HttpGet]
        [Route("GetIcon")]
        public async Task GetIcon()
        {
            var test = await _aws3Services.GetIcon();

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditPassword")]
        public async Task<User> EditPassword([FromForm] string newPassword, [FromForm] string oldPassword)
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
                    user.password = newPassword;
                    var result = await _user.UpdateUser(user);
                    if(result != null)
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
