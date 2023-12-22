using StepMaster.Models.Entity;

namespace StepMaster.Models.API.UserModel;

public class UserEditModel
{
    public string? nickname { get; set; }

    public string? fullname { get; set; }

    public string? region_id { get; set; }

    public User ConvertToBase()
    {
        return new User()
        {
            fullname = fullname,
            nickname = nickname,
            region_id = region_id,
        };
    }

}
