using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;

namespace StepMaster.Models.API.UserModel
{
    public class UserResponse
    {

        public string? email { get; set; }

        public string? nickname { get; set; }

        public string? fullname { get; set; }

        public string? role { get; set; }


        public PlaceUserOnRating? rating { get; set; }


        public string? region_id { get; set; }

        public string? gender { get; set; }

        public string? avatarLink { get; set; }

        public UserResponse(User user)
        {
            if (user != null)
            {
                rating = user.rating;
                email = user.email;
                nickname = user.nickname;
                fullname = user.fullname;
                role = user.role;
                gender = user.gender;
                region_id = user.region_id;
                avatarLink = user.AvatarLink;
            }

        }


    }
}
