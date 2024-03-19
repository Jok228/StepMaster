using API.Auth.AuthBase;
using Application.Services.ForDb.APIDatebaseSet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using StepMaster.Controllers.Hubs;
using StepMaster.HandlerException;
using StepMaster.Initialization.FireBase;
using StepMaster.Initialization.Scope;
using System.Text;
namespace StepMaster
{
    public class Program
    {


        public async static Task Main(string[] args)
        {
            Console.WriteLine("Relise 1.017 Chats 1.3");
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            services
       .AddAuthentication(o =>
       {
           o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       })
       .AddCookie();
            builder.Host.UseNLog();
            ReadFireBaseAdminSDK.ReadFireBaseAdminSdk();
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Info("Process start");
            builder.Services.Configure<ApiDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));


            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));






            var nameDataBase = builder.Configuration.GetValue<string>("APIDatabaseSettings:DatabaseName");


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMemoryCache();
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAunteficationHandler>("Basic", null);
            var mongoClient = ScopeBuilder.InitializerDb(builder.Services, builder);
            ScopeBuilder.InitializerService(services);
            ScopeBuilder.InitializerRepsitories(services);
            ScopeBuilder.InitializerAws(services);

            services.AddSignalR ();

            var app = builder.Build();
            app.UseHttpsRedirection ();


            app.UseHttpsRedirection ();

            app.UseSwagger();
            app.UseMiddleware<FactoryMiddleware>();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.MapHub<ChatHub> ("ChatHub");
            app.UseAuthorization();
            app.UseAuthentication ();
            app.UseWebSockets();
            app.MapControllers();
            app.Run();

        }

    }
}