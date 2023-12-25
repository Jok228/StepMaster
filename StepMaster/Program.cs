using API;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using StepMaster.Services.ForDb.Interfaces;
using API.Auth.AuthBase;
using Application.Services.ForDb.APIDatebaseSet;
using System.Runtime.CompilerServices;
namespace StepMaster
{
    public class Program
    {
     

        public async static Task Main(string[] args)
        {
          
            var builder = WebApplication.CreateBuilder(args);
            
            var services = builder.Services;
            services
       .AddAuthentication(o =>
       {    
           o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       })
       .AddCookie();

            builder.Services.Configure<ApiDatabaseSettings>(
<<<<<<< HEAD
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));         

            
            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));
=======
                builder.Configuration.GetSection(nameof(ApiDatabaseSettings)));
            // Add services to the container.
            
            builder.Services.AddScoped<IAPIDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ApiDatabaseSettings>>().Value);
            //var cert = new X509Certificate2("certificates/root.crt");
            

            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString")));
            Console.Write(builder.Configuration.GetValue<string>("APIDatabaseSettings:ConnectionString"));
            //clientSettings.UseTls = true;


            //clientSettings.SslSettings.CheckCertificateRevocation = false;

            //clientSettings.SslSettings = new SslSettings
            //{

            //    CheckCertificateRevocation = true,
            //    //ClientCertificates = new[] { cert },

            //};
            //clientSettings.VerifySslCertificate = false;


            builder.Services.AddScoped<IMongoClient>(sp =>
                new MongoClient(clientSettings));
>>>>>>> master
           
            

            


            var nameDataBase = builder.Configuration.GetValue<string>("APIDatabaseSettings:DatabaseName");
            

            builder.Services.AddControllers();           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAunteficationHandler>("Basic", null);
            

            var mongoClient = ScopeBuilder.InitializerServices(builder.Services, builder);

            var app = builder.Build();

<<<<<<< HEAD
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
=======
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

>>>>>>> master
            app.UseAuthorization();
            app.MapControllers();

            var myAppStart = new StartinItialisation(app);
            await myAppStart.Start();

            app.Run();
            
        }
 
    }
}