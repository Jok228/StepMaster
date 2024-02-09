using StepMaster.Models.Entity;


namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IUser_Service
    {



        #region CRUD
        public Task<User> GetByLoginAsync(string login);

        public Task<User> GetUserbyCookie(string cookies);

        public Task<User> RegUserAsync(User newUser);

        public Task<User> UpdateUser(User userUpdate);

        public Task<string> DeleteUser(string user);


        #endregion

        #region ViewMethods

        public Task<List<User>> GetUsers(string searchText,int page,string? myRegionId );


        #endregion

        #region InfrastuctMethods
        public Task<bool> CheckUser(string login);

        public Task<bool> DeleteCookie(string userEmail);

        public Task UpdateLastBeOnline(string user);
        public Task<string> GetActualAchievementUser(string userEmail);

        public Task<User> EditPassword(string email, string newPassword, string oldPassword);

        #endregion





        #region friends system

        public Task<User> CreateRequestInFriend(string emailUser,string emailFriend);

        public Task<User> AcceptRequest (string emailUser, string emailFriend);

        public Task<User> DenyRequest(string emailMain, string emailFriend);

        public Task<User> AddInBlockedList(string emailMain, string emailTarget);

        public Task<User> RemoveFromBlockedList(string emailMain, string emailTarget);

        public Task<User> RemoveFriend(string emailMain, string emailTarget);

        public Task<User> RemoveRequest(string emailMain, string emailTarget);

        public Task<User> GetFriends(string emailMain, int page);       

        #endregion
    }
}
