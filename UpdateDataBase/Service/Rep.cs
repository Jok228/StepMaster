using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateDateBase.Service
{
    internal class Rep
    {
        public class AllOfTime_Rep
        {
            private string RegionsCache = "Regions";

            private readonly IMongoCollection<Day> _days;
            private readonly IMongoCollection<User> _users;
            private readonly IMongoCollection<Region> _regions;
            private readonly IMongoCollection<Rating> _rating;
            private DateTime _date;
            public AllOfTime_Rep( IMongoClient mongoClient)
            {
                var database = mongoClient.GetDatabase("StepMaster");
                _days = database.GetCollection<Day>("Days");
                _users = database.GetCollection<User>("User");
                _regions = database.GetCollection<Region>("Regions");
                _rating = database.GetCollection<Rating>("Rating");


            }
            public async Task UpdateRating()
            {
                _date = DateTime.Now;
                await ForRegion();
                await ForAll();
            }
            private async Task ForAll()
            {
                try
                {

                    var ratingForAll = new Rating();
                    try
                    {
                        ratingForAll = await _rating.FindAsync(rat => rat.regionId == null && rat.date.Month == _date.Month && rat.date.Year == _date.Year)
                        .Result
                        .FirstAsync();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Sequence contains no elements")
                        { //Add regions which not exist in DB
                            ratingForAll = new Rating()
                            {
                                date = _date,
                                regionId = null,
                                ratingUsers = new List<UserRating>()
                            };
                            _rating.InsertOne(ratingForAll);
                        }
                        else
                        {
                            Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                        }
                    }
                    var listRatAllReg = await _rating.FindAsync(rating => rating.date.Month == _date.Month && rating.date.Year == _date.Year)
                        .Result
                        .ToListAsync();
                    var actualListUserReit = new List<UserRating>();
                    foreach (var rating in listRatAllReg)
                    {
                        if (rating.regionId != null)
                        {
                            actualListUserReit.AddRange(rating.ratingUsers);
                        }
                    }
                    ratingForAll.ratingUsers = actualListUserReit;
                    ratingForAll.ratingUsers = ratingForAll.ratingUsers
                        .OrderBy(rating => rating.step)
                        .Reverse()
                        .ToList(); //Sort    
                    await _rating.ReplaceOneAsync(rat => rat.regionId == null, ratingForAll);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                }
            }

            private async Task ForRegion()
            {
                try
                {
                    var regions = await _regions.FindAsync(r => true)
                        .Result
                        .ToListAsync();

                    var ratingFromReg = new Rating();
                    foreach (var region in regions)
                    {

                        try
                        {
                            ratingFromReg = await _rating.FindAsync(rating => rating.date.Month == _date.Month && rating.date.Year == _date.Year && rating.regionId == region._id.ToString())
                           .Result
                           .FirstAsync(); //Get all region
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Sequence contains no elements")
                            { //Add regions which not exist in DB
                                ratingFromReg = new Rating()
                                {
                                    date = _date,
                                    regionId = region._id.ToString(),
                                    ratingUsers = new List<UserRating>()
                                };
                                _rating.InsertOne(ratingFromReg);
                            }
                            else
                            {
                                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                            }

                        }
                        var usersFromReg = await _users.FindAsync(u => u.region_id == region._id.ToString())
                            .Result //All user which is located in region
                            .ToListAsync();
                        var filterForList = new UserRating();
                        foreach (var user in usersFromReg) // forreach each user  
                        {
                            var daysFromUser = await _days.FindAsync(day => day.date.Month == _date.Month && day.date.Year == _date.Year && day.email == user.email)
                                .Result
                                .ToListAsync();//Get days by user for this month                      
                            int allSteps = 0;
                            foreach (var day in daysFromUser)
                            {
                                var step = day.steps;
                                allSteps += step; //Sum steps user for day
                            }
                            var ratingUser = new UserRating { step = allSteps, email = user.email, name = user.nickname };
                            filterForList = ratingFromReg.ratingUsers.FirstOrDefault(r => r.email == user.email);
                            ratingFromReg.ratingUsers.Remove(filterForList);
                            ratingFromReg.ratingUsers.Add(ratingUser);

                        }
                        ratingFromReg.ratingUsers = ratingFromReg.ratingUsers
                               .OrderBy(rating => rating.step)
                               .Reverse()
                               .ToList(); //Sort     
                        _rating.ReplaceOne(rating => rating.regionId == region._id.ToString() && rating.date.Month == _date.Month && rating.date.Year == _date.Year, ratingFromReg);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                }
            }
        }
    }
}
