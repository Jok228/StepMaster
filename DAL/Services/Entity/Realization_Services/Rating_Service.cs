using Amazon.S3;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using DnsClient;
using Domain.Entity.API;
using Domain.Entity.Main;
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
                listRatRegion =  await CreateNewRating(regionId);
            }
            return listRatRegion;
 
        }
        public async Task<string> GetPlace(string regionId, string email)
        {
            var rating = _rating.GetRating(regionId).Result.Data;
            var filterPlaceInRegion = rating.ratingUsers.FirstOrDefault(rating => rating.email == email);  //filter for find index
            int placeInRegion = rating.ratingUsers.IndexOf(filterPlaceInRegion) + 1; //find index
            int userRegion = rating.ratingUsers.Count(); //all user in list
            await _rating.UpdateRating(rating);//replace object in DB
            return $"{placeInRegion}/{userRegion}";
        }
        public async Task UpdateRating(string region)
        {

            var ratingRegion = await _rating.GetRating(region);
            if (ratingRegion.Status == MyStatus.NotFound)
            {
                ratingRegion.Data = await CreateNewRating(region);
            }
            var users = await _users.GetObjectsByRegion(region);
            ratingRegion.Data = await SortRating(users.Data, ratingRegion.Data);
            await _rating.UpdateRating(ratingRegion.Data);

        }
        private async Task<Rating> SortRating(List<User> users, Rating ratingRegion)
        {
            TimeSpan diff = DateTime.UtcNow - ratingRegion.lastUpdate;
            if (diff.TotalMinutes > 20)
            {
                ratingRegion.ratingUsers = new List<UserRating>();
                foreach (var user in users)
                {

                    var dayUser = await _days.GetActualDay(user.email);
                    int steps = 0;
                    foreach (var day in dayUser.Data)
                    {
                        steps += (int)day.steps;
                    }
                    ratingRegion.ratingUsers.Add(new UserRating
                    {
                        email = user.email,
                        name = user.nickname,
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
        public async Task UpdateRatingsUser(User oldUser, User newUser)
        {
            var oldRating = _rating.GetRating(oldUser.region_id)
                .Result
                .Data;
            var rating = oldRating.ratingUsers.Where(place => place.email == oldUser.email).FirstOrDefault(); //get old rating user in region 


            if (oldUser.region_id != newUser.region_id && newUser.region_id !=null) //region was changed
            {
                await EditOldRating(oldRating, rating, oldUser, newUser);
                return;
            }
            if (oldUser.nickname != newUser.nickname)
            {
                await EditOldNickName(oldUser.email, oldUser.region_id, newUser.nickname);
                return;
            }


        }
        private async Task EditOldRating(Rating oldRating, UserRating rating, User oldUser, User newUser)
        {
            var oldNickName = oldUser.nickname;

            oldRating.ratingUsers.Remove(rating);
            await _rating.UpdateRating(oldRating);

            var newRating = await _rating.GetRating(newUser.region_id);
            if (newRating.Status == MyStatus.Success)
            {
                await _users.UpdateObject(oldUser.UpdateUser(newUser));
                if (oldNickName != newUser.nickname)
                {
                    rating.name = newUser.nickname;
                    var country = _rating.GetRating(null).Result.Data;
                    var placeCountry = country.ratingUsers.Where(place => place.email == oldUser.email).First();
                    country.ratingUsers.Remove(placeCountry);
                    placeCountry.name = newUser.nickname;

                    country.ratingUsers.Add(placeCountry);
                    country.Sort();
                    await _rating.UpdateRating(country);
                }
                newRating.Data.ratingUsers.Add(rating);
                newRating.Data.Sort();
                await _rating.UpdateRating(newRating.Data);

            }
            else
            {
                if (oldNickName != newUser.nickname)
                {
                    rating.name = newUser.nickname;
                    var country = _rating.GetRating(null).Result.Data;
                    var placeCountry = country.ratingUsers.Where(place => place.email == oldUser.email).First();
                    country.ratingUsers.Remove(placeCountry);
                    placeCountry.name = newUser.nickname;
                    country.ratingUsers.Add(placeCountry);
                    await _rating.UpdateRating(country);
                }
                await _users.UpdateObject(oldUser.UpdateUser(newUser));
                await _rating.UpdateRating(await CreateNewRating(newUser.region_id));
            }
        }
        private async Task EditOldNickName(string email, string regionid, string newNickName)
        {
            var country = _rating.GetRating(null).Result.Data;
            var region = _rating.GetRating(regionid).Result.Data;

            var placeRegion = region.ratingUsers.Where(rating => rating.email == email).First();
            region.ratingUsers.Remove(placeRegion);
            placeRegion.name = newNickName;
            region.ratingUsers.Add(placeRegion);

            var placeCountry = country.ratingUsers.Where(place => place.email == email).First();
            country.ratingUsers.Remove(placeCountry);
            placeCountry.name = newNickName;
            country.ratingUsers.Add(placeCountry);

            await _rating.UpdateRating(region);
            await _rating.UpdateRating(country);
        }
    }
}
