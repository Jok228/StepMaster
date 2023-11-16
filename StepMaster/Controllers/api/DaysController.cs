using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;
using StepMaster.Services.Interfaces;

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
        [Route("GetAllDayUser")]
        public async Task<ResponseList<Day>> GetAllDayUser([FromForm]string email)
        {
            var response = new ResponseList<Day>();
            var list = await _days.GetDaysUserByEmail(email);
            if (list == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                response.Result = list;
                return response;
            }

        }
        [HttpPost]
        
        [Route("SetNewDay")]
        public async Task<Day> SetNewDay([FromForm]Day day)
        {
            var response = await _days.SetDayAsync(day);
            return response;

        }
    }
}
