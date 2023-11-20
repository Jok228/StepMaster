using API;
using API.DAL.Entity.SecrurityClass;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.AspNetCore3;
namespace StepMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            services
       .AddAuthentication(o =>
       {
           
           o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
         
           o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
        
           o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       })
       .AddCookie()
       .AddGoogleOpenIdConnect(options =>
       {
           options.ClientId = "434328467243-1hb02jpg6131qdrfamll69a72ajsgt79.apps.googleusercontent.com";
           options.ClientSecret = "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";
           
       });

            builder.Services.Configure<ApiDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));
            // Add services to the container.
            
            builder.Services.AddScoped<IAPIDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ApiDatabaseSettings>>().Value);
            //var cert = new X509Certificate2("certificates/root.crt");
            

            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));
           


            builder.Services.AddScoped<IMongoClient>(sp =>
                new MongoClient(clientSettings));
           
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAunteficationHandler>("Basic", null);
            

            ScopeBuilder.InitializerServices(builder.Services);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

           
            app.UseHttpsRedirection();     

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}