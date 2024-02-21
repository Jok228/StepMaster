using Domain.Entity.Main;


namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IDays_Service
    {
        Task<List<Day>> GetDaysUserByEmail(string email);

        Task<Day> SetDayAsync(Day day, string email);

        Task<Day> UploadDayAsync(Day uploadday);

       
    }
}
