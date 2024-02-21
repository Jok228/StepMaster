using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.Main;
using Application.Repositories.Db.Interfaces_Repository;

namespace StepMaster.Services.ForDb.Repositories
{
    public class Days_Service : IDays_Service
    {
        private readonly IDay_Repository _days;     

        public Days_Service(IDay_Repository days)
        {

            _days = days;         
          
        }
        public async Task<List<Day>> GetDaysUserByEmail(string email)
        {
            return await _days.GetDaysByEmail(email);
        }

        public async Task<Day> SetDayAsync(Day day, string email)
        {
            day.email = email;
            day.date = DateTime.UtcNow.Date;
            if (await _days.ChechDayDateNow(email))
            {
                throw new HttpRequestException("Day is exist in Data base", null, System.Net.HttpStatusCode.Conflict);
            }
            var response = await _days.SetObject(day);
            
            return response;
        }

        public async Task<Day> UploadDayAsync(Day uploadday)
        {
            var oldDay = await _days.GetObjectBy(uploadday._id);
            var response = await _days.UpdateObject(oldDay.UpdateDay(uploadday));
            return response;

        }
    }
}
