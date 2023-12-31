using Domain.Entity.API;
using StepMaster.Models.Entity;
using System.Globalization;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface IRating_Service
    {
        Task SwapPosition(string oldRegionId, string newRegionId, string email);

        Task<UserRanking> GetUserRanking( string email, string regionId);

        Task  <UserRanking>AddNewPosition(string email, string regionId);
    }
}
