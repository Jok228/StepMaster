using Amazon.S3;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using DnsClient;
using Domain.Entity.API;
using Domain.Entity.Main;
using MongoDB.Driver.Linq;
using StepMaster.Models.Entity;
using StepMaster.Services.ForDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Entity.Realization_Services
{
    public class Rating_Service : IRating_Service
    {
        private readonly IRating_Repository _rating;
        private readonly IUser_Repository _users;
        private readonly IDay_Repository _days;

        public Rating_Service(IRating_Repository rating, IUser_Repository user, IDay_Repository day)
        {
            _rating = rating;
            _users = user;
            _days = day;
        }

        public async Task<Rating> GetRating(string regionId)
        {
            var listRatRegion = _rating.GetRating(regionId).Result.Data;
            if (listRatRegion == null)
            {
                listRatRegion = await CreateNewRating(regionId);
            }
            return listRatRegion;

        }
        public async Task UpdateRating(string region)
        {
            var ratingRegion = await GetRating(region);            
            var users = await _users.GetObjectsByRegion(region);
            ratingRegion = await SortRating(users.Data, ratingRegion);
            await _rating.UpdateRating(ratingRegion);

        }
        private async Task<Rating> SortRating(List<User> users, Rating ratingRegion)
        {
            TimeSpan diff = DateTime.UtcNow - ratingRegion.lastUpdate;
            if (diff.TotalMinutes > 20)
            {
                ratingRegion.ratingUsers = new List<Position>();
                foreach (var user in users)
                {

                    var dayUser = await _days.GetActualDay(user.email);
                    int steps = 0;
                    foreach (var day in dayUser.Data)
                    {
                        steps += (int)day.steps;
                    }
                    ratingRegion.ratingUsers.Add(new Position
                    {
                        email = user.email,                        
                        step = steps,
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
            ratingRegion.Data = await SortRating(users.Data, ratingRegion.Data);
            return ratingRegion.Data;
        }
        public async  Task <UserRanking> AddNewPosition(string email,string regionId)
        {
            var newRating = new Position(email);//Create new user rating for top list in DB

            var listRatRegion = await  GetRating(regionId);
            listRatRegion.ratingUsers.Add(newRating);
            listRatRegion.Sort();

            var listRatCountry = await GetRating(null);
            listRatCountry.ratingUsers.Add(newRating);
            listRatCountry.Sort();


            await _rating.UpdateRating(listRatCountry);
            await _rating.UpdateRating(listRatRegion);
            return new UserRanking()
            {
                placeInRegion = listRatRegion.GetUserRanking(email),
                placeInCountry = listRatCountry.GetUserRanking(email),
            };           
        }
        public async Task<UserRanking> GetUserRanking(string email, string regionId)
        {
            await UpdateRating(regionId);
            await UpdateRating(null);
            var listRatRegion = await GetRating(regionId);
            var listRatCountry = await GetRating(null);
            return new UserRanking()
            {
                placeInRegion = listRatRegion.GetUserRanking(email),
                placeInCountry = listRatCountry.GetUserRanking(email),
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
