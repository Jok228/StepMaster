using API.Services.ForS3.Configure;
using API.Services.ForS3.Rep;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Repositories.S3.Interfaces;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.Entity.Realization_Services;
using Application.Services.FIreBase.Interfaces;
using Application.Services.FIreBase.Realization;
using Application.Services.ForDb.APIDatebaseSet;

using Application.Services.Post.Repositories;
using Application.Services.Post.Services;
using Infrastructure.MongoDb.Cache.Implementation;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.Repositories;


using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StepMaster.HandlerException;
using StepMaster.Models.Entity;
using StepMaster.Services.ForDb.Interfaces;
using StepMaster.Services.ForDb.Repositories;
using System.Threading.RateLimiting;

namespace StepMaster.Initialization.Scope
{
    public static class ScopeBuilder
    {

        public static void InitializerService(IServiceCollection service)
        {
            service.AddTransient<FactoryMiddleware>();         
            service.AddScoped<ITitles_Services, Titles_Service>();
            service.AddScoped<IFireBase_Service, FireBase_Service>();
            service.AddScoped<IRating_Service, Rating_Service>();
            service.AddScoped<IRegion_Service, RegionRep>();
            service.AddScoped<IUser_Service, User_Service>();
            service.AddScoped<IDays_Service, Days_Service>();
            service.AddScoped<IClan_Service, Clan_Service>();
            service.AddScoped<IPost_Service, Post_Service>();
        }
        public static void InitializerRepsitories(IServiceCollection service)
        {
            service.AddScoped<ICondition_Repository, Condition_Repository>();
            service.AddScoped<IDay_Repository, Day_Repository>();
            service.AddScoped<IClan_Repository, Clan_Repository>();
            service.AddScoped<IRegion_Repository, Region_Repository>();
            service.AddScoped<IUser_Repository, User_Repository>();
            service.AddScoped<IRating_Repository, Rating_Repository>();
            service.AddScoped<IMy_Cache, My_Cache_Implementate>();
        }
        public static void InitializerAws(IServiceCollection service)
        {
            service.AddSingleton<IAppConfiguration, AppConfiguration>();
            service.AddScoped<IAws_Repository, Aws_Repository>();
        }
        public static MongoClient InitializerDb(IServiceCollection service, WebApplicationBuilder builder)
        {
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
            var mongoClient = new MongoClient(clientSettings);
            service.AddSingleton<IAPIDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<ApiDatabaseSettings>>().Value);            
            service.AddSingleton<IMongoClient>(sp =>
                mongoClient);

            return mongoClient;
        }
    }

}
