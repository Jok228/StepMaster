using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main.Room
{
    public class Room : EntityDb
    {
        [BsonElement ("users")]
        public List<string> Users { get; set; }
        [BsonElement ("messages")]
        public List<string> Messages { get; set; }
        [BsonElement ("is_public")]
        public bool IsPublic { get; set; }
        [BsonElement ("name")]
        public string Name { get; set; }

        public Room()
            {
            Users = new List<string>();

            Messages = new List<string>();

            }
        public void RemoveUser(string userEmail)
            {
            if (this.Users.Contains (userEmail))
                {
                this.Users.Remove (userEmail);
                }
            }

        public void AddUser(string userEmail)
            {
            if (!this.Users.Contains (userEmail))
                {
                this.Users.Add (userEmail);
                }
            }
        public void AddMessage(string messageId)
            {
            if (!this.Messages.Contains (messageId))
                {
                this.Messages.Add (messageId);
                }
            }
        public void RemoveMessage(string messageId)
            {
            if (this.Messages.Contains (messageId))
                {
                this.Messages.Remove (messageId);
                }
            }

        }
}
