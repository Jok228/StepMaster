
using API.Services.ForS3.Configure;
using Application.Services.ForDb.APIDatebaseSet;
using Application.Services.ForDb.Interfaces;
using Application.Services.ForDb.Repositories;
using Application.Services.Post.Repositories;
using Application.Services.Post.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StepMaster.Services.ForDb.Interfaces;
using StepMaster.Services.ForDb.Repositories;
using System.Threading.RateLimiting;

namespace API
{
    public static class ScopeBuilder 
    {
        public static MongoClient InitializerServices(this IServiceCollection service, WebApplicationBuilder builder)
        {
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
            var mongoClient = new MongoClient(clientSettings);


            service.AddSingleton<IAPIDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<ApiDatabaseSettings>>().Value);
            service.AddScoped<IRegion_Service, RegionRep>();
            service.AddSingleton<IAppConfiguration, AppConfiguration>();
            service.AddSingleton<IMongoClient>(sp =>
                mongoClient);

            service.AddSingleton<IAllOfTime_Service, AllOfTime_Rep>();
            service.AddScoped<IUser_Service, UserRep>();
            service.AddScoped<IRating_Service, Rating_Rep>();
            service.AddScoped<IDays_Service, DaysRep>();
            service.AddScoped<IPost_Service, PostRep>();
            return mongoClient;
        }
        public static void InitializerRateLimiter(this IServiceCollection service)
        {
            service.AddRateLimiter(opt => opt.AddConcurrencyLimiter("ForFile", parametrs =>
            {
                parametrs.PermitLimit = 5;
                parametrs.QueueLimit = 5;
                parametrs.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
            }).RejectionStatusCode = 423);
            service.AddRateLimiter(opt => opt.AddConcurrencyLimiter("ForOther", parametrs =>
            {
                parametrs.PermitLimit = 10;
                parametrs.QueueLimit = 25;
                parametrs.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
            }).RejectionStatusCode = 423);

        }
    }
    
}
