using Microsoft.AspNetCore.Authorization;

namespace StepMaster.Services.AuthBase
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = "Basic";
        }
    }
}
