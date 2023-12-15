using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System.Security.Cryptography;
using StepMaster.Models.HashSup;
using MongoDB.Bson;
using StepMaster.Services.ForDb.Interfaces;

using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.API;

namespace StepMaster.Services.ForDb.Repositories
{
    public class UserRep : IUser_Service
    {
        IMemoryCache _cache;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Rating> _rating;
        public UserRep(IAPIDatabaseSettings settings, IMongoClient mongoClient, IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("User");
            _rating = database.GetCollection<Rating>("Rating");
            _cache = cache;

        }
        public async Task<List<User>> GetAllUser()
        {
            var list = new List<User>();
            list = await _users.FindAsync(_ => true).Result.ToListAsync();
            return list;
        }


        public async Task<BaseResponse<User>> GetByLoginAsync(string email)
        {
            var response = new BaseResponse<User>();
            try
            {
                _cache.TryGetValue(email, out User? user);
                if (user == null)
                {
                    user = await _users.FindAsync(user => user.email == email).Result.FirstAsync();
                    _cache.Set(email, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.Data = user;
                    return response;
                }

            }
            catch (Exception ex)
            {

                if (ex.Message == "Sequence contains no elements")
                {
                    response.Data = null;
                    response.Status = MyStatus.NotFound;
                    return response;
                }
                Console.WriteLine(ex.Message + ',' + ex.StackTrace);
                response.Data = null;
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<User> RegUserAsync(User newUser)
        {
            try
            {

                newUser.password = HashCoder.GetHash(newUser.password); // Logic for entity user
                newUser.role = "user";

                var newRating = new UserRating(newUser.email, newUser.nickname); //Create new user rating for top list in DB
               


                var listRatRegion = await _rating.FindAsync(rating => rating.regionId == newUser.region_id) //Logic for region -> find list rating regiona
                    .Result
                    .FirstOrDefaultAsync();
                listRatRegion.ratingUsers.Add(newRating); //add new rating list user in it
                listRatRegion.ratingUsers = listRatRegion.ratingUsers // sort
                    .OrderBy(rating => rating.step)
                    .Reverse()
                    .ToList();//Create rating but for userDB,
                var filterPlaceInRegion = listRatRegion.ratingUsers.FirstOrDefault(rating => rating.email == newUser.email);  //filter for find index
                int placeInRegion = listRatRegion.ratingUsers.IndexOf(filterPlaceInRegion)+1; //find index
                int placesInRegion = listRatRegion.ratingUsers.Count()+1; //all user in list


                await _rating.ReplaceOneAsync(rating => rating.regionId == listRatRegion.regionId, listRatRegion); //replace object in DB

                var listRatCounty = await _rating.FindAsync(rating => rating.regionId == null) // all same thing but for country
                    .Result
                    .FirstOrDefaultAsync();
                listRatCounty.ratingUsers.Add(newRating);
                listRatCounty.ratingUsers = listRatCounty.ratingUsers
                    .OrderBy(rating => rating.step)
                    .Reverse()
                    .ToList();
                var filterPlaceInCountry = listRatCounty.ratingUsers.FirstOrDefault(rating => rating.email == newUser.email);//filter for find index
                int placeInCountry = listRatRegion.ratingUsers.IndexOf(filterPlaceInRegion) + 1; //find index
                int placesInCountry = listRatRegion.ratingUsers.Count() + 1;  //all user in list


                await _rating.ReplaceOneAsync(rating => rating.regionId == listRatCounty.regionId, listRatCounty);

                var userPlaceInRating = new PlaceUserOnRating(placeInRegion: placeInRegion, allRegion: placesInRegion, placeInCountry: placeInCountry, allCountry: placesInCountry);
                newUser.rating = userPlaceInRating;



                await _users.InsertOneAsync(newUser); // inser newUsers
                return newUser;
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> UpdateUser(User userUpdate)
        {
            try
            {

                var filter = Builders<User>.Filter.Eq("email", userUpdate.email);
                await _users.ReplaceOneAsync(filter, userUpdate);
                _cache.Remove(userUpdate.email);

                return userUpdate;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetUserbyCookie(string cookies)
        {

            try
            {                
                var user = await _users.FindAsync(user => user.lastCookie == cookies)
                    .Result
                    .FirstAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BaseResponse<bool>> CheckPassword(string login, string password)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _cache.TryGetValue(login, out User? user);
                if(user == null)
                {
                    user = await _users.FindAsync(u => u.email == login)
                    .Result
                    .FirstAsync();
                    _cache.Set(user, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
                
                
                if (HashCoder.Verify(user.password, password))
                {
                    response.Data = true;
                    response.Status = MyStatus.Success;
                    return response;

                }
                else
                {
                    response.Data = false;
                    response.Status = MyStatus.Unauthorized;
                    return response;
                }
                   
                
                
            }
            catch (Exception ex)
            {
                if(ex.Message == "Sequence contains no elements")
                {
                    response.Data = false;
                    response.Status = MyStatus.NotFound;
                    return response;
                }
                Console.WriteLine(ex.Message + ',' + ex.StackTrace);
                response.Data= false;
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<BaseResponse<List<User>>> FindUserByParams(string region = "none")
        {
            var response = new BaseResponse<List<User>>();
            try
            {
                var listUserByRegion = await _users.FindAsync(u => u.region_id == region)
                    .Result
                    .ToListAsync();
                //listUserByRegion.Sort(u => u.)
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                response.Data = null;
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                return response;
            }
        }

        public async Task<BaseResponse<bool>> DeleteCookie(string userEmail)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var user = await _users.FindAsync(user => user.email == userEmail)
                    .Result
                    .FirstAsync();
                user.lastCookie = null;
                await  _users.ReplaceOneAsync(user => user.email == userEmail, user);
                response.Data = true;
                response.Status = MyStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                response.Data = false;
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                return response;
            }
        }
    }
}
