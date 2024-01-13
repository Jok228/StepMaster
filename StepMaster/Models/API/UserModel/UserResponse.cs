using Domain.Entity.Main.Titles;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using static StepMaster.Models.Entity.User;

namespace StepMaster.Models.API.UserModel
{
    public class UserResponse
    {

        public string? email { get; set; }

        public string? nickname { get; set; }

        public string? fullname { get; set; }

        public string? role { get; set; }


        public UserRanking? rating { get; set; }


        public string? region_id { get; set; }

        public string? gender { get; set; }

        public string? avatarLink { get; set; }
        public bool vipStatus { get; set; }

        public List<string> titles { get; set; }
        public List<string> selectedTitles { get; set; }


        public UserResponse(User user, UserRanking? ranking, string? avatar)
        {
            if (user != null)
            {
                rating = ranking;
                email = user.email;
                nickname = user.nickname;
                fullname = user.fullname;
                role = user.role;
                gender = user.gender;
                region_id = user.region_id;
                avatarLink = avatar;
                titles = user.titles;
                selectedTitles = user.selectedTitles;
                vipStatus = user.vipStatus;
            }

        }


    }
}
