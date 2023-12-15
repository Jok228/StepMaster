using Application.Services.Post.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StepMaster.Models.HashSup;
using StepMaster.Services.ForDb.Interfaces;

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
            _cache.TryGetValue(email + password, out var result);
            string response = string.Empty;
            if (result != null)
            {
                var user = await _user.GetByLoginAsync(email);                
                
                if (user.Data != null)
                {
                    user.Data.password = HashCoder.GetHash(password);
                    var state = _user.UpdateUser(user.Data);
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
