using StepMaster.Models.Entity;

namespace StepMaster.Models.API.UserModel;

public class UserEditModel
{
    public string? nickname { get; set; }

    public string? fullname { get; set; }

    public string? region_id { get; set; }

    public User ConvertToBase(User oldUser)
    {
       if(this.fullname != null)oldUser.fullname = this.fullname;
       if(this.region_id != null)oldUser.region_id = this.region_id;
       if(this.nickname != null)oldUser.nickname = this.nickname;
       return oldUser;
    }

}
