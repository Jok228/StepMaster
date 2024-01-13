

using Domain.Entity.Main.Titles;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace StepMaster.Models.API.Title
{
    public class ConditionResponse
    {
        public string _id {  get; set; } 

        public string type { get; set; }

        public string name { get; set; }

        public string groupName { get; set; }

        public string link { get; set; }

        public string needed_progress { get; set; }

        public string dealt_progress { get; set; }

        public string desc { get; set; }

        public static List<ConditionResponse> Convert(List<Condition>list)
        {
            var result = new List<ConditionResponse>();

            foreach(var condition in list)
            {
                result.Add(new ConditionResponse()
                {
                    _id = condition._id.ToString(),
                    type = condition.Type,
                    name = condition.Name,
                    groupName = condition.GroupName,
                    link = condition.awsLink,
                    dealt_progress = condition.Dealt_Progress,
                    needed_progress = condition.Needed_Progress,
                    desc = condition.Description,
                });
            };
            return result;

            

        }

    }
}
