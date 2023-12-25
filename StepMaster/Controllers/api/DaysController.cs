using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using System.IO;
using StepMaster.Services.ForDb.Interfaces;
using API.Auth.AuthCookie;
using Domain.Entity.API;
using Domain.Entity.Main;


namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDays_Service _days;
        public DaysController(IDays_Service days)
        {
            _days = days;
        }
        [HttpGet]
        [CustomAuthorizeUser("user")]
        [Route("GetAllDayUser")]
        public async Task<ResponseList<Day>> GetAllDayUser()
        {

            var email = User.Identity.Name;
            var response = new ResponseList<Day>();
            var responseBody = await _days.GetDaysUserByEmail(email);

            switch (responseBody.Status)
            {
                case MyStatus.Success: Response.StatusCode = (int)responseBody.Status; response.Result = responseBody.Data; return response; ; break;
                case MyStatus.Except: Response.StatusCode = (int)responseBody.Status; return null; break;
                case MyStatus.Exists: Response.StatusCode = (int)responseBody.Status; return null; break;

            }
            return null;

        }
        [HttpPost]
        [CustomAuthorizeUser("user")]
        [Route("SetNewDay")]
        public async Task<Day> SetNewDay([FromForm] Day day)
        {
            
            
            var email = User.Identity.Name;
            day.email = email;
            var response = await _days.SetDayAsync(day, email);
            switch (response.Status)
            {
                case MyStatus.SuccessCreate: Response.StatusCode = (int)response.Status; return response.Data; break;
                case MyStatus.Except: Response.StatusCode = (int)response.Status; return null; break;
                case MyStatus.Exists: Response.StatusCode = (int)response.Status; return null; break;

            }
            return null;

        }
        [HttpPut]
        [CustomAuthorizeUser("user")]
        [Route("UploadDay")]
        public async Task<Day> UploadDay([FromForm] Day day)
        {
            if (day._id == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            else
            {
                

                var email = User.Identity.Name;
                day.email = email;
                var response = await _days.UploadDayAsync(day);
                switch (response.Status)
                {
                    case MyStatus.Success: Response.StatusCode = (int)response.Status; return response.Data; break;
                    case MyStatus.Except: Response.StatusCode = (int)response.Status; return null; break;
                    case MyStatus.NotFound: Response.StatusCode = (int)response.Status; return null; break;
                    case MyStatus.Exists: Response.StatusCode = (int)response.Status; return null; break;

                }
                return null;
            }


        }
       
    }
}
