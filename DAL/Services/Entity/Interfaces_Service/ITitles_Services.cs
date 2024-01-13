using Domain.Entity.API;
using Domain.Entity.Main.Titles;
using static Domain.Entity.Main.Titles.Condition;
using static StepMaster.Models.Entity.User;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface ITitles_Services
    {
        Task<List<Condition>> GetLinksTitles(List<Condition> listPaths);

        Task UpdateTitlesList(string email);

        Task<List<string>> UpdateSelectUserTitles(string email, string conditionMongoId);
        Task<List<Condition>> GetActualAllProgress(string email,List<Condition> list);

    }
}
