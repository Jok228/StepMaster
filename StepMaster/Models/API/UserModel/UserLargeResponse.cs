using DnsClient;
using Domain.Entity.Main.Titles;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using static StepMaster.Models.Entity.User;

namespace StepMaster.Models.API.UserModel
{
    
    public abstract class UserBaseResponse
    {
        public string? email { get; set; }

        public string? nickname { get; set; }

        public string? fullname { get; set; }

        public string? clanId { get; set; }

        public string? role { get; set; }

        public string? region_id { get; set; }

        public string? gender { get; set; }

        public bool vipStatus { get; set; }
        public List<string> titles { get; set; }
        public List<string> selectedTitles { get; set; }
        public List<string> friends { get; set; }

        public bool isOnline { get; set; }

        public string actualTitleId { get; set; }

        public List<string> blockedUsers { get; set; }

        public List<string> requrequestInFriends { get; set; }
        public UserBaseResponse(User user)
        {
            if (user != null)
            {
                if (DateTime.UtcNow < user.LastBeOnline.AddMinutes(5))
                {
                    this.isOnline = true;
                }
                else
                {
                    this.isOnline = false;
                }

                actualTitleId = user.actualTitleList;

                clanId = user.clanId;
                email = user.Email;
                nickname = user.NickName;
                fullname = user.FullName;
                role = user.Role;
                gender = user.Gender;
                region_id = user.RegionId;
                titles = user.Titles;
                selectedTitles = user.SelectedTitles;
                vipStatus = user.VipStatus;

                friends = user.Friends;
                blockedUsers = user.BlockedUsers;
                requrequestInFriends = user.RequrequestInFriends;
            }

        }
    }
    public class UserLiteResponse : UserBaseResponse
    {
        public UserLiteResponse(User user) : base(user)
        {

        }
    }
    public class UserLargeResponse:UserBaseResponse
    { 
        public UserRanking? rating { get; set; }
        public string avatarLink { get; set; }
        public List<UserFriendHideResponse> friends { get; set; }
        public UserLargeResponse(User user, UserRanking ranking, string avatar,List<UserFriendHideResponse> friends) : base(user)

        {
            if (user != null)
            {
                this.friends = friends;
                this.actualTitleId = actualTitleId;
                rating = ranking;                
                avatarLink = avatar;                
            }
        }       
    }
    #region Friends Pages
    public class UserFriendHideResponse
    {
        public string? email { get; set; }
        public bool isOnline { get; set; }
        public UserFriendHideResponse(User user)
        {
            email = user.Email;
            if (DateTime.UtcNow < user.LastBeOnline.AddMinutes(5))
            {
                this.isOnline = true;
            }
            else
            {
                this.isOnline = false;
            }
        }


    }
    public class UserFriendResponse: UserFriendHideResponse
    {
        public UserFriendResponse(User user) : base(user)
        {
            fullName = user.FullName;
        }

        public string fullName { get; set; }

        public string avatarLink { get; set; }

        public string idActualTitle { get; set; }
    }
    public class UserSearchResponse : UserFriendResponse
    {
        public string nickName { get; set; }
        public Relative relative { get; set; }
        public UserSearchResponse(User user,Relative relative) : base(user)
        {
            nickName = user.NickName;
            this.relative = relative;
        }
    }
    public enum Relative
    {
        Friend  = 0,
        Request = 1,
        None = 2,
        Ban = 3,
    }

    #endregion

}
