using API.Entity.SecrurityClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;
using System.Net;

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
        [BasicAuthorization]
        public Cookie Auth()
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
             
            Cookie answer = new Cookie();
            answer.Name = "username";
            answer.Value = "12345";

            
            return answer;
        }
    }
}
