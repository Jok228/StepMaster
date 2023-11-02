
using API;
using API.DAL.Entity.APIDatebaseSet;
using API.DAL.Entity.SecrurityClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace StepMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<APIDatabaseSettings>(
                    builder.Configuration.GetSection(nameof(APIDatabaseSettings)));
            // Add services to the container.
            builder.Services.AddScoped<IAPIDatabaseSettings>(sp =>
                     sp.GetRequiredService<IOptions<APIDatabaseSettings>>().Value);
            builder.Services.AddScoped<IMongoClient>(sp =>
                new MongoClient(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
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