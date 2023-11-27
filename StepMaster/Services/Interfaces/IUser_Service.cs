using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces
{
    public interface IUser_Service
    {
        public Task<User> GetByLoginAsync(string login);
        public Task<User> RegUserAsync(User newUser);

        public Task<User>UpdateUser(User userUpdate);

        public Task<List<User>> GetAllUser();

        public Task<User> GetUserbyCookie (string cookies);


    }
}
