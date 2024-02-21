using MongoDB.Bson.Serialization.Attributes;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main
{
    public abstract class ClassWithPosition:EntityDb
    {
        [BsonElement("ratingUsers")]
        public List<Position> RatingUsers { get; set; }
        virtual public ClassWithPosition Sort()
        {
            this.RatingUsers = this.RatingUsers // sort
        .OrderBy(rating => rating.Step)
        .Reverse()
        .ToList();//Create rating but for userDB,
            return this;   
        }
        public string GetUserRanking(string email)
        {
            var filterPlaceInRegion = this.RatingUsers.FirstOrDefault(rating => rating.Email == email);  //filter for find index
            int placeInRegion = this.RatingUsers.IndexOf(filterPlaceInRegion) + 1; //find index
            int userRegion = this.RatingUsers.Count(); //all user in list            
            return $"{placeInRegion}/{userRegion}";
        }
        public void UpdatePosition(Position newPosition)
        {
          var oldPosition =  GetUserPosition(newPosition.Email);
          DeleteUserPosition(oldPosition);
          AddUserPosittion(newPosition);
        }
        public Position GetUserPosition(string email)
        {
           var result = this.RatingUsers.Where(position => position.Email == email).FirstOrDefault();
           if(result.Email == null)
            {
                throw new HttpRequestException("This user is not a member of the clan", null, System.Net.HttpStatusCode.NotFound);
            }
            else
            {
                return result;
            }
        }
        public void DeleteUserPosition(Position position)
        {
            this.RatingUsers.Remove(position);
        }
        public void AddUserPosittion(Position position)
        {
            var positionExist = this.RatingUsers.Find(p => p.Email == position.Email);
            if (positionExist.Email != null)
            {
                throw new HttpRequestException("This user is already a member of the clan", null, System.Net.HttpStatusCode.Conflict);
            }
            else
            {
                this.RatingUsers.Add(position);
                this.Sort();
            }
        }

        public struct Position
        {
            [BsonElement("step")]
            public int Step { get; set; }
            [BsonElement("email")]
            public string Email { get; set; }
            [BsonElement("nickname")]
            public string NickName { get; set; }
        }
    }
}
