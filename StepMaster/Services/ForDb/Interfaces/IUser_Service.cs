using StepMaster.Models.Entity;
using StepMaster.Models.Entity.Response;

namespace StepMaster.Services.ForDb.Interfaces
{
    public interface IUser_Service
    {
        public Task<User> GetByLoginAsync(string login);
        public Task<User> RegUserAsync(User newUser);

        public Task<BaseResponse<bool>> CheckPassword(string login,string password);

        public Task<User> UpdateUser(User userUpdate);

        public Task<List<User>> GetAllUser();

        public Task<User> GetUserbyCookie(string cookies);


    }
}
