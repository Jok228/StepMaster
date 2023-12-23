using MongoDB.Driver;
using StepMaster.Models.Entity;
using StepMaster.Models.HashSup;
using StepMaster.Services.ForDb.Interfaces;
using Domain.Entity.API;
using Application.Repositories.S3.Interfaces;
using Application.Repositories.Db.Interfaces_Repository;
using Application.Services.Entity.Interfaces_Service;
using Application.Services.Entity.Realization_Services;

namespace StepMaster.Services.ForDb.Repositories
{
    public class User_Service : IUser_Service
    {
        private readonly IRating_Service _ratingService;
        private readonly IUser_Repository _users;
        private readonly IDay_Repository _days;
        private readonly IRating_Repository _rating;
        private readonly IAws_Repository _aws;
        public User_Service(IUser_Repository users, IRating_Repository rating, IAws_Repository aws, IDay_Repository day, IRating_Service ratingervice)
        {
            _users = users;
            _rating = rating;
            _aws = aws;
            _days = day;
            _ratingService = ratingervice;
        }


        public async Task<BaseResponse<User>> EditUser(string email, User userEdit)
        {
            var oldUser = await _users.GetObjectBy(email);
            await _ratingService.UpdateRatingsUser(oldUser.Data, userEdit);
            await _users.UpdateObject(oldUser.Data.UpdateUser(userEdit));
            
            if (oldUser.Status == MyStatus.Success)
            {
                return oldUser;
            }
            return oldUser;
        }
        public async Task<BaseResponse<User>> GetByLoginAsync(string email)
        {
            return await _users.GetObjectBy(email);
        }
        public async Task<BaseResponse<User>> RegUserAsync(User newUser)
        {
            try
            {
                var newRating = new UserRating(newUser.email, newUser.nickname);//Create new user rating for top list in DB

                var listRatRegion = await _ratingService.GetRating(newUser.region_id);
                listRatRegion.ratingUsers.Add(newRating);
                listRatRegion.Sort();

                var listRatCountry = await _ratingService.GetRating(null);
                listRatCountry.ratingUsers.Add(newRating);
                listRatCountry.Sort();


                await _rating.UpdateRating(listRatCountry);
                await  _rating.UpdateRating(listRatRegion);

                var placeInRegion = await _ratingService.GetPlace(newUser.region_id, newUser.email);
                var placeInCountry = await _ratingService.GetPlace(null, newUser.email);
                newUser.rating = new PlaceUserOnRating
                {
                    placeInCountry = placeInCountry,
                    placeInRegion = placeInRegion,
                };
                await _users.SetObject(newUser); // inser newUsers
                return BaseResponse<User>.Create(newUser, MyStatus.Success);
            }
            catch
            {
                return BaseResponse<User>.Create(null, MyStatus.Except); ;
            }
        }
        public async Task<BaseResponse<User>> UpdateUser(User userUpdate)
        {
            return await _users.UpdateObject(userUpdate);
        }
        public async Task<BaseResponse<User>> GetUserbyCookie(string cookies)
        {
            return await _users.GetByCookie(cookies);
        }
        public async Task<BaseResponse<bool>> DeleteCookie(string userEmail)
        {
            var user = await _users.GetObjectBy(userEmail);
            if (user.Data != null)
            {
                user.Data.lastCookie = null;
                return BaseResponse<bool>.Create(true, MyStatus.Success);
            }
            else
            {
                return BaseResponse<bool>.Create(false, user.Status);
            }


        }
        public async Task<BaseResponse<User>> EditPassword(string email, string newPassword, string oldPassword)
        {
            var user = await _users.GetObjectBy(email);
            if (user.Data != null)
            {
                if (HashCoder.Verify(user.Data.password, oldPassword))
                {
                    user.Data.password = HashCoder.GetHash(newPassword);
                    return await _users.UpdateObject(user.Data);
                }
                else
                {
                    return BaseResponse<User>.Create(null, MyStatus.Unauthorized);
                }
            }
            else
            {
                return BaseResponse<User>.Create(null, MyStatus.NotFound);
            }

        }
        public async Task<BaseResponse<User>> GetFullUser(string email)
        {
            var user = await _users.GetObjectBy(email);
            if (user.Status == MyStatus.Success)
            {
                var link = await _aws.GetLink(email);
               
                    var rating = await GetPlace(email, user.Data.region_id);
                    if (rating.Status == MyStatus.Success)
                    {
                        user.Data.AvatarLink = link;
                        user.Data.rating = rating.Data;
                        return user;
                    }
               

            }
            return BaseResponse<User>.Create(null, MyStatus.NotFound);
        }
        public async Task<BaseResponse<PlaceUserOnRating>> GetPlace(string email, string region)
        {
            await _ratingService.UpdateRating(region);
            await _ratingService.UpdateRating(null);
            var placeInCountry = await _ratingService.GetPlace(null, email);
            var placeInRegion = await _ratingService.GetPlace(region, email);

            return BaseResponse<PlaceUserOnRating>.Create(new PlaceUserOnRating
            {
                placeInCountry = placeInCountry,
                placeInRegion = placeInRegion,
            }, MyStatus.Success
            );
        }
    }
}
