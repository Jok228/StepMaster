using Microsoft.AspNetCore.Mvc;
using StepMaster.Models.Entity.Response;
using StepMaster.Models.Entity;
using StepMaster.Services.Interfaces;

namespace StepMaster.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodiesController : ControllerBase
    {
        private readonly IBodies_Service _bodies;
        public BodiesController(IBodies_Service bodies)
        {
            _bodies = bodies;
        }
        [HttpGet]
        [Route("GetBody")]
        public async Task<Body> GetBody([FromForm]string email)
        {
            var response = await _bodies.GetBodyByEmail(email);
            if (response == null)
            {
                Response.StatusCode = 404;
                return response;
            }
            else
            {
                return response;
            }
        }
        [HttpPost]
        [Route("SetBody")]
        public async Task<Body> SetBody([FromForm] Body newBody)
        {
            var response =  await _bodies.SetEditBody(newBody);
            if (response == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            else
            {
                return response;
            }
        }
    }
}
