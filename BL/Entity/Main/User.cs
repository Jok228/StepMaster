using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Globalization;
using MongoDB.Driver;
using Domain.Entity.Main.Titles;

namespace StepMaster.Models.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("nickname")]
        public string nickname { get; set; }
        [BsonElement("fullname")]
        public string fullname { get; set; }
        [BsonElement("role")]
        public string? role { get; set; }    
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("region_id")]
        public string region_id { get; set; }
        [BsonElement("gender")]
        public string gender { get; set; }
        [BsonElement("lastCookie")]
        public string? lastCookie { get; set; }
        [BsonElement("vipStatus")]
        public bool vipStatus{ get; set; }
        [BsonElement("titles")]
        public List<TitleDb> titles { get; set; }
        [BsonElement("selectedTitles")]
        public List<TitleDb> selectedTitles { get; set; }
        public User()
        {
            titles = new List<TitleDb>();
            selectedTitles = new List<TitleDb>();   
        }
        public void UpdateTitles(TitleDb newTitle)
        {
           var filter =  this.titles.Find(titleDb => titleDb.id == newTitle.id & titleDb.groupId == newTitle.groupId & titleDb.type == newTitle.type);
           if(filter != null)
            {
                return;                
            }
           this.titles.Add(newTitle);
        }

        public User UpdateUser( User newValue)
        {
            if(newValue.fullname != null) this.fullname = newValue.fullname;
            if(newValue.nickname != null) this.nickname = newValue.nickname;
            if(newValue.region_id != null) this.region_id = newValue.region_id;
            return this;
        }

        public void UpdateSelectedTitles(TitleDb newTtitles)
        {
            var checkedTitles = this.titles.FirstOrDefault(achievement => achievement.id == newTtitles.id & achievement.groupId == newTtitles.groupId & achievement.type == newTtitles.type);
            if(checkedTitles == null)
            {
                throw new HttpRequestException("The Titles not exists in User.Titles", null, System.Net.HttpStatusCode.BadRequest);
            }
            if(this.selectedTitles.Count == 3)
            {
                this.selectedTitles.Remove(selectedTitles[0]);
                this.selectedTitles.Add(checkedTitles);
            }
            else
            {
                this.selectedTitles.Add(checkedTitles);
            }
          
        }

        public TitleDb GetLastAchievemtn(List<Condition> listConditions)
        {

            var result = this.titles.LastOrDefault(ach => ach.groupId == 3 & ach.type == "achievement");
            if (result != null)
            {
                var condition = listConditions.Find(con => con.localId == result.id+1 & con.type == "achievement" & con.groupId == result.groupId);
                return new TitleDb
                {
                    id = condition.localId,
                    groupId = result.groupId,
                    name = result.name,
                    type = result.type,
                };
            }
            return new TitleDb
            {
                name = "10 км",
                id = 1,
                groupId = 3,
                type = "achievement"
            };
        }
    }
    
}
