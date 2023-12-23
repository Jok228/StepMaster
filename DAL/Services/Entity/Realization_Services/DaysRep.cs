using Domain.Entity.API;
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.Main;
using Application.Repositories.Db.Interfaces_Repository;

namespace StepMaster.Services.ForDb.Repositories
{
    public class DaysRep : IDays_Service
    {
        private readonly IDay_Repository _days;

        public DaysRep(IDay_Repository days)
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
            if (_days.ChechDayDateNow(email).Result.Data)
            {
                return BaseResponse<Day>.Create(null, MyStatus.Exists);
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
