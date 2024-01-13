
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.ForDb.APIDatebaseSet;
using Domain.Entity.Main.Titles;
using Infrastructure.MongoDb.Cache.Interfaces;
using Infrastructure.MongoDb.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;

namespace Infrastructure.MongoDb.Repositories
{
    public class Condition_Repository : ICondition_Repository
    {
        private readonly IMongoCollection<Condition> _condition;
        private IMy_Cache _cache;
        private const string _keyList = "listConditions";
        private const string _keyItem = "_condition";
        public Condition_Repository(IMy_Cache cache, IAPIDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _condition = database.GetCollection<Condition>(TableName.Condition);
            _cache = cache;

        }
        public async Task<List<Condition>> GetConditionsAsync()
        {
            try
            {
                var list = (List<Condition>)_cache.GetObject(_keyList);
                if(list == null)
                {
                    list = await _condition.FindAsync(condition => true).Result.ToListAsync();
                    //foreach (var item in list)
                    //{
                    //    if (item.Type == "achievement" & item.GroupId == 1)
                    //    {
                    //        item.TimeDay = 0;
                    //        item.Description = $"Вам нужно за один день пройти - {item.Distance} шагов";
                    //    }
                    //    if (item.Type == "achievement" & item.GroupId == 2)
                    //    {
                    //        item.Distance = null;
                    //        var text = string.Empty;
                    //        if (item.TimeDay != 1)
                    //        {
                    //            text = "дней";
                    //        }
                    //        else
                    //        {
                    //            text = "дня";
                    //        }
                    //        item.Description = $"Для получения этого достижения вам нужно достигать поставленной цели на протяжении - {item.TimeDay} {text}";
                    //    }
                    //    if (item.Type == "achievement" & item.GroupId == 3)
                    //    {
                    //        item.Description = $"Для получения этого  достижения вам нужно суммарно нужно пройти - {item.Name} км";
                    //    }
                    //    if (item.Type == "achievement" & item.GroupId == 4)
                    //    {
                    //        item.Description = $"Для получения этого достижения вам нужно суммарно нужно пройти - {item.Distance} шагов";
                    //    }
                    //    if (item.Type == "grade" & item.GroupId != 5 & item.GroupId != 6 & item.GroupId != 7 & item.GroupId != 8)
                    //    {
                    //        item.TimeDay = null;
                    //        item.Description = $"Для получения этого звания вам нужно преодолеть - {item.Distance} шагов";
                    //    }
                    //    if (item.Type == "grade" & item.GroupId == 5)
                    //    {
                    //        item.TimeDay = null;
                    //        item.Distance = null;
                    //        item.Description = $"Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц";
                    //    }
                    //    if (item.Type == "grade" & item.GroupId == 6)
                    //    {
                    //        item.TimeDay = null;
                    //        item.Distance = null;
                    //        item.Description = $"Для получения этого звания вам нужно стать лучшим в рейтинге страны за год";
                    //    }
                    //    if (item.Type == "grade" & item.GroupId == 7)
                    //    {
                    //        item.TimeDay = null;
                    //        item.Distance = null;
                    //        item.Description = $"Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц";
                    //    }
                    //    if (item.Type == "grade" & item.GroupId == 8)
                    //    {
                    //        item.TimeDay = null;
                    //        item.Distance = null;
                    //        item.Description = $"Для получения этого звания вам нужно стать победителем денежного розыгрыша";
                    //    }

                    //}
                    //await _condition.DeleteManyAsync(a => true);
                    //await _condition.InsertManyAsync(list);
                }
                _cache.SetObject(_keyList, list,600);
                return list;
            }
            catch 
            {
                throw new HttpRequestException("MongoDb ..", null, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Condition> GetOneAsync(string mongoId)
        {
            try
            {
                var response = (Condition)_cache.GetObject(_keyItem+mongoId);
                if(response == null)
                {
                   response = await _condition.FindAsync(condition => condition._id == mongoId)
                        .Result
                        .FirstAsync();
                    _cache.SetObject(_keyList, response, 600);
                }
                return response;
            }
            catch
            {
                throw new HttpRequestException("Shit happens", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
