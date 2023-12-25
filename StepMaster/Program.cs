using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using StepMaster.Services.ForDb.Interfaces;
using API.Auth.AuthBase;
using Application.Services.ForDb.APIDatebaseSet;
using System.Runtime.CompilerServices;
using StepMaster.Initialization.Scope;
namespace StepMaster
{
    public class Program
    {


        public async static Task Main(string[] args)
        {
            Console.WriteLine("Ver 1.010 postman test and update rating services debug 1.0");
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            services
       .AddAuthentication(o =>
       {
           o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       })
       .AddCookie();

            builder.Services.Configure<ApiDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));


            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));






            var nameDataBase = builder.Configuration.GetValue<string>("APIDatabaseSettings:DatabaseName");


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAunteficationHandler>("Basic", null);


            var mongoClient = ScopeBuilder.InitializerDb(builder.Services, builder);
            ScopeBuilder.InitializerService(services);
            ScopeBuilder.InitializerRepsitories(services);
            ScopeBuilder.InitializerAws(services);


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }

    }
}