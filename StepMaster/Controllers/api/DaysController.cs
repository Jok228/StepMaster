using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using System.IO;
using StepMaster.Services.ForDb.Interfaces;
using API.Auth.AuthCookie;
using Domain.Entity.API;
using Domain.Entity.Main;
using StepMaster.Models.API.Day;
using StepMaster.Auth.AuthRequest;
using Application.Services.Entity.Interfaces_Service;
using StepMaster.HandlerException;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDays_Service _days;
        private readonly ITitles_Services _titlesService;
        private readonly IClan_Service _clanService;
        private readonly IUser_Service _userService;
        public DaysController(IDays_Service days, ITitles_Services titlesService, IClan_Service clanService, IUser_Service userService = null)
        {
            _days = days;
            _titlesService = titlesService;
            _clanService = clanService;
            _userService = userService;
        }
        [HttpGet]
        [ConventionalMiddleware]
        [CustomAuthorizeUser("user")]
        [Route("GetAllDayUser")]
        public async Task<ResponseList<Day>> GetAllDayUser()
        {
            var email = User.Identity.Name;            
            var responseBody = await _days.GetDaysUserByEmail(email);
            await _titlesService.UpdateTitlesList(email);
            return new ResponseList<Day>(responseBody);
           

        }
        [HttpPost]
        [CustomAuthorizeUser("user")]        
        [Route("SetNewDay")]
        public async Task<Day> SetNewDay([FromForm] DayCreate daySet)
        {
            var email = User.Identity.Name;            
            var response = await _days.SetDayAsync(daySet.ConvertToBase(), email);
            await _titlesService.UpdateTitlesList(email);
            await _clanService.UpdateStepsInClanByUser(email);
            if (response != null)
            {
                Response.StatusCode = 201;
            }
            return response;

        }
        [HttpPut]
        [CustomAuthorizeUser("user")]
        [ModelValidateFilterAttribute]
        [Route("UploadDay")]
        public async Task<Day> UploadDay([FromForm] DayResponse dayUpd)
        {
            var email = User.Identity.Name;
            var response = await _days.UploadDayAsync(DayResponse.ConvertToBase(dayUpd));
            await _titlesService.UpdateTitlesList(email);
            await _clanService.UpdateStepsInClanByUser(email);
            return response;
        }
       
    }
}
