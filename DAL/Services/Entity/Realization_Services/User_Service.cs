using MongoDB.Driver;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using StepMaster.Services.ForDb.Interfaces;
using Application.Repositories.S3.Interfaces;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.Entity.Realization_Services;
using Domain.Entity.Main.Titles;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;

namespace StepMaster.Services.ForDb.Repositories
{
    public class User_Service : IUser_Service
    {
       
        private readonly IUser_Repository _usersRepository;
        private readonly IDay_Repository _dayRepository;
        private readonly IRating_Repository _ratingRepository;
        private readonly ICondition_Repository _conditionRepository;
        private readonly IClan_Repository _clanRepository;
        public User_Service(IUser_Repository users, IDay_Repository dayRepository, IRating_Repository ratingRepository, ICondition_Repository conditionRepository = null, IClan_Repository clanRepository = null)
        {
            _usersRepository = users;
            _dayRepository = dayRepository;
            _ratingRepository = ratingRepository;
            _conditionRepository = conditionRepository;
            _clanRepository = clanRepository;
        }
        #region CRUD
        public async Task<User> GetByLoginAsync(string email)
        {
            var user = await _usersRepository.GetObjectBy(email);
            user.actualTitleList = await GetActualAchievementUser(email);
            var clan = await _clanRepository.GetClanByUser(email);
            user.clanId = clan._id;
            return user;
        }


        #endregion

        public async Task<User> RegUserAsync(User newUser)
        {                            
                return await _usersRepository.SetObject(newUser); // inser newUsers        
        }
        public async Task<User> UpdateUser(User userUpdate)
        {
            return await _usersRepository.UpdateObject(userUpdate);
        }
        public async Task<User> GetUserbyCookie(string cookies)
        {
            return await _usersRepository.GetByCookie(cookies);
        }
        public async Task<bool> DeleteCookie(string userEmail)
        {
            var user = await _usersRepository.GetObjectBy(userEmail);
            user.LastCookie = null;
            return true;
        }
        public async Task<User> EditPassword(string email, string newPassword, string oldPassword)
        {
            var user = await GetByLoginAsync(email);
            if (HashCoder.Verify(user.Password, oldPassword))
            {
                user.Password = HashCoder.GetHash(newPassword);
                return await _usersRepository.UpdateObject(user);
            }

            else
            {
                throw new HttpRequestException("403 Bad password", null, HttpStatusCode.Forbidden);
            }

        }

        public async Task<string> DeleteUser(string email)
        {
            await _dayRepository.DeleteObjectsByEmail(email); 
            var ratingsUser = await _ratingRepository.GetRatingsByUserEmail(email);
            foreach (var rating in ratingsUser)
            {
                var position = rating.GetUserPosition(email);
                rating.DeleteUserPosition(position);
                await _ratingRepository.UpdateRating(rating);
            }
            var user = await _usersRepository.DeleteObject(email);
            return user._id.ToString();
        }

        public async Task<bool> CheckUser(string login)
        {
            return await _usersRepository.CheckUser(login);
        }
        #region friends system
        public async Task<User> RemoveRequest(string emailMain, string emailTarget)
        {
            var userTarget = await _usersRepository.GetObjectBy(emailTarget);
            if (!userTarget.RequrequestInFriends.Contains(emailMain))
            {
                throw new HttpRequestException("Request is not exist", null, HttpStatusCode.BadRequest);
            }
            userTarget.RequrequestInFriends.Remove(emailMain);
            return await _usersRepository.UpdateObject(userTarget);
        }
        public async Task<User> CreateRequestInFriend(string emailMain, string emailTarget)
        {
            var userOwnRequest = await _usersRepository.GetObjectBy(emailMain);
            var userTarget = await _usersRepository.GetObjectBy(emailTarget);
            if (userTarget.BlockedUsers.Contains(emailMain)|| userOwnRequest.BlockedUsers.Contains(emailTarget))
            {
                throw new HttpRequestException("You or he/she were on to blocked each other is list ", null, HttpStatusCode.BadRequest);
            }
            if (userTarget.Friends.Contains(emailMain))
            {
                throw new HttpRequestException("You are already on this user is list friends", null, HttpStatusCode.BadRequest);
            }
            if (userTarget.RequrequestInFriends.Contains(emailMain))
            {
                throw new HttpRequestException("You are already send request of friendship", null, HttpStatusCode.BadRequest);
            }

            userTarget.RequrequestInFriends.Add(emailMain);
            return await _usersRepository.UpdateObject(userTarget);            
        }

        public async Task<User> AcceptRequest(string emailUser, string emailTarget)
        {
            var userMain = await _usersRepository.GetObjectBy(emailUser);
            var userTarget = await _usersRepository.GetObjectBy(emailTarget);
            if (!userMain.RequrequestInFriends.Contains(emailTarget))
            {
                throw new HttpRequestException("Request is not exist", null, HttpStatusCode.BadRequest);
            }
            userMain.RequrequestInFriends.Remove(emailTarget);

            userMain.Friends.Add(emailTarget);
            userTarget.Friends.Add(emailUser);

            await _usersRepository.UpdateObject(userTarget);


            return await _usersRepository.UpdateObject(userMain);

        }

        public async Task<User> DenyRequest(string emailUser, string emailRequested)
        {
            var userMain = await _usersRepository.GetObjectBy(emailUser);
            if (!userMain.RequrequestInFriends.Contains(emailRequested))
            {
                throw new HttpRequestException("Request is not exist", null, HttpStatusCode.BadRequest);
            }
            userMain.RequrequestInFriends.Remove(emailRequested);
            return await _usersRepository.UpdateObject(userMain);
        }

        public async Task<User> AddInBlockedList(string emailUser, string emailTarget)
        {
            var userMain = await _usersRepository.GetObjectBy(emailUser);
            if (userMain.BlockedUsers.Contains(emailTarget))
            {
                throw new HttpRequestException("This user alredy existed to blocked list", null, HttpStatusCode.BadRequest);
            }
            if (userMain.Friends.Contains(emailTarget))
            {
                throw new HttpRequestException("You can not add friend to blocked list", null, HttpStatusCode.BadRequest);
            }
            userMain.BlockedUsers.Add(emailTarget);
            return await _usersRepository.UpdateObject(userMain);
        }

        public async Task<User> RemoveFromBlockedList(string emailMain, string emailTarget)
        {
            var userMain = await _usersRepository.GetObjectBy(emailMain);
            if (!userMain.BlockedUsers.Contains(emailTarget))
            {
                throw new HttpRequestException("This user not existed to blocked list", null, HttpStatusCode.BadRequest);
            }
            userMain.BlockedUsers.Remove(emailTarget);
            return await  _usersRepository.UpdateObject(userMain);
        }

        public async Task<User> RemoveFriend(string emailMain, string emailTarget)
        {
            var userMain = await _usersRepository.GetObjectBy(emailMain);
            var userTarget = await _usersRepository.GetObjectBy(emailTarget);
            if (!userMain.Friends.Contains(emailTarget))
            {
                throw new HttpRequestException("This user not existed to friend list", null, HttpStatusCode.BadRequest);
            }
            userMain.Friends.Remove(emailTarget);
            userTarget.Friends.Remove(emailMain);

            await _usersRepository.UpdateObject(userMain);
            return await _usersRepository.UpdateObject(userTarget);

        }



        #endregion

        #region View methods



        #endregion

        public async  Task UpdateLastBeOnline(string userEmail)
        {
            var user = await _usersRepository.GetObjectBy(userEmail);
            user.LastBeOnline = DateTime.UtcNow;
            await _usersRepository.UpdateObject(user);
        }

        public async Task<string> GetActualAchievementUser(string userEmail)
        {
            string type = "achievement";
            int groudId = 3;
            var actualSteps = await _dayRepository.GetAllStepsUser(userEmail, null);
            var listAchievements = await _conditionRepository.GetGroupConditionsAsync(type, groudId);
            int idAchievement = 0;
            foreach ( Condition achievement in listAchievements)
            {
                if(achievement.Distance > actualSteps)
                {
                    idAchievement = achievement.IdLocal - 1;
                }
            }
            return listAchievements.Find(a => a.IdLocal == idAchievement)._id;


        }

        public async Task<List<User>> GetFriends(string emailMain, int page)
        {
            //var user = await _usersRepository.GetObjectBy(emailMain);

            return new List<User>();
        }

        Task<User> IUser_Service.GetFriends(string emailMain, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsers(string searchText, int page, string? myRegionId)
        {
            return await  _usersRepository.GetUsers(searchText, myRegionId, page);
            
        }

       
    }
}
