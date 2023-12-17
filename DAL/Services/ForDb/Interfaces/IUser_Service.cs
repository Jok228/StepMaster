using StepMaster.Models.Entity;
using Domain.Entity.API;

namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IUser_Service
    {
        public Task<BaseResponse<User>> GetByLoginAsync(string login);


        public Task<BaseResponse<List<User>>> FindUserByParams(string region = "none");

        public Task<BaseResponse<UserResponse>> EditUser(string email, UserResponse user);

        public Task<User> RegUserAsync(User newUser);


        public Task<BaseResponse<bool>> CheckPassword(string login,string password);

        public Task<User> UpdateUser(User userUpdate);

        public Task<List<User>> GetAllUser();

        public Task<User> GetUserbyCookie(string cookies);

        public Task<BaseResponse<bool>> DeleteCookie(string userEmail);


    }
}
