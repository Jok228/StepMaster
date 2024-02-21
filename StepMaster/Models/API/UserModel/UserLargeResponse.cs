using DnsClient;
using Domain.Entity.Main.Titles;
using Newtonsoft.Json;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using static StepMaster.Models.API.UserModel.UserLargeResponse;
using static StepMaster.Models.Entity.User;

namespace StepMaster.Models.API.UserModel
    {

    public abstract class UserBaseResponse : UserSearchResponse
        {
        protected UserBaseResponse ( User user ) : base (user)
            {
            clanId = user.clanId;
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

        public string? clanId { get; set; }

        public string? role { get; set; }

        public string? region_id { get; set; }

        public string? gender { get; set; }

        public bool vipStatus { get; set; }
        public List<string> titles { get; set; }
        public List<string> selectedTitles { get; set; }
        public List<string> friends { get; set; }

        public List<string> blockedUsers { get; set; }

        public List<string> requrequestInFriends { get; set; }

        }
    public class UserLiteResponse : UserBaseResponse
        {
        public UserLiteResponse ( User user ) : base (user)
            {

            }
        }
    public class UserLargeResponse : UserBaseResponse
        {
        public UserRanking? rating { get; set; }
        public List<UserFriendHideResponse> detailedFriendsList { get; set; }
        public UserLargeResponse ( User user,UserRanking ranking,string avatar,List<UserFriendHideResponse> friends ) : base (user)
            {
            if (user != null)
                {
                this.detailedFriendsList = friends;
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
        public UserFriendHideResponse ( User user )
            {
            email = user.Email;
            if (DateTime.UtcNow < user.LastBeOnline.AddMinutes (5))
                {
                this.isOnline = true;
                }
            else
                {
                this.isOnline = false;
                }
            }


        }
    public class UserFriendResponse : UserFriendHideResponse
        {
        public UserFriendResponse ( User user ) : base (user)
            {
            fullName = user.FullName;
            idActualTitle = user.actualTitle;
            }

        public string fullName { get; set; }

        public string avatarLink { get; set; }

        public string idActualTitle { get; set; }
        }
    public class UserSearchResponse : UserFriendResponse
        {
        public string nickName { get; set; }
        public Relative? relative { get; set; } = null;
        public UserSearchResponse ( User user ) : base (user)
            {
            nickName = user.NickName;
            }
        public static Relative SetRelative ( User userResponse,User mainUser )
            {

            if (mainUser.Friends.Contains (userResponse.Email))
                {
                return Relative.Friend;
                }
            if (mainUser.RequrequestInFriends.Contains (userResponse.Email))
                {
                return Relative.RequestToMe;
                
                }
            if (userResponse.RequrequestInFriends.Contains (mainUser.Email))
                {
                return Relative.RequestToIt;
               
                }
            if (userResponse.BlockedUsers.Contains (mainUser.Email))
                {
                return Relative.BanToMe;
                ;
                }
            if (mainUser.BlockedUsers.Contains (userResponse.Email))
                {
                return Relative.BanToIt;
               
                }
            return Relative.None;
            }

        }
    public enum Relative
        {
        Friend = 0,
        RequestToIt = 1,
        None = 2,
        BanToIt = 3,
        BanToMe = 4,
        RequestToMe = 5,
        }

    #endregion

    }

