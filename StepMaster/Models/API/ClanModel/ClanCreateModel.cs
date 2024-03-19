using Domain.Entity.Main;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;
using static Domain.Entity.Main.ClassWithPosition;

namespace StepMaster.Models.API.ClanModel
{
    public class ClanCreateModel
    {
        
        public string Name { get; set; }
     
        public string Description { get; set; }
    
        public int MaxUsers { get; set; }

        public Clan ConvertToClan(string email,int steps,string nickName,string regionName)
        {
            if(this.MaxUsers<1)
            {
                throw new HttpRequestException("Field - 'Max User in a clan' can not be less 0 and more 500",null,HttpStatusCode.BadRequest);
            }
            return new Clan { Name = this.Name,
                Description = this.Description,
                MaxUsers = this.MaxUsers,
                RegionName = regionName,
                OwnerUserEmail = email,
                DataCreate = DateTime.UtcNow,
                RatingUsers = new List<Position>() { new Position() { Step = steps, Email = email, NickName= nickName } }
                };
        }
    }
    public class ClanResponseModel:Clan
    {
        public int plaseOfRegion { get; set; }

        public int countClanInRegion { get; set; }

    }
}
