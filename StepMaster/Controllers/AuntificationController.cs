using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuntificationController : ControllerBase
    {
        private readonly IUser_Service _user;

        
        public AuntificationController(IUser_Service user)
        {
            _user = user;
        }
        [HttpGet]
        [Route("Auth")]
        public async Task<List<User>> Auth()
        {            
            return await _user.GetAllUser();
        }
    }
}
