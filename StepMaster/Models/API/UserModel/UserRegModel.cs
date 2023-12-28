using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using System.Runtime.CompilerServices;

namespace StepMaster.Models.API.UserModel
{
    public class UserRegModel
    {
        public string email { get; set; }

        public string nickname { get; set; }

        public string fullname { get; set; }

        public string password { get; set; }

        public string region_id { get; set; }

        public string gender { get; set; }

        static public User GetFullUser(UserRegModel regModel)
        {
            return new User()
            {
                email = regModel.email,
                nickname = regModel.nickname,
                fullname = regModel.fullname,
                password = HashCoder.GetHash(regModel.password),
                region_id = regModel.region_id,
                gender = regModel.gender,
                role = "user"
            };
        }

    }
}
