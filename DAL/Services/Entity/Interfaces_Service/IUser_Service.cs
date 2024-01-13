using StepMaster.Models.Entity;
using Domain.Entity.API;
using System.Globalization;
using Domain.Entity.Main.Titles;

namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IUser_Service
    {
        public Task<BaseResponse<User>> GetByLoginAsync(string login);
        public Task<BaseResponse<User>> RegUserAsync(User newUser);

        public Task<BaseResponse<User>> EditPassword(string email, string newPassword,  string oldPassword );
        public Task<BaseResponse<User>> UpdateUser(User userUpdate);

        public Task<BaseResponse<User>> GetUserbyCookie(string cookies);

        public Task<BaseResponse<bool>> DeleteCookie(string userEmail);

        public Task<BaseResponse<string>> DeleteUser(string user);
    }
}
