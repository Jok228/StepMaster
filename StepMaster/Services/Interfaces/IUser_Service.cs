using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces
{
    public interface IUser_Service
    {
        public Task<User> RegUserAsync(User newUser);

        public Task<List<User>> GetAllUser();

        public Task<User> GetUser(string username, string password);

    }
}
