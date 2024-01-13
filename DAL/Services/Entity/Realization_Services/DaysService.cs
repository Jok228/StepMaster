using Domain.Entity.API;
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.Main;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;

namespace StepMaster.Services.ForDb.Repositories
{
    public class DaysService : IDays_Service
    {
        private readonly IDay_Repository _days;
        

        public DaysService(IDay_Repository days)
        {

            _days = days;         
          
        }
        public async Task<BaseResponse<List<Day>>> GetDaysUserByEmail(string email)
        {
            return await _days.GetDaysByEmail(email);
        }

        public async Task<BaseResponse<Day>> SetDayAsync(Day day, string email)
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

        public async Task<BaseResponse<Day>> UploadDayAsync(Day uploadday)
        {
            var oldDay = await _days.GetObjectBy(uploadday._id);
            if (oldDay.Status == MyStatus.Success)
            {
                var response = await _days.UpdateObject(oldDay.Data.UpdateDay(uploadday));
                return response;
            }
            return BaseResponse<Day>.Create(null, MyStatus.NotFound);

        }
    }
}
