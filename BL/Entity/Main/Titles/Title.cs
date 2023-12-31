using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Main.Titles
{
    public class Title
    {
        public int id { get; set; }
        public string name { get; set; }

        public string link { get; set; }

      

    }
    public class GroupTitle
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public List<Title> data { get; set; }
        public GroupTitle(string? name = "none", int id = 0)
        {
            this.name = name;
            this.id = id;
            data = new List<Title>();
        }
        public void SortById()
        {
           this.data = data.OrderBy(titles => titles.id)
                .ToList();
        }
    }
}
