using StepMaster.Models.Entity;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IRating_Repository
    {
        Task<Rating> GetRating(string regionId);

        Task<bool> CheckRating(string regionId);

        Task<Rating> CreateRating(string regionId);

        Task<Rating> UpdateRating(Rating newRating);

        Task<Rating> SetRating(Rating newRating);

        Task<List<Rating>> GetRatingsByUserEmail(string email);


    }
}
