using Amazon.Runtime.Internal.Util;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Amazon.S3.Transfer;
using API.Auth.AuthCookie;
using API.Services.ForS3.Configure;

using API.Services.ForS3.Rep;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using DnsClient;
using Domain.Entity.API;
using Domain.Entity.Main;
using Domain.Entity.Main.Titles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Auth.AuthRequest;
using StepMaster.HandlerException;
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
using static Domain.Entity.Main.Titles.Condition;
using static StepMaster.Models.Entity.User;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IAws_Repository _awsRepository;        
        private readonly IUser_Service _usersService;
        private readonly IClan_Repository _clanRepository;
        private readonly IClan_Service _clanService;
        private readonly IRating_Service _ratingService;
        private readonly ITitles_Services _titleService;

        public ProfileController(IUser_Service users, IAws_Repository aws, IRating_Service ratingService, ITitles_Services titleService, IClan_Repository clanRepository, IClan_Service clanService = null)
        {
            _awsRepository = aws;
            _usersService = users;
            _ratingService = ratingService;
            _titleService = titleService;
            _clanRepository = clanRepository;
            _clanService = clanService;
        }

        [HttpPut]
        
        [CustomAuthorizeUser("all")]
        [ModelValidateFilterAttribute]
        [Route("EditUser")]
        public async Task<UserLiteResponse> EditUser([FromForm] UserEditModel newUser)
        {
            
            var email = User.Identity.Name;            
            var oldUserResponse = await _usersService.GetByLoginAsync(email);            
                if (oldUserResponse.RegionId != newUser.region_id && newUser.region_id != null)
                {
                    await _ratingService.SwapPosition(oldUserResponse.RegionId, newUser.region_id, email);                    
                }
                oldUserResponse = newUser.ConvertToBase(oldUserResponse);
                var updResponse = await _usersService.UpdateUser(oldUserResponse);
            return new UserLiteResponse(updResponse);        
            
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
        public async Task<UserLargeResponse> GetUser()
        {
            var email = User.Identity.Name;
            return await  GetLargeUserPrivate(email);
            
        }
        private async Task<UserLargeResponse> GetLargeUserPrivate(string email)
        {
            var userResponse = await _usersService.GetByLoginAsync(email);
            var clanId = _clanRepository.GetClanByUser(email).Result._id;
            var avatarLink = await _awsRepository.GetUserAvatarLink(email);
            var rating = await _ratingService.GetUserRanking(email, userResponse.RegionId, clanId);
            var friendsList = await GetFriendsHide(userResponse.Friends);
            await _titleService.UpdateTitlesList(email);
            return new UserLargeResponse(userResponse, rating, avatarLink, friendsList);
        }
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetUserByEmail")]
        public async Task GetUserByEmail([FromForm]string email, [FromForm] int typeId)
        {     
            var typeEnum = (GetUserType)Enum.ToObject(typeof(GetUserType), typeId);
            if(typeEnum == GetUserType.LiteVer)
            {
                var userResponse = await _usersService.GetByLoginAsync(email);
                Response.ContentType = "application/json";
                await Response.WriteAsJsonAsync(new UserLiteResponse(userResponse));
            }
            else if  (typeEnum == GetUserType.LargeVer)
            {  
                Response.ContentType = "application/json";
                await Response.WriteAsJsonAsync(await GetLargeUserPrivate(email));
            }
            else
            {
                throw new HttpRequestException("Bad code Type. Type is must be 0 or 1",null,HttpStatusCode.BadRequest);
            }
            
        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("EditPassword")]
        public async Task<UserLiteResponse> EditPassword([FromForm] string newPassword, [FromForm] string oldPassword)
        {
            var email = User.Identity.Name;            
            var response = await _usersService.EditPassword(email, newPassword, oldPassword);
            return new UserLiteResponse(response);           

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("SetSelectedTitle")]
        public async Task<UserLiteResponse> SetSelectedTitles([FromForm] string conditionMongoId)
        {
            var email = User.Identity.Name;

            await _titleService.UpdateSelectUserTitles(email, conditionMongoId);
            var result = await _usersService.GetByLoginAsync(email);
            return new UserLiteResponse(result);
        }

        #region Friends system
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("RemoveRequest")]
        public async Task<UserLiteResponse> RemoveRequest([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.RemoveRequest(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("CreateRequestInFriend")]
        public async Task<UserLiteResponse> CreateRequestInFriend([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.CreateRequestInFriend(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("AcceptRequest")]
        public async Task<UserLiteResponse> AcceptRequest([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.AcceptRequest(email,targetEmail);
            return new UserLiteResponse(responseUser);
        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("DenyRequest")]
        public async Task<UserLiteResponse> DenyRequest([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.DenyRequest(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("AddInBlockedList")]
        public async Task<UserLiteResponse> AddInBlockedList([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.AddInBlockedList(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("RemoveFromBlockedList")]
        public async Task<UserLiteResponse> RemoveFromBlockedList([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.RemoveFromBlockedList(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        [HttpPut]
        [CustomAuthorizeUser("all")]
        [Route("RemoveFriend")]
        public async Task<UserLiteResponse> RemoveFriend([FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
            if (email == targetEmail)
            {
                throw new HttpRequestException("emails not can be equals", null, HttpStatusCode.BadRequest);
            }
            var responseUser = await _usersService.RemoveFriend(email, targetEmail);
            return new UserLiteResponse(responseUser);

        }
        #endregion


        #region Friedns Views
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetFriends")]
        public async Task<ResponseList<UserFriendResponse>> GetFriends()
        {
            var mainUser = await _usersService.GetByLoginAsync(User.Identity.Name);
            
            var requestList = await GetUserFriendByList(mainUser.Friends);
            return new ResponseList<UserFriendResponse>(requestList);
        }

        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetRequestList")]
        public async Task<ResponseList<UserFriendResponse>> GetRequestList()
        {
            var mainUser = await _usersService.GetByLoginAsync(User.Identity.Name);
            var requestList = await GetUserFriendByList(mainUser.RequrequestInFriends);
            return new ResponseList<UserFriendResponse>(requestList);
        }
        private async Task<List<UserFriendResponse>> GetUserFriendByList(List<string> listUsers)
        {
            var response = new List<UserFriendResponse>();
            foreach (var emailUser in listUsers)
            {

                var user = await _usersService.GetByLoginAsync(emailUser);
                var avatarLink = await _awsRepository.GetUserAvatarLink(emailUser);
                var userResponse = new UserFriendResponse(user);
                userResponse.avatarLink = avatarLink;
                userResponse.idActualTitle = await _usersService.GetActualAchievementUser(emailUser);
                response.Add(userResponse);

            }
            response = response.OrderBy(a => a.isOnline).Reverse().ToList();
            return response;
        }

        #endregion

        #region Search Users
        [HttpGet]
        [CustomAuthorizeUser("all")]
        [Route("GetUsers")]
        public async Task<ResponseList<UserSearchResponse>> GetUsers([FromQuery]string? searchText, [FromQuery] bool myRegion, [FromQuery] int page)
        {
            if(searchText == null)
            {
                searchText = string.Empty;
            }
            var mainUser = await _usersService.GetByLoginAsync(User.Identity.Name);
            if(myRegion)
            {
                var listUsers = await _usersService.GetUsers(searchText, page, mainUser.RegionId);
                var response = SetRelativeList(mainUser, listUsers);
                response = await SetAvatarLinks(response);
                return new ResponseList<UserSearchResponse>(response);
            }
            else
            {
                var listUsers = await _usersService.GetUsers(searchText, page,null);
                var response = SetRelativeList(mainUser, listUsers);
                response = await SetAvatarLinks(response);                
                return new ResponseList<UserSearchResponse>(response);
            }           

        }
        private async Task<List<UserSearchResponse>> SetAvatarLinks(List<UserSearchResponse> users)
        {
            foreach(var user in users)
            {
                user.avatarLink = await _awsRepository.GetUserAvatarLink(user.email);
            }
            return users;
        }
        private List<UserSearchResponse> SetRelativeList(User user, List<User> listUsers)
        {
            var response = new List<UserSearchResponse>();
            SetRelativeOfGroup(response,user.Friends,listUsers,Relative.Friend);
            SetRelativeOfGroup(response, user.BlockedUsers, listUsers, Relative.Ban);
            SetRelativeOfGroup(response, user.RequrequestInFriends, listUsers, Relative.Request);
            
            foreach (var haveStatus in response)
            {
                foreach(User itemUser in listUsers)
                {
                    if (itemUser.Email == haveStatus.email)
                    {
                        listUsers.Remove(itemUser);
                        break;
                    }
                }
            }
            foreach(User requestUser in listUsers)
            {
                response.Add(new UserSearchResponse(user, Relative.None));
            }          
            
                
            
            response.OrderBy(user => user.relative);
            return response;
        }
        private List<UserSearchResponse> SetRelativeOfGroup(List<UserSearchResponse> listResponse,List<string> listEmails, List<User> users,Relative relative)
        {
            foreach(var emailUser in listEmails)
            {
                foreach (User userDb  in users)
                {
                    if (userDb.Email == emailUser)
                    {
                        listResponse.Add(new UserSearchResponse(userDb, relative));
                    }
                }
            }
            return listResponse;
        }

        #endregion
        private async Task <List<UserFriendHideResponse>> GetFriendsHide(List<string> friends)
        {
            var response = new List<UserFriendHideResponse>();
            foreach (var userEmail in friends)
            {
                var userFriend = await _usersService.GetByLoginAsync(userEmail);
                response.Add(new UserFriendHideResponse(userFriend));
            }
            return response;
        }
        private enum GetUserType
        {
            LiteVer = 0,
            LargeVer = 1,
        }
    }
}
