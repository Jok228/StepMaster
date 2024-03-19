using API.Auth.AuthCookie;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Domain.Entity.API;
using Domain.Entity.Main;
using Domain.Entity.Main.Room;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StepMaster.HandlerException;
using StepMaster.Models.API.ClanModel;
using StepMaster.Services.ForDb.Interfaces;
using StepMaster.Services.ForDb.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClanController : ControllerBase
    {
        private readonly IDay_Repository _daysRepository;
        private readonly IClan_Service _clanService;
 
        public readonly IUser_Service _userService;
        private readonly IRegion_Service _regionService;

        public ClanController(IClan_Service clanService,IUser_Service userService,IRegion_Service regionService,IDay_Repository daysRepository)
            {
            _clanService = clanService;
            _userService = userService;
            _regionService = regionService;
            _daysRepository = daysRepository;
            }

        [HttpGet]
        [Route("GetAll")]
        [CustomAuthorizeUser("all")]
        
        public async Task<ResponseClanGetAllModel> GetClans([FromForm] int type, [FromForm] int numberPage, [FromForm] [AllowNull] string? searchName)
        {            
            if(searchName == null)
            {
                searchName = string.Empty;  
            }
            var email = User.Identity.Name;
            var user = await _userService.GetByLoginAsync(email);
            var region = await _regionService.GetRegionById(user.RegionId);
            var typeSort = (Clan.SortType)Enum.ToObject(typeof(Clan.SortType), type);
            var result = await _clanService.GetAllClansBySortType(searchName, typeSort, region.fullName, numberPage);

            return new ResponseClanGetAllModel()
            {
                Result = result
            };
        }
        [HttpGet]
        [ConventionalMiddleware]
        [Route("GetClanById")]
        [CustomAuthorizeUser("all")]

        public async Task<Clan> GetClanById([FromForm] string mongoId)
        {
            return await _clanService.GetClan(mongoId);
        }
        [HttpPut]
        [Route("JoinUser")]
        [CustomAuthorizeUser("all")]
        public async Task<Clan> JoinUser([FromForm] string mongoIdClan)
        {
           var email = User.Identity.Name;
           return await  _clanService.AddUser(mongoIdClan, email);
        }
        [HttpPut]
        [Route("LeaveUser")]
        [CustomAuthorizeUser("all")]
        public async Task<Clan> LeaveUser([FromForm] string mongoIdClan)
        {            
            var email = User.Identity.Name;
            return await _clanService.LeaveUser(mongoIdClan, email);
        }
        [HttpPut]
        [Route("KickOutUser")]
        [CustomAuthorizeUser("all")]
        public async Task<Clan> KickOutUser([FromForm] string mongoIdClan, [FromForm] string targetEmail)
        {
            var email = User.Identity.Name;
           
            if (await _clanService.CheckOwner(mongoIdClan, email))
            {
                if (email == targetEmail)
                {
                    throw new HttpRequestException("Owner can not remove himself", null, HttpStatusCode.Unauthorized);
                }
                return await _clanService.LeaveUser(mongoIdClan, targetEmail);
            }
            else
            {
                throw new HttpRequestException("You is not ownder this clan", null, HttpStatusCode.Unauthorized);
            }
        }
        [HttpDelete]
        [Route("DeleteClan")]
        [CustomAuthorizeUser("all")]
        public async Task DeleteClan([FromForm] string mongoIdClan)
        {
            var email = User.Identity.Name;
            if (await _clanService.CheckOwner(mongoIdClan, email))
            {
                await _clanService.DeleteClan(mongoIdClan);
                Response.StatusCode = 204;
            }
            else
            {
                throw new HttpRequestException("You is not ownder this clan", null, HttpStatusCode.Unauthorized);
            }
        }
        [HttpPost]
        [Route("CreateClan")]
        [CustomAuthorizeUser("all")]
        public async Task<Clan> CreateNewClan([FromForm]ClanCreateModel newClan)
        {
            var email = User.Identity.Name;
            var user = await _userService.GetByLoginAsync(email);
            var region = await _regionService.GetRegionById(user.RegionId);
            if (user.VipStatus == false)
            {
                throw new HttpRequestException("User is not having a vip status", null, HttpStatusCode.Unauthorized);
            }
           var steps = await _daysRepository.GetAllStepsUser(email,null);
           var clan = newClan.ConvertToClan (email,steps,user.NickName,region.fullName);
           return await _clanService.SetNewClan(clan);
        }

    }
}
