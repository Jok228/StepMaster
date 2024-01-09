using Amazon.Runtime.Internal.Util;
using Amazon.S3.Transfer;
using API.Auth.AuthCookie;
using API.Services.ForS3.Configure;

using API.Services.ForS3.Rep;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using DnsClient;
using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Auth.AuthRequest;
using StepMaster.Auth.ResponseLogic;
using StepMaster.Models.API.Title;
using StepMaster.Models.API.UserModel;
using StepMaster.Models.Entity;

using StepMaster.Models.HashSup;

using StepMaster.Services.ForDb.Interfaces;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using ZstdSharp.Unsafe;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IAws_Repository _awsRepository;        
        private readonly IUser_Service _usersService;
        private readonly IRating_Service _ratingService;
        private readonly ITitles_Services _titleService;


        public ProfileController(IUser_Service users, IAws_Repository aws, IRating_Service ratingService, ITitles_Services titleService)
        {
            _awsRepository = aws;
            _usersService = users;
            _ratingService = ratingService;
            _titleService = titleService;
        }

        [HttpPut]
        [CustomAuthorizeUser("all")]
        [ModelValidateFilterAttribute]
        [Route("EditUser")]
        public async Task<UserResponse> EditUser([FromForm] UserEditModel newUser)
        {
            var email = User.Identity.Name;
            var oldUserResponse = await _usersService.GetByLoginAsync(email);
            if(oldUserResponse.Status == MyStatus.Success)
            {
                if (oldUserResponse.Data.region_id != newUser.region_id && newUser.region_id != null)
                {
                    await _ratingService.SwapPosition(oldUserResponse.Data.region_id, newUser.region_id, email);
                    
                }
                oldUserResponse.Data = newUser.ConvertToBase(oldUserResponse.Data);
                var updResponse = await _usersService.UpdateUser(oldUserResponse.Data);
                return  ResponseLogic<UserResponse>.Response(Response, updResponse.Status, new UserResponse(updResponse.Data,null,null));
            }
            return ResponseLogic<UserResponse>.Response(Response, MyStatus.NotFound, null);
            
        }
        [HttpPost]
        [CustomAuthorizeUser("all")]
        [Route("InsertAvatar")]
        public async Task InsertAvatar([FromForm] IFormFile image)
        {
            var userName = User.Identity.Name;
            var response = await _awsRepository.InsertFile(userName, image);
            Response.StatusCode = response ? 201 : 500;
        }
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetUser")]
        public async Task<UserResponse> GetUser()
        {
            var email = User.Identity.Name;
            var userResponse = await _usersService.GetByLoginAsync(email);
            if ( userResponse.Status == MyStatus.Success)
            {
                var avatarLink = await _awsRepository.GetUserAvatarLink(email);
                var rating = await _ratingService.GetUserRanking(email, userResponse.Data.region_id);
                await _titleService.UpdateTitlesList(email);
                return ResponseLogic<UserResponse>.Response(Response, userResponse.Status, new UserResponse(userResponse.Data,rating,avatarLink));
            }
            return ResponseLogic<UserResponse>.Response(Response, userResponse.Status,null);
        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditPassword")]
        public async Task<UserResponse> EditPassword([FromForm] string newPassword, [FromForm] string oldPassword)
        {
            var email = User.Identity.Name;
            var response = await _usersService.EditPassword(email, newPassword, oldPassword);
            if(response.Status == MyStatus.Success)
            {
                return ResponseLogic<UserResponse>.Response(Response, response.Status, new UserResponse(response.Data,null,null));
            }
           
            return ResponseLogic<UserResponse>.Response(Response, response.Status, null);
            

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("SetSelectedTitle")]
        public async Task<List<TitleDb>> SetSelectedTitles([FromForm] TitleDb newTitle)
        {
            if (newTitle.type != "achievement") throw new HttpRequestException("The selected title must be only type - achievement!",null,HttpStatusCode.BadRequest);
            var email = User.Identity.Name;
            var result = await _titleService.UpdateSelectUserTitles(email, newTitle);
            return result.Data;
        }
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetTitleProgress")]
        public async Task<TitleProgress> GetTitleProgress()
        {
            var email = User.Identity.Name;
            return await _titleService.GetActualProgress(email);
        }


    }
}
