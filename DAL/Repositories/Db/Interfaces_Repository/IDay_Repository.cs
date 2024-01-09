using Domain.Entity.API;
using Domain.Entity.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IDay_Repository : IBase_Repository<Day>
    {
        Task<BaseResponse<List<Day>>> GetDaysByEmail(string email);

        Task<BaseResponse<bool>> ChechDayDateNow(string email);

        Task<BaseResponse<List<Day>>> GetActualDay(string email);

        Task<int> GetStepRangeForAchievements(string email, int? dayCount);

        Task<int> GetStepRangeForGrades(string email, int? dayCount);

        Task<int> GetCountDayMoreMax(string email, int? dayCount);

        Task<List<Day>>GetRangDay(string email, int dayCount);
    }
}
