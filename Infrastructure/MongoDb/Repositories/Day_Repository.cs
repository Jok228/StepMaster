using Amazon.S3.Model;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Linq;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.MongoDb.Repositories
{
    public class Day_Repository : IDay_Repository

    {
        private readonly IMongoCollection<Day> _days;
        private ILogger<Day_Repository> _logger;
        
        private IMy_Cache _cache;
        public Day_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient, ILogger<Day_Repository> logger)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _days = database.GetCollection<Day>(TableName.Day);
            _cache = cache;
            _logger = logger;
            _logger.LogDebug("NLog is integrate to Day_Controler ");
        }
        #region Проверка есть ли день с такой же датой
        public async Task<bool> ChechDayDateNow(string email)
        {
            try
            {
                var day = await _days.FindAsync(day => day.email == email
                && day.date.Date == DateTime.UtcNow.Date)
                   .Result
                   .FirstAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return false;
                }
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        #endregion

        #region Получить актуальный день
        public async Task<List<Day>> GetActualDay(string email)
        {
            try
            {
                var days = await _days.FindAsync(day => day.email == email
                && day.date.Month == DateTime.UtcNow.Month
                && day.date.Year == DateTime.UtcNow.Year)
                    .Result
                    .ToListAsync();
                return days;

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NotFound)
                {
                    return new List<Day>();
                }
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);

            }
        }
        #endregion

        #region Получить дни юзера
        public async Task<List<Day>> GetDaysByEmail(string email)
        {
            try
            {
                var days = await _days.FindAsync(day => day.email == email)
                    .Result
                    .ToListAsync();
                return days;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NotFound)
                {
                    return new List<Day>();
                }
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region Получить объект
        public async Task<Day> GetObjectBy(string idOrEmail)
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
                    return day;
                }
                return day;


            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region Взять количество шагов пользователя в диапозоне
        public async Task<int> GetAllStepsUser(string email, int? dayCount)
        {
            try
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
                if (ex.Message == DbExMessage.NoElements)
                {                    
                    return 0;
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }

        }
        #endregion

        #region Взять количество шагов пользователя в дня у которых меньше 30к шагов, в диапозоне
        public async Task<int> GetStepRangeForGrades(string email)
        {
            
            
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
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage3, aggregateStage4 };
            try
            {
                var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                return (int)result["totalSteps"];
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return 0;

                }
                _logger.LogError("GetStepRangeForGrades");
                _logger.LogError($"{ex.Message}");
                return 0;
            }
        }
        #endregion

        #region Добавить объект
        public async Task<Day> SetObject(Day value)
        {
            try
            {
                await _days.InsertOneAsync(value);
                _cache.SetObject(value._id, value, 10);
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region Обновить объект
        public async Task<Day> UpdateObject(Day newValue)
        {
            try
            {
                var filter = Builders<Day>.Filter.Eq("_id", newValue._id);
                await _days.ReplaceOneAsync(filter, newValue);
                _cache.DeleteObject(newValue._id);
                _cache.SetObject(newValue._id, newValue, 10);
                return newValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region Взять количество дней которые больше 30к, в диапозоне
        public async Task<int> GetCountDayMoreMax(string email)
        {
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
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage3, aggregateStage4 };
            try
            {
                var result = await _days.Aggregate<BsonDocument>(pipeline).FirstAsync();
                return (int)result["passing_scores"];
            }
            catch (Exception ex)
            {
                _logger.LogWarning("GetCountDayMoreMax");
                _logger.LogError(ex.Message);

                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        #endregion

        #region Взять диапозон дней
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
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2 };
            try
            {
                List<Day> result = await _days.Aggregate<Day>(pipeline).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("GetCountDayMoreMax");
                _logger.LogError(ex.Message);
                return new List<Day>();
            }

        }

        #endregion

        #region Удалить объект
        public async Task<Day> DeleteObject(string idOrEmail)
        {
            try
            {
                var response = await GetObjectBy(idOrEmail);
                await _days.DeleteOneAsync(day => day._id == idOrEmail);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region Удалить дни пользователя
        public async Task DeleteObjectsByEmail(string email)
        {
            try
            {
                await _days.DeleteManyAsync(day => day.email == email);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
             
        }
        #endregion
    }
}
