using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Globalization;
using MongoDB.Driver;
using Domain.Entity.Main.Titles;
using Domain.Entity.Main;

namespace StepMaster.Models.Entity
{
    public class User:EntityDb
    {        
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("nickname")]
        public string NickName { get; set; }
        [BsonElement("fullname")]
        public string FullName { get; set; }
        [BsonElement("role")]
        public string? Role { get; set; }    
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("region_id")]
        public string RegionId { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("lastCookie")]
        public string? LastCookie { get; set; }
        [BsonElement("fireBaseToken")]
        public string? FireBaseToken { get; set; }
        [BsonElement("vipStatus")]
        public bool VipStatus{ get; set; }
        [BsonElement("titles")]
        public List<string> Titles { get; set; }
        [BsonElement("selectedTitles")]
        public List<string> SelectedTitles { get; set; }
        [BsonElement("blockedUsers")]
        public List<string> BlockedUsers { get; set; }
        [BsonElement("requrequestInFriends")]
        public List<string> RequrequestInFriends { get; set; }
        [BsonElement("friends")]
        public List<string> Friends { get; set; }
        [BsonElement("lastBeOnline")]
        public DateTime LastBeOnline { get; set; }
        [BsonIgnore]
        public string clanId { get; set; }

        [BsonIgnore]
        public string  actualTitle{ get; set; }

        public User()
        {
            Friends = new List<string>();
            RequrequestInFriends = new List<string>();
            BlockedUsers = new List<string>();
            Titles = new List<string>();
            SelectedTitles = new List<string>();   
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
            if (this.Titles.Contains(newTitle._id.ToString()))
            {
                return;
            }
            this.Titles.Add(newTitle._id.ToString());
        }

        public User UpdateUser( User newValue)
        {
            if(newValue.FullName != null) this.FullName = newValue.FullName;
            if(newValue.NickName != null) this.NickName = newValue.NickName;
            if(newValue.RegionId != null) this.RegionId = newValue.RegionId;
            return this;
        }

        public void UpdateSelectedTitles(string conditionMongoId)
        {
            if (this.Titles.Contains(conditionMongoId))
            {
                if(this.SelectedTitles.Count >= 3)
                {
                    this.SelectedTitles.RemoveAt(0);
                }
                this.SelectedTitles.Add(conditionMongoId);
            }
            else
            {
                throw new HttpRequestException("The Titles not exists in User.Titles", null, System.Net.HttpStatusCode.BadRequest);
            }
        }

    }
    
}
