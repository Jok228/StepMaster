using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;

namespace StepMaster.Services.Interfaces
{
    public interface IDays_Service
    {
        Task<BaseResponse<List<Day>>> GetDaysUserByEmail(string email);

        Task<BaseResponse<Day>> SetDayAsync(Day day, string email);

        Task<BaseResponse<Day>> UploadDayAsync (Day uploadday);
    }
}
