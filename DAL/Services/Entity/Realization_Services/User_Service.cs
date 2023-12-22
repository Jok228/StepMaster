using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System.Security.Cryptography;
using StepMaster.Models.HashSup;
using MongoDB.Bson;
using StepMaster.Services.ForDb.Interfaces;

using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.API;
using Domain.Entity.Main;
using DnsClient;
using Amazon.Runtime;
using Application.Repositories.S3.Interfaces;
using Application.Repositories.Db.Interfaces_Repository;
using MongoDB.Bson.Serialization.Serializers;

namespace StepMaster.Services.ForDb.Repositories
{
    public class User_Service : IUser_Service
    {
        private readonly IUser_Repository _users;
        private readonly IDay_Repository _days;
        private readonly IRating_Repository _rating;
        private readonly IAws_Repository _aws;
        public User_Service(IUser_Repository users, IRating_Repository rating,IAws_Repository aws,IDay_Repository day )
        {            
            _users = users;
            _rating = rating;   
            _aws = aws;
            _days = day;

        }
       

        public async Task<BaseResponse<User>> EditUser(string email, User userEdit)
        { 
            var oldUser = await _users.GetObjectBy(email);            
            await UpdOther(oldUser.Data, userEdit);
            if (oldUser.Status == MyStatus.Success)
            {
                return oldUser;               
            }
            return oldUser;
        }

        private async Task UpdOther(User oldUser, User newUser)
        {           
            var oldRating =  _rating.GetRating(oldUser.region_id)
                .Result
                .Data;
            var rating = oldRating.ratingUsers.Where(place => place.email == oldUser.email).FirstOrDefault();
            if(oldUser.region_id != newUser.region_id)
            {
                await EditOldRating(oldRating, rating, oldUser,newUser);
                return;
            }
            if(oldUser.nickname != newUser.nickname)
            {
                await EditOldNickName(oldUser.email, oldUser.region_id, newUser.nickname);
                return;
            }


        }
        private async Task EditOldRating(Rating oldRating,UserRating rating,User oldUser,User newUser)
        {
            string oldNickName = oldUser.nickname;
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


        public async Task<BaseResponse<User>> GetByLoginAsync(string email)
        {
            return await _users.GetObjectBy(email);            
        }
        
        public async Task<string> GetPlace(Rating rating, UserRating newRating, string email)
        {
            rating.ratingUsers.Add(newRating);
            rating.Sort();
            var filterPlaceInRegion = rating.ratingUsers.FirstOrDefault(rating => rating.email == email);  //filter for find index
            int placeInRegion = rating.ratingUsers.IndexOf(filterPlaceInRegion) + 1; //find index
            int userRegion = rating.ratingUsers.Count(); //all user in list
            await _rating.UpdateRating(rating);//replace object in DB
            return $"{placeInRegion}/{userRegion}";
        }
        public async Task<BaseResponse<User>> RegUserAsync(User newUser)
        {
            try
            {  
                var newRating = new UserRating(newUser.email, newUser.nickname);//Create new user rating for top list in DB
                var listRatRegion = await _rating.GetRating(newUser.region_id);
                if(listRatRegion.Data == null)
                {
                    listRatRegion = await _rating.CreateRating(newUser.region_id);
                }

                var listRatCountry = await _rating.GetRating(null);
                if(listRatCountry.Data == null)
                {
                    listRatCountry = await _rating.CreateRating(null);
                }

                if (listRatRegion.Data != null && listRatCountry.Data != null)
                {
                    var placeInRegion = await GetPlace(listRatRegion.Data, newRating, newUser.email);
                    var placeInCountry = await GetPlace(listRatCountry.Data, newRating, newUser.email);
                    newUser.rating = new PlaceUserOnRating
                    {
                        placeInCountry = placeInCountry,
                        placeInRegion = placeInRegion,
                    };
                    await _users.SetObject(newUser); // inser newUsers
                    return BaseResponse<User>.Create(newUser,MyStatus.Success);
                }
                else
                {
                    return BaseResponse<User>.Create(null, MyStatus.NotFound);
                }

                
            }
            catch
            {
                return BaseResponse<User>.Create(null, MyStatus.Except); ;
            }
        }
        public async Task<BaseResponse<User>> UpdateUser(User userUpdate)
        {
            return await _users.UpdateObject(userUpdate);
        }
        public async Task<BaseResponse<User>> GetUserbyCookie(string cookies)
        {
            return await _users.GetByCookie(cookies);
        }
        
        public async Task<BaseResponse<bool>> DeleteCookie(string userEmail)
        {
            var user = await _users.GetObjectBy(userEmail);
            if (user.Data != null)
            {
                user.Data.lastCookie = null;
                return BaseResponse<bool>.Create(true, MyStatus.Success);
            }
            else
            {
                return BaseResponse<bool>.Create(false, user.Status);
            }
            
            
        }
        public async Task<BaseResponse<User>> EditPassword(string email, string newPassword, string oldPassword)
        {
            var user = await _users.GetObjectBy(email);
            if (user.Data != null)
            {
                if (HashCoder.Verify(user.Data.password, oldPassword))
                {
                    user.Data.password = HashCoder.GetHash(newPassword);
                    return await _users.UpdateObject(user.Data);
                }
                else
                {
                    return BaseResponse<User>.Create(null, MyStatus.Unauthorized);
                }
            }
            else
            {
                return BaseResponse<User>.Create(null, MyStatus.NotFound);
            }

        }

        public async Task<BaseResponse<User>> GetFullUser(string email)
        {
            var user = await _users.GetObjectBy(email);
            if(user.Status == MyStatus.Success)
            {
                var link = await _aws.GetLink(email);
                if (link != null)
                {
                    var rating = await GetPlace(email, user.Data.region_id);
                    if (rating.Status == MyStatus.Success)
                    {
                        user.Data.AvatarLink = link;
                        user.Data.rating = rating.Data;
                        return user;
                    }
                }

            }
            return BaseResponse<User>.Create(null,MyStatus.NotFound);
        }
        public async Task<BaseResponse<PlaceUserOnRating>>GetPlace(string email, string region)
        {
            await UpdateRating(region);
            await UpdateRating(null);
            var placeInCountry = await GetRatingRegion(email, null);
            var placeInRegion = await GetRatingRegion(email, region);
            if (placeInCountry != null && placeInRegion!=null)
            {
                return BaseResponse<PlaceUserOnRating>.Create(new PlaceUserOnRating
                {
                    placeInCountry = placeInCountry,
                    placeInRegion = placeInRegion,
                }, MyStatus.Success
                );
            }
            else
            {
                return BaseResponse<PlaceUserOnRating>.Create(new PlaceUserOnRating(),MyStatus.Except);
            }
            
        }
        public async Task<string> GetRatingRegion(string email, string region)
        {
            var ratingRegion = await _rating.GetRating(region);

            if (ratingRegion.Status == MyStatus.NotFound)
            {
                ratingRegion.Data = await CreateNewRating(region);
            }
            
            ratingRegion.Data.Sort();
            
            await _rating.UpdateRating(ratingRegion.Data);
            return GetRatingUser(ratingRegion.Data, email);



        }
        private async Task<Rating> SortRating(List<User> users,Rating ratingRegion)
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
        private async Task UpdateRating (string region)
        {
            
            var ratingRegion = await _rating.GetRating(region);
            if(ratingRegion.Status == MyStatus.NotFound)
            {
               ratingRegion.Data =  await CreateNewRating(region);
            }
            var users = await _users.GetObjectsByRegion(region);
            ratingRegion.Data = await SortRating(users.Data, ratingRegion.Data);
            await _rating.UpdateRating(ratingRegion.Data);
            
        }
        private async Task<Rating> CreateNewRating(string region)
        {
            var ratingRegion = await _rating.CreateRating(region);
            var users = await _users.GetObjectsByRegion(region);
            ratingRegion.Data = await SortRating(users.Data, ratingRegion.Data);
            return ratingRegion.Data;
        }

        public string GetRatingUser(Rating rating, string email)
        {

            var filterForRegion = rating.ratingUsers.FirstOrDefault(r => r.email == email);
            var placeInRegion = rating.ratingUsers.IndexOf(filterForRegion) + 1;
            var usersInRegion = rating.ratingUsers.Count();
            return $"{placeInRegion}/{usersInRegion}";
        }
    }
}
