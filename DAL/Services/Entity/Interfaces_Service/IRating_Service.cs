using StepMaster.Models.Entity;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface IRating_Service
    {

        Task<Rating> GetRating(string regionId);

        Task<string> GetPlace(string regionId, string email);

        Task UpdateRating(string region);

        Task UpdateRatingsUser(User oldUser, User newUser);
    }
}
