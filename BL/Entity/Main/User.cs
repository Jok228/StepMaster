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
        public List<string> titles { get; set; }
        [BsonElement("selectedTitles")]
        public List<string> selectedTitles { get; set; }

        public User()
        {
            titles = new List<string>();
            selectedTitles = new List<string>();   
        }

        //public class TitleUser
        //{

        //    [BsonElement("idAchievemen")]
        //    public int id { get; set; }
        //    [BsonElement("idGroup")]
        //    public int groupId { get; set; }

        //    [BsonElement("type")]
        //    public string type { get; set; }

        //    [BsonElement("name")]
        //    public string name { get; set; }

        //}


        public void UpdateTitles(Condition newTitle)
        {
            if (this.titles.Contains(newTitle._id.ToString()))
            {
                return;
            }
            this.titles.Add(newTitle._id.ToString());
        }

        public User UpdateUser( User newValue)
        {
            if(newValue.fullname != null) this.fullname = newValue.fullname;
            if(newValue.nickname != null) this.nickname = newValue.nickname;
            if(newValue.region_id != null) this.region_id = newValue.region_id;
            return this;
        }

        public void UpdateSelectedTitles(string conditionMongoId)
        {
            if (this.titles.Contains(conditionMongoId))
            {
                if(this.selectedTitles.Count >= 3)
                {
                    this.selectedTitles.RemoveAt(0);
                }
                this.selectedTitles.Add(conditionMongoId);
            }
            else
            {
                throw new HttpRequestException("The Titles not exists in User.Titles", null, System.Net.HttpStatusCode.BadRequest);
            }
        }

    }
    
}
