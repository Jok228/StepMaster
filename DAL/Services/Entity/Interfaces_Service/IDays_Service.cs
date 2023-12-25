using Domain.Entity.Main;
using Domain.Entity.API;

namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IDays_Service
    {
        Task<BaseResponse<List<Day>>> GetDaysUserByEmail(string email);

        Task<BaseResponse<Day>> SetDayAsync(Day day, string email);

        Task<BaseResponse<Day>> UploadDayAsync(Day uploadday);

       
    }
}
