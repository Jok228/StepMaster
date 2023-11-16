using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces
{
    public interface IDays_Service
    {
        Task<List<Day>> GetDaysUserByEmail(string email);

        Task<Day> SetDayAsync(Day day);
    }
}
