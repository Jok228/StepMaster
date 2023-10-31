using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces
{
    public interface IUser_Service
    {
        public Task NewUser();

        public Task<List<User>> GetAllUser();

    }
}
