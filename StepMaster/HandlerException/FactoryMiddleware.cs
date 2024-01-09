using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Text.Json;

namespace StepMaster.HandlerException
{
    public class FactoryMiddleware:IMiddleware
    {
    
        //private readonly ILogger _logger;
        public FactoryMiddleware( )
        {        
            //_logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //_logger.LogInformation("Before request");
            try
            {
                await next(context);
            }
            catch (HttpRequestException ex)
            {
                if(ex.StatusCode != null)
                {
                    context.Response.StatusCode = (int)ex.StatusCode;
                    context.Response.Headers.ContentLanguage = "ru-RU";
                    context.Response.Headers.ContentType = "text/plain; charset=utf-8";
                    context.Response.Headers.Append("secret-id", "256");
                    await context.Response.WriteAsync(ex.Message);
                }

            }
           

         
        }

      
    }
}
