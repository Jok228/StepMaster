using StepMaster.Services.ForDb.Interfaces;
using System.Diagnostics;

namespace StepMaster
{
    public class StartinItialisation
    {
        private readonly WebApplication _app;

        public StartinItialisation(WebApplication applicationBuilder)
        {
            _app = applicationBuilder;
        }
        public async Task Start() 
        {
            //var startTimeSpan = TimeSpan.Zero;
            //var periodTimeSpan = TimeSpan.FromSeconds(5);
            //int count = 0;
            //var service = _app.Services.GetService<IAllOfTime_Service>();
            //new Timer (async ( e) =>
            //{
            //   count++;
            //   Console.WriteLine("I update Rating" + $" count: {count}");
               
            //   //Process.Start("app/Update/Update/UpdateDateBase.exe");
            //}, null, startTimeSpan, periodTimeSpan);
            
                 
        }

    }
}
