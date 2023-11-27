using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;
using System.Net;
using System.Security;
using System.Security.Claims;



namespace StepMaster.Services.AuthCookie
{
    public sealed class CustomAuthorizeUserAttribute: Attribute, IAuthorizationFilter 
    {       
       private string _option;
       public CustomAuthorizeUserAttribute( string option) 
        {
         _option = option;
        }    
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var role = string.Empty;
                try
                {
                    role = context.HttpContext.User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                }
                catch
                {
                    context.Result = new ObjectResult("Not authorized")
                    {
                        
                    StatusCode = (int)HttpStatusCode.Forbidden
                        
                    };
                    return;
                }

                if (_option == "all")
                {
                    return;
                }
                else
                {
                    if (role == _option )
                    {
                        return;
                    }
                    else
                    {
                        context.Result = new ObjectResult("Not authorized")
                        {
                            StatusCode = (int)HttpStatusCode.Forbidden

                        };
                    }
                }
            }
        }
    }


 
}
