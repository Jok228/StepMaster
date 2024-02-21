using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StepMaster.Services.ForDb.Interfaces;
using System.Net;
using System.Text.Json;

namespace StepMaster.HandlerException
{
    public class FactoryMiddleware:IMiddleware
    {
        private readonly IUser_Service _userService;

        public FactoryMiddleware(IUser_Service userService = null)
        {
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //_logger.LogInformation("Before request");
            try
            {
                context.Response.Headers.Add("Version","Relise 1.016");
                if (context.Request.Headers.ContainsKey("IsOnline"))
                {
                    context.Request.Headers.TryGetValue("Cookie", out var newCookies);
                    var user = await _userService.GetUserbyCookie(newCookies);
                    user.LastBeOnline = DateTime.UtcNow;
                    await _userService.UpdateUser(user);
                }
                
                await next(context);
                
            }
            catch (HttpRequestException ex)
            {
                if(ex.StatusCode != null)
                {
                    context.Response.StatusCode = (int)ex.StatusCode;
                    context.Response.Headers.ContentLanguage = "ru-RU";
                    context.Response.Headers.ContentType = "text/plain; charset=utf-8";                    
                    await context.Response.WriteAsync(ex.Message);
                }

            }
           

         
        }

      
    }
    public class ConventionalMiddleware : Attribute,IAsyncActionFilter
    {        

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            return next();
        }

        


    }
}
