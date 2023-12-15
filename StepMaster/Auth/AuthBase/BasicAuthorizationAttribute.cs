using Microsoft.AspNetCore.Authorization;

namespace API.Auth.AuthBase
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = "Basic";
        }
    }
}
