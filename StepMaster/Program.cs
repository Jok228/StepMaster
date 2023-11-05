using API;
using API.DAL.Entity.SecrurityClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StepMaster.Models.APIDatebaseSet;

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
            builder.Services.AddScoped<IMongoClient>(sp =>
                new MongoClient(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
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