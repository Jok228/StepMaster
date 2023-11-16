using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Services.Interfaces;

namespace StepMaster.Controllers.view
{
    [Route("view/[controller]")]
    public class SystemAnswerController : Controller
    {
        private readonly IUser_Service _user;
        
        IMemoryCache _cache;

        public SystemAnswerController(IUser_Service user, IPost_Service post, IMemoryCache cache)
        {
            
            _user = user;
            _cache = cache;
        }
        [HttpGet]
        [Route("AcceptPassword")]
        public async Task<string> AcceptPassword([FromQuery] string email, string password)
        {
            _cache.TryGetValue(password, out var result);
            string response = string.Empty;
            if (result != null)
            {
                var user = await _user.GetByLoginAsync(email);
                user.password = password;
                if (user != null)
                {
                    var state = _user.RecoveryPasswordAsync(user);
                    if (state != null)
                    {
                        response = $"Ok, password was edit. Your new password -> {password}";
                    }
                    else
                    {
                        response = "Fail edit password";
                    }
                }
                else
                {
                    response = "This user not found";
                }
                
            }
            else
            {
                response = "This email not found";
            }

            return response;

        }
    }
}
