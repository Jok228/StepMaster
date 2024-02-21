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
                Email = regModel.email,
                NickName = regModel.nickname,
                FullName = regModel.fullname,
                Password = HashCoder.GetHash(regModel.password),
                RegionId = regModel.region_id,
                Gender = regModel.gender,
                Role = "user",
                VipStatus = true,
                LastBeOnline = DateTime.UtcNow,
                

            };
        }

    }
}
