using Amazon.S3.Model.Internal.MarshallTransformations;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.DbHelper;
using Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
using static System.Net.WebRequestMethods;

namespace Infrastructure.MongoDb.Repositories
{
    public class Clan_Repository : IClan_Repository
    {
        private readonly IMongoCollection<Clan> _clansDb;
        private IMy_Cache _cache;
        private string _keyCacheGetObject = "GetOneClan";
        private string _keyCacheListMaxToMin = "MaxToMinClans";
        private string _keyCacheListMinToMax = "MinToMaxClans";
        private string _keyCacheListRating = "RatingListClans";
        private int _entityOnPage = 15;
        private ILogger<Day_Repository> _logger;
        public Clan_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient, ILogger<Day_Repository> logger)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _clansDb = database.GetCollection<Clan>(TableName.Clan);
            _cache = cache;
            _logger = logger;
        }


        public async Task<Clan> GetObjectBy(string idOrEmail)
        {
            try
            {
                var clan = (Clan)_cache.GetObject(_keyCacheGetObject + idOrEmail);
                if (clan == null)
                {
                    clan = await _clansDb.FindAsync(clan => clan._id == idOrEmail).Result.FirstAsync();
                    _cache.SetObject(_keyCacheGetObject + idOrEmail, clan, 10);
                }
                return clan;
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    throw new HttpRequestException("404 Not Found", null, HttpStatusCode.NotFound);
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }

        }

        public async Task<Clan> SetObject(Clan value)
        {
            try
            {
                await _clansDb.InsertOneAsync(value);
                var response = GetObjectBy(value._id);
                _cache.SetObject(_keyCacheGetObject + value._id.ToString(), value, 10);
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }

        public async Task<Clan> UpdateObject(Clan newValue)
        {
            try
            {
                var filter = Builders<Clan>.Filter.Eq("_id", newValue._id);
                await _clansDb.ReplaceOneAsync(filter, newValue);
                _cache.DeleteObject(_keyCacheGetObject + newValue._id.ToString());
                _cache.SetObject(newValue._id, newValue, 10);
                return newValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
            }
        }
        public async Task<Clan> DeleteObject(string forigenKey)
        {
            var response = await GetObjectBy(forigenKey);
            await _clansDb.DeleteOneAsync(clan => clan._id == response._id);
            _cache.DeleteObject(_keyCacheGetObject + response._id);
            _cache.DeleteObject(_keyCacheListRating);
            return response;
        }

        public async Task<List<ClanLite>> GetClansMinToMax(string nameSearch,int numberPage = 0)
        {
            int skip = numberPage * _entityOnPage;
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$project",new BsonDocument() {

                       {
                           "full_steps",new BsonDocument()
                           {
                               {
                               "$sum","$ratingUsers.step"
                               }
                           }
                       },
                       {
                           "name",1

                       },
                       {
                           "region_name",1
                       },

                       {
                           "count_users",new BsonDocument()
                           {
                               {
                               "$size","$ratingUsers"
                               }
                           }
                       }
                   }
                }
           };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "name", new BsonDocument()
                            {

                                {
                                "$regex",nameSearch
                                }

                            }

                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
           {
               {
                   "$sort", new BsonDocument()
                   {
                       {
                           "full_steps",1
                       }
                   }
               }
           };
            var aggregateStage4 = new BsonDocument()
            {
                {
                    "$skip",skip
                }
            };
            var aggregateStage5 = new BsonDocument()
                {
                    {
                        "$limit",_entityOnPage
                    }
                };
            var aggregateStage6 = new BsonDocument()
            {
                {
                    "$project",new BsonDocument()
                    {
                        {
                            "full_steps",0
                        }
                    }
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4, aggregateStage5, aggregateStage6 };
            try
            {
                var result = await _clansDb.Aggregate<ClanLite>(pipeline).ToListAsync();
                _cache.SetObject(_keyCacheListMinToMax, result, 30);

                return result;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
            
        }
        public async Task<List<ClanLite>> GetClansMaxToMin(string nameSearch, int numberPage = 0)
        {
            int skip = numberPage * _entityOnPage;


            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$project",new BsonDocument() {
                       {
                           "full_steps",new BsonDocument()
                           {
                               {
                               "$sum","$ratingUsers.step"
                               }
                           }
                       },
                       {
                           "name",1

                       },
                       {
                           "region_name",1
                       },

                       {
                           "count_users",new BsonDocument()
                           {
                               {
                               "$size","$ratingUsers"
                               }
                           }
                       }

                   }
                }
           };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "name", new BsonDocument()
                            {

                                {
                                "$regex",nameSearch
                                }

                            }

                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
           {
               {
                   "$sort", new BsonDocument()
                   {
                       {
                           "full_steps",-1
                       }
                   }
               }
           };
            var aggregateStage4 = new BsonDocument()
            {
                {
                    "$skip",skip
                }
            };
            var aggregateStage5 = new BsonDocument()
                {
                    {
                        "$limit",_entityOnPage
                    }
                };
            var aggregateStage6 = new BsonDocument()
            {
                {
                    "$project",new BsonDocument()
                    {
                        {
                            "full_steps",0
                        }
                    }
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4, aggregateStage5, aggregateStage6 };
            var result = await _clansDb.Aggregate<ClanLite>(pipeline).ToListAsync();
            _cache.SetObject(_keyCacheListMaxToMin, result, 30);

            return result;
        }
        public async Task<List<ClanLite>> GetClansMyRegion(string nameSearch, string regionName,  int numberPage = 0)
        {
            int skip = numberPage * _entityOnPage;
            var aggregateStage1 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "region_name",regionName
                        }
                    }
                }
            };
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "name", new BsonDocument()
                            {

                                {
                                "$regex",nameSearch
                                }

                            }

                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$skip",skip
                }
            };
            var aggregateStage4 = new BsonDocument()
                {
                    {
                        "$limit",_entityOnPage
                    }
                };
            var aggregateStage5 = new BsonDocument()
            {
                {
                    "$project",new BsonDocument()
                    {
                        {
                            "name",1
                        },
                        {
                            "region_name",1
                        },
                        {
                            "count_users", new BsonDocument()
                            {
                                {
                                    "$size","$ratingUsers"
                                }
                            }

                        }


                    }
                }
            };

            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4, aggregateStage5 };
            return await _clansDb.Aggregate<ClanLite>(pipeline).ToListAsync();
        }

        public async Task<List<ClanLite>> GetClansByFreePlace(string nameSearch,int numberPage = 0)
        {
            var aggregateStage1 = new BsonDocument("$project", new BsonDocument
    {
        { "name", 1 },
        { "region_name", 1 },
        { "count_users", new BsonDocument("$size", "$ratingUsers") },
        { "count_free_plase", new BsonDocument("$subtract", new BsonArray
          {
              new BsonDocument("$add", new BsonArray{"$max_users"}),
              new BsonDocument("$size", "$ratingUsers")
          }
          )
        }
    });
            var aggregateStage2 = new BsonDocument()
            {
                {
                    "$match",new BsonDocument()
                    {
                        {
                            "name", new BsonDocument()
                            {
                                
                                {
                                "$regex",nameSearch
                                }

                            }
                            
                        }
                    }
                }
            };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$sort",new BsonDocument()
                    {
                        {
                            "count_free_plase",-1
                        }
                    }
                }
            };
            var aggregateStage4 = new BsonDocument()
                {
                    {
                        "$limit",_entityOnPage
                    }
                };
            var aggregateStage5 = new BsonDocument()
            {
                {
                    "$project",new BsonDocument()
                    {
                        {
                         "count_free_plase",0
                        }
                    }
                }
            };
            try
            {
                var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4,aggregateStage5 };
                return await _clansDb.Aggregate<ClanLite>(pipeline).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }


        }

        public async Task<List<string>> UpdateRatingClans()
        {
            var listResult = new List<string>();
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$project",new BsonDocument() {
                       {
                           "full_steps",new BsonDocument()
                           {
                               {
                               "$sum","$ratingUsers.step"
                               }
                           }
                       },

                   }
                }
           };
            var aggregateStage2 = new BsonDocument()
           {
               {
                   "$sort", new BsonDocument()
                   {
                       {
                           "full_steps",-1
                       }
                   }
               }
           };
            var aggregateStage3 = new BsonDocument()
            {
                {
                    "$project",new BsonDocument()
                    {
                        {
                            "_id", new BsonDocument()
                            {
                                {
                                    "$toString","$_id"
                                }
                            }
                        }
                    }
                }
            };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3 };
            try
            {
                var result = await _clansDb.Aggregate<BsonDocument>(pipeline).ToListAsync();
                foreach (var resultItem in result)
                {
                    listResult.Add((string)resultItem["_id"]);
                }
                _cache.SetObject(_keyCacheListRating, listResult, 60);
                return listResult;
            }
            catch (Exception ex) 
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return new List<string>();
                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
            
        }
        public async Task<string> GetNumberOfPlace(string mongoId)
        {
            var listRatingClans = (List<string>)_cache.GetObject(_keyCacheListRating);
            if (listRatingClans == null)
            {
                listRatingClans = await UpdateRatingClans();
            }
            return $"{listRatingClans.FindIndex(Ids => Ids == mongoId) + 1}/{await GetCount()}" ;
        }

        public async Task<Clan> GetClanByUser(string email)
        {
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$match",new BsonDocument() {
                       {
                           "ratingUsers.email",email
                       },

                   }
                }
           };
            var pipeline = new BsonDocument[] { aggregateStage1 };
            try
            {
                return await _clansDb.Aggregate<Clan>(pipeline).FirstAsync();
            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return new Clan();

                }               
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);

            }
        }

        public async Task<Clan> GetClanOrNullByName(string name,string regionName)
        {
            return await _clansDb.FindAsync(clan => clan.Name == name && clan.RegionName == regionName ).Result.FirstOrDefaultAsync();
        }

        public async Task<long> GetCount()
        {            
            try
            {

                return await _clansDb.CountDocumentsAsync(new BsonDocument());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<string> GetNumberOfPlaceRegion(string regionName, string mongoId)
        {
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$match",new BsonDocument()
                   {
                       {
                       "region_name",regionName
                       }

                   }
                }
           };
            var aggregateStage2 = new BsonDocument()
           {

                {
                   "$project",new BsonDocument()
                   {
                       {
                           "full_steps", new BsonDocument()
                           {
                               {
                                    "$sum","$ratingUsers.step"
                               }
                           }
                          
                       }
                   }
                }
           };
            var aggregateStage3 = new BsonDocument()
           {

                {
                   "$sort",new BsonDocument()
                   {
                       {
                           "full_steps",-1
                       }
                   }
                }
           };
            var aggregateStage4 = new BsonDocument()
           {

                {
                   "$addFields",new BsonDocument()
                   {
                       {
                           "id",new BsonDocument()
                           {
                               {
                                   "$toString","$_id"
                               }
                           }
                       }
                   }
                }
           };
            var aggregateStage5 = new BsonDocument()
           {

                {
                   "$project",new BsonDocument()
                   {
                       {
                           "full_steps",0
                       },
                       {
                           "_id",0
                       }

                   }
                }
           };

            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2, aggregateStage3, aggregateStage4, aggregateStage5 };
            try
            {
                var stp =  await _clansDb.Aggregate<BsonDocument>(pipeline).ToListAsync();
                var listResult = new List<string>();
                foreach(BsonDocument doc in stp)
                {
                    listResult.Add(doc["id"].ToString());
                }
                return $"{listResult.IndexOf(mongoId)+1}/{await GetCountClansInRegion(regionName)}";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<long> GetCountClansInRegion (string regionName)
        {
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$match",new BsonDocument()
                   {
                       {
                       "region_name",regionName
                       }

                   }
                }
           };
            var aggregateStage2 = new BsonDocument()
           {

                {
                   "$count","count"
                }
           };
            var pipeline = new BsonDocument[] { aggregateStage1, aggregateStage2 };
            try
            {
                var count = (int) _clansDb.Aggregate<BsonDocument>(pipeline).FirstAsync().Result["count"];
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> CheckClansByUser(string email)
        {
            var aggregateStage1 = new BsonDocument()
           {

                {
                   "$match",new BsonDocument() {
                       {
                           "ratingUsers.email",email
                       },

                   }
                }
           };
            var pipeline = new BsonDocument[] { aggregateStage1 };
            try
            {
                var check =  await _clansDb.Aggregate<Clan>(pipeline).FirstAsync();
                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message == DbExMessage.NoElements)
                {
                    return false;

                }
                _logger.LogError(ex.Message);
                throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.InternalServerError);

            }
        }

        public async Task<Clan> GetClansById(string mongoId)
        {
            return await _clansDb.FindAsync(clan => clan._id == mongoId).Result.FirstAsync();
        }
    }
}
