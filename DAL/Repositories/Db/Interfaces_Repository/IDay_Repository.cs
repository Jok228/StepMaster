using Domain.Entity.Main;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IDay_Repository : IBase_Repository<Day>
    {
        Task<List<Day>> GetDaysByEmail(string email);

        Task<bool> ChechDayDateNow(string email);

        Task<List<Day>> GetActualDay(string email);

        Task<int> GetAllStepsUser(string email, int? dayCount);

        Task<int> GetStepRangeForGrades(string email);

        Task<int> GetCountDayMoreMax(string email);

        Task<List<Day>>GetRangDay(string email, int dayCount);

        Task DeleteObjectsByEmail(string email);
    }
}
