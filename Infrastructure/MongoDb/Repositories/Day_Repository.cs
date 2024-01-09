using Amazon.S3.Model;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.API;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Linq;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.MongoDb.Repositories
{
    public class Day_Repository : IDay_Repository

    {
        private readonly IMongoCollection<Day> _days;
        private IMy_Cache _cache;
        public Day_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _days = database.GetCollection<Day>(TableName.Day);
            _cache = cache;

        }

        public async Task<BaseResponse<bool>> ChechDayDateNow(string email)
        {
            try
            {
                var day = await _days.FindAsync(day => day.email == email
                && day.date.Date == DateTime.UtcNow.Date)
                   .Result
                   .FirstAsync();
                return BaseResponse<bool>.Create(true, MyStatus.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return BaseResponse<bool>.Create(false, MyStatus.Success);
                }
                return BaseResponse<bool>.Create(true, MyStatus.Except);
            }
        }

        public async Task<BaseResponse<List<Day>>> GetActualDay(string email)
        {
            try
            {
                var days = await _days.FindAsync(day => day.email == email
                && day.date.Month == DateTime.UtcNow.Month
                && day.date.Year == DateTime.UtcNow.Year)
                    .Result
                    .ToListAsync();
                return BaseResponse<List<Day>>.Create(days, MyStatus.Success);

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NotFound)
                {
                    return BaseResponse<List<Day>>.Create(new List<Day>(), MyStatus.Success);
                }
                return BaseResponse<List<Day>>.Create(null, MyStatus.Except);
            }
        }

        public async Task<BaseResponse<List<Day>>> GetDaysByEmail(string email)
        {
            try
            {
                var days = await _days.FindAsync(day => day.email == email)
                    .Result
                    .ToListAsync();
                return BaseResponse<List<Day>>.Create(days, MyStatus.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NotFound)
                {
                    return BaseResponse<List<Day>>.Create(new List<Day>(), MyStatus.Success);
                }
                return BaseResponse<List<Day>>.Create(null, MyStatus.Except);
            }
        }

        public async Task<BaseResponse<Day>> GetObjectBy(string idOrEmail)
        {
            try
            {
                var day = _cache.GetObject(idOrEmail) as Day;
                if (day == null)
                {
                    day = await _days.FindAsync(day => day._id == idOrEmail)
                    .Result
                    .FirstAsync();
                    _cache.SetObject(idOrEmail, day, 10);
                    return BaseResponse<Day>.Create(day, MyStatus.Success);
                }
                return BaseResponse<Day>.Create(day, MyStatus.Success);


            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return BaseResponse<Day>.Create(null, MyStatus.NotFound);
                }
                return BaseResponse<Day>.Create(null, MyStatus.Except);
            }
        }

        public async Task<int> GetStepRangeForAchievements(string email, int? dayCount)
        {
            var dateNow = new BsonDateTime(DateTime.UtcNow.Date);
            var aggregateStage1 = new BsonDocument()
           {
               {
                   "$match",new BsonDocument() {
                       {"email",email}
                   }
               }
           };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$group",new BsonDocument()
                    {
                        {
                            "_id","null"
                        },
                        {
                            "totalSteps", new BsonDocument()
                            {
                                {
                                    "$sum","$steps"
                                }
                            }
                        }
                    }
                }
            };
            try
            {
                if (dayCount != null)
                {
                    var dateRange = new BsonDateTime(DateTime.UtcNow.AddDays(-(int)dayCount).Date);
                    var aggregateStage2 = new BsonDocument() { { "$match", new BsonDocument() { { "date", new BsonDocument() { { "$gte", dateRange }, { "$lte", dateNow } } } } } };
                    var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3 };
                    var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                    return (int)result["totalSteps"];
                }
                else
                {
                    var dateRange = new BsonDateTime(DateTime.MinValue.Date);
                    var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage3 };
                    var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                    return (int)result["totalSteps"];
                }
            }
            catch (Exception ex)
            {
                if(ex.Message == DbExMessage.NoElements)
                {
                    return 0;
                }
                throw ex;
            }
            
        }

        public async Task<int> GetStepRangeForGrades(string email, int? dayCount)
        {
            var dateRange = new BsonDateTime(DateTime.UtcNow.AddDays(-(int)dayCount).Date);
            var dateNow = new BsonDateTime(DateTime.UtcNow.Date);
            var aggregateStage1 = new BsonDocument()
           {
               {
                   "$match",new BsonDocument() {
                       {"email",email}
                   }
               }
           };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                              "date",new BsonDocument()
                              {
                                  {
                                      "$gte", dateRange
                                  },
                                  {
                                       "$lte",dateNow
                                  }

                              }
                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "steps",new BsonDocument()
                            {
                                {
                                    "$lte",30000
                                }
                            }
                        },

                    }
                }
            };
            var aggregateStage4 = new BsonDocument()
            {
                {
                    "$group",new BsonDocument()
                    {
                        {
                            "_id","null"
                        },
                        {
                            "totalSteps", new BsonDocument()
                            {
                                {
                                    "$sum","$steps"
                                }
                            }
                        }
                    }
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4 };
            try
            {
                var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                return (int)result["totalSteps"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<BaseResponse<Day>> SetObject(Day value)
        {
            try
            {
                await _days.InsertOneAsync(value);
                _cache.SetObject(value._id, value, 10);
                return BaseResponse<Day>.Create(value, MyStatus.SuccessCreate);
            }
            catch
            {
                return BaseResponse<Day>.Create(null, MyStatus.Except);
            }

        }

        public async Task<BaseResponse<Day>> UpdateObject(Day newValue)
        {
            try
            {
                var filter = Builders<Day>.Filter.Eq("_id", newValue._id);
                await _days.ReplaceOneAsync(filter, newValue);
                _cache.DeleteObject(newValue._id);
                _cache.SetObject(newValue._id, newValue, 10);
                return BaseResponse<Day>.Create(newValue, MyStatus.Success);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<Day>.Create(null, MyStatus.Except);
            }

        }
      

        public async Task<int> GetCountDayMoreMax(string email, int? dayCount)
        {
            var dateRange = new BsonDateTime(DateTime.UtcNow.AddDays(-(int)dayCount).Date);
            var dateNow = new BsonDateTime(DateTime.UtcNow.Date);
            var aggregateStage1 = new BsonDocument()
           {
               {
                   "$match",new BsonDocument() {
                       {"email",email}
                   }
               }
           };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                              "date",new BsonDocument()
                              {
                                  {
                                      "$gte", dateRange
                                  },
                                  {
                                       "$lte",dateNow
                                  }

                              }
                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "steps",new BsonDocument()
                            {
                                {
                                    "$gte",30000
                                }
                            }
                        },

                    }
                }
            };
            var aggregateStage4 = new BsonDocument()
            {
                {
                    "$count","passing_scores"
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4 };
            try
            {
                var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                return (int)result["passing_scores"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<List<Day>> GetRangDay(string email, int dayCount)
        {
            var dateRange = new BsonDateTime(DateTime.UtcNow.AddDays(-dayCount).Date);
            var dateNow = new BsonDateTime(DateTime.UtcNow.Date);
            var aggregateStage1 = new BsonDocument()
           {
               {
                   "$match",new BsonDocument() {
                       {"email",email}
                   }
               }
           };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                              "date",new BsonDocument()
                              {
                                  {
                                      "$gte", dateRange
                                  },
                                  {
                                       "$lte",dateNow
                                  }

                              }
                        }
                    }
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2};
            try
            {
                List<Day> result = await _days.Aggregate<Day>(pipeline).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Day>();
            }
            
        }
        private class TotalSteps
        {
            public string _id;
            public string totlaSteps;
            //TotalSteps(BsonDocument doc)
            //{
            //    this._id = doc._id
            //}
        }
    }
}
