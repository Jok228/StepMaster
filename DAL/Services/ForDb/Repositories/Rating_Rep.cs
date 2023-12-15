using Application.Services.ForDb.APIDatebaseSet;
using Application.Services.ForDb.Interfaces;
using Domain.Entity.Main;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ForDb.Repositories
{
    public class Rating_Rep : IRating_Service
    {
        private readonly IMongoCollection<Rating> _rating;
        private readonly IMongoCollection<User> _user;

        public Rating_Rep(IAPIDatabaseSettings settings, IMongoClient mongoClient, IMemoryCache cache)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);            
            _rating = database.GetCollection<Rating>("Rating");
            _user = database.GetCollection<User>("User");
        }

        public async Task<PlaceUserOnRating> GetRatingUser(string email, string regionId)
        {
           var response = new PlaceUserOnRating();
            try
            {
                
                var ratingRegion = await _rating.FindAsync(r => r.regionId == regionId) //Logic for rating user in region
                    .Result
                    .FirstAsync();
                var filterForRegion = ratingRegion.ratingUsers.FirstOrDefault(r => r.email == email);   
                var placeInRegion = ratingRegion.ratingUsers.IndexOf(filterForRegion) +1;
                var usersInRegion = ratingRegion.ratingUsers.Count();
              




                var ratingCounty = await _rating.FindAsync(r => r.regionId == null) //Logic for rating user in country
                    .Result
                    .FirstAsync();
                var filterForAll = ratingRegion.ratingUsers.FirstOrDefault (r => r.email == email);
                var placeInCounty = ratingCounty.ratingUsers.IndexOf(filterForAll) +1;
                var usersInCounty = ratingCounty.ratingUsers.Count();
               


                response = new PlaceUserOnRating(allCountry: usersInCounty, allRegion: usersInRegion,placeInCountry:placeInCounty,placeInRegion:placeInRegion);

                var filter = Builders<User>.Filter.Eq(x => x.email, email);
                var update = Builders<User>.Update.Set(x => x.rating, response);
                var result = _user.UpdateOneAsync(filter, update).Result;
                return response;

            }
            catch(Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    var ratingCountry = await _rating.FindAsync(r => r.regionId == null) //Logic if user not found in rat list. -> client most likey a new user.
                    .Result
                    .FirstAsync();
                    var ratingRegion = await _rating.FindAsync(r => r.regionId == regionId)
                    .Result
                    .FirstAsync();
                    var usersInCountry = ratingCountry.ratingUsers.Count() + 1;
                    var usersInRegion = ratingRegion.ratingUsers.Count() + 1;

                    response.placeInCountry = $"{usersInCountry}/{usersInCountry}";
                    response.placeInRegion = $"{usersInRegion}/{usersInRegion}";
                    return response;
                }
                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                return response;
            }
        }
    }
}
