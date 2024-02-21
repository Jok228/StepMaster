using StepMaster.Models.Entity;
using System.Globalization;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface IRating_Service
    {
        Task SwapPosition(string oldRegionId, string newRegionId, string email);

        Task<UserRanking> GetUserRanking( string email, string regionId,string? clanId);

        Task  <UserRanking>AddNewPosition(string email, string regionId);
    }
}
