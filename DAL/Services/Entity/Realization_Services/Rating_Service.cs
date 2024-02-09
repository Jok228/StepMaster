using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using StepMaster.Models.Entity;
using static Domain.Entity.Main.ClassWithPosition;

namespace Application.Services.Entity.Realization_Services
{
    public class Rating_Service : IRating_Service
    {
        private readonly IRating_Repository _rating;
        private readonly IUser_Repository _users;
        private readonly IDay_Repository _days;
        private readonly IClan_Repository _clanRepository;

        public Rating_Service(IRating_Repository rating, IUser_Repository user, IDay_Repository day, IClan_Repository clanRepository = null)
        {
            _rating = rating;
            _users = user;
            _days = day;
            _clanRepository = clanRepository;
        }

        public async Task<Rating> GetRating(string regionId)
        {
            var listRatRegion = await _rating.GetRating(regionId);
            if (listRatRegion == null)
            {
                listRatRegion = await CreateNewRating(regionId);
            }
            return listRatRegion;

        }
        public async Task UpdateRating(string region)
        {
            var ratingRegion = await GetRating(region); 
            var users = new List<User>();
            if(region == null)
            {
                users = await _users.GetUserByCountry();
            }
            else
            {
                users = await _users.GetObjectsByRegion(region);
            }            
            ratingRegion = await SortRating(users, ratingRegion);
            await _rating.UpdateRating(ratingRegion);

        }
        private async Task<Rating> SortRating(List<User> users, Rating ratingRegion)
        {
            TimeSpan diff = DateTime.UtcNow - ratingRegion.lastUpdate;
            if (diff.TotalMinutes > 20)
            {
                ratingRegion.RatingUsers = new List<Position>();
                foreach (var user in users)
                {

                    var dayUser = await _days.GetActualDay(user.Email);
                    int steps = 0;
                    foreach (var day in dayUser)
                    {
                        steps += (int)day.steps;
                    }
                    ratingRegion.RatingUsers.Add(new Position
                    {
                        NickName = user.NickName,
                        Email = user.Email,                        
                        Step = steps,
                    });

                }
                ratingRegion.Sort();
            }
            return ratingRegion;
        }
        private async Task<Rating> CreateNewRating(string region)
        {
            var ratingRegion = await _rating.CreateRating(region);
            var users = await _users.GetObjectsByRegion(region);
            ratingRegion = await SortRating(users, ratingRegion);
            return ratingRegion;
        }
        public async  Task <UserRanking> AddNewPosition(string email,string regionId)
        {
            var listRatRegion = await  GetRating(regionId);            
            listRatRegion.Sort();

            var listRatCountry = await GetRating(null);            
            listRatCountry.Sort();


            await _rating.UpdateRating(listRatCountry);
            await _rating.UpdateRating(listRatRegion);
            return new UserRanking()
            {
                placeInRegion = listRatRegion.GetUserRanking(email),
                placeInCountry = listRatCountry.GetUserRanking(email),
            };           
        }
        public async Task<UserRanking> GetUserRanking(string email, string regionId, string? clanId)
        {
            string placeRating = null;
            if(clanId != null)
            {
                var clan = await _clanRepository.GetClansById(clanId);
                var placeUser = clan.RatingUsers.IndexOf(clan.RatingUsers.Find(user => user.Email == email))+1;
                placeRating = $"{placeUser}/{clan.RatingUsers.Count}";
            }
            await UpdateRating(regionId);
            await UpdateRating(null);
            var listRatRegion = await GetRating(regionId);
            var listRatCountry = await GetRating(null);


            return new UserRanking()
            {
                placeInRegion = listRatRegion.GetUserRanking(email),
                placeInCountry = listRatCountry.GetUserRanking(email),
                placeInClan = placeRating
            };
        }

        


        public async Task SwapPosition(string oldRegionId, string newRegionId,string email)
        {
            var oldRating = await GetRating(oldRegionId);
            var position = oldRating.GetUserPosition(email);
            oldRating.DeleteUserPosition(position);
            var newReting = await GetRating(newRegionId);
            newReting.AddUserPosittion(position);
            await _rating.UpdateRating(newReting);
            await _rating.UpdateRating(oldRating);
            
        }
    }
}
