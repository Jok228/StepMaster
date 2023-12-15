using DnsClient;
using Domain.Entity.Main;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UpdateDateBase.Service.Rep;

namespace UpdateDateBase
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("I begin work 1.4");
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://root:password@mongodb:27017/"));
            var mongoClient = new MongoClient(clientSettings);
            var start = new AllOfTime_Rep(mongoClient);
            while (true)
            {
                await start.UpdateRating();
                Console.WriteLine("Copleted");
                System.Threading.Thread.Sleep(60000);
            }
        }
    }
   
}
