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

namespace StepMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<ApiDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));
            // Add services to the container.
            builder.Services.AddScoped<IAPIDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ApiDatabaseSettings>>().Value);
            //var cert = new X509Certificate2("certificates/root.crt");

            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));
            clientSettings.UseTls = true;


            clientSettings.SslSettings.CheckCertificateRevocation = false;

            clientSettings.SslSettings = new SslSettings
            {

                CheckCertificateRevocation = true,
                //ClientCertificates = new[] { cert },

            };
            clientSettings.VerifySslCertificate = false;


            builder.Services.AddScoped<IMongoClient>(sp =>
                new MongoClient(clientSettings));
           
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAunteficationHandler>("Basic", null);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                });

            ScopeBuilder.InitializerServices(builder.Services);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}